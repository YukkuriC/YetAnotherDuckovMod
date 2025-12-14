using Cysharp.Threading.Tasks;
using Duckov.Buffs;
using Duckov.Scenes;
using Duckov.UI;
using Duckov.Utilities;
using ItemStatsSystem;
using UnityEngine;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Items.Guns
{
    public static class ChickSpawner
    {
        const float PROB_RECURSIVE = 0.3f;
        const float EXPLOSIVE_RANGE = 4;
        static Buff[] FuseBuffs = new Buff[] {
            GameplayDataSettings.Buffs.Burn,
            GameplayDataSettings.Buffs.Space,
            GameplayDataSettings.Buffs.Poison,
        };

        public static CharacterRandomPreset presetChick;
        public static Tag tagMelee;
        public static void Init(Item item, ItemSetting_Gun gun)
        {
            item.Constants.SetString(GunRegistry.HASH_CALIBER, "PWL");
            item.Constants.SetFloat("ExplosionRange", EXPLOSIVE_RANGE);
            item.Stats["BulletSpeed"].BaseValue *= 0.6f;
            item.Stats["Damage"].BaseValue *= 3;
            item.Stats["ArmorPiercing"].BaseValue = 3;

            gun.BindCustomFire(p =>
            {
                if (presetChick == null)
                {
                    presetChick = ResourceGrabber.Get<CharacterRandomPreset>("SpawnPreset_Animal_Jinitaimei");
                    presetChick = Object.Instantiate(presetChick);
                    Object.DontDestroyOnLoad(presetChick);
                    presetChick.nameKey = "Cname_AlienGun.CCB";
                }
                var vel = p.velocity * (0.6f + Random.value * 0.4f);
                vel += Vector3.up * (5 + Random.value * 10);
                SpawnChick(p.transform.position, vel, p.context).Forget();
            });

            var agent = (ItemAgent_Gun)item.CopyAgent();
            ItemUtils.AddMaterialToGun(agent, "Skin_StormCreature");
        }

        public static async UniTask SpawnChick(Vector3 muzzle, Vector3 velocity, ProjectileContext ctx, CharacterMainControl parentChick = null)
        {
            var isBig = Random.value < PROB_RECURSIVE;
            var player = ctx.fromCharacter;
            if (player == null) return;
            var chick = await presetChick.CreateCharacterAsync(muzzle, velocity, MultiSceneCore.MainScene.Value.buildIndex, null, false);
            chick.SetPosition(muzzle);
            chick.dropBoxOnDead = false;
            chick.CharacterItem.SetInt("Exp", 0);
            chick.CharacterItem.DisplayNameRaw = "CName_CCB";

            // launch
            var mover = chick.movementControl.characterMovement;
            mover.PauseGroundConstraint();
            mover.velocity = velocity;

            // pet
            chick.SetTeam(player.Team);
            if (chick.GetComponentInChildren<PetAI>() is PetAI pet) pet.SetMaster(player);

            // element & fuse
            var health = chick.Health;
            var fuseBuff = FuseBuffs.GetRandom();
            var changedElement = false;
            health.OnHurtEvent.AddListener(src =>
                {
                    chick.AddBuff(fuseBuff);
                    if (src.fromCharacter == null && !changedElement)
                    {
                        changedElement = true;
                        if (chick.CurrentHoldItemAgent is ItemAgent_MeleeWeapon melee)
                        {
                            melee.setting.element = src.elementFactors.Find(x => x.factor > 0).elementType;
                            melee.setting.buff = fuseBuff;
                            melee.setting.buffChance = 0.3f;
                        }

                    }
                });
            chick.AddBuff(fuseBuff);
            chick.AddBuff(GameplayDataSettings.Buffs.Weight_Light);

            // explode
            health.OnDeadEvent.AddListener(src =>
            {
                var dmg = ctx.ToDamage();
                if (src.fromCharacter == null) dmg.elementFactors = src.elementFactors;
                LevelManager.Instance.ExplosionManager.CreateExplosion(chick.transform.position, EXPLOSIVE_RANGE, dmg);
            });
            health.OnHealthChange.AddListener(h =>
            {
                if (h.CurrentHealth == 1) h.CurrentHealth = 0;
            });

            // 递归下崽器
            if (isBig)
            {
                chick.transform.localScale *= 2;
                chick.OnAttackEvent += (melee) =>
                {
                    var dir = chick.CurrentAimDirection;
                    var vel = dir * ctx.speed * 0.5f * (0.6f + Random.value * 0.4f);
                    vel += Vector3.up * (5 + Random.value * 10);
                    SpawnChick(chick.transform.position, vel, ctx, chick).Forget();
                };
            }
            if (parentChick != null)
            {
                parentChick.Health.OnDeadEvent.AddListener(dmg =>
                {
                    if (health == null) return;
                    dmg.damageValue = health.MaxHealth;
                    dmg.damageType = DamageTypes.realDamage;
                    dmg.Attack(chick.mainDamageReceiver, noFriendlyFire: false, dashingEvades: false);
                });
            }

            // hide health bar
            health.showHealthBar = false;
            HealthBarManager.Instance.GetActiveHealthBar(health)?.Release();
        }
    }
}
