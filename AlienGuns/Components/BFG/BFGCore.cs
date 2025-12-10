using Duckov.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components.BFG
{
    public class BFGCore : BehaviourWithTimerModules
    {
        const float CHECK_CHARA_INTERVAL = 0.1f;
        const float RANDOM_ARC_INTERVAL = 0.2f;

        public float CheckRange = 6f;
        public BFGArc prefabArc;

        Projectile proj;
        Dictionary<DamageReceiver, BFGArc> charaMarked;

        void Awake()
        {
            proj = GetComponent<Projectile>();
            AddModule(CHECK_CHARA_INTERVAL, () =>
            {
                foreach (var collider in Physics.OverlapSphere(transform.position, CheckRange, GameplayDataSettings.Layers.damageReceiverLayerMask))
                {
                    var receiver = collider.GetComponent<DamageReceiver>();
                    if (receiver?.health == null) continue;

                    // check chara team
                    if (receiver.health?.TryGetCharacter() is CharacterMainControl chara)
                    {
                        if (!Team.IsEnemy(chara.Team, Context.team) || chara.Dashing) continue;
                    }

                    SpawnArc(receiver);
                }
            });
            var terrainMask = GameplayDataSettings.Layers.groundLayerMask | GameplayDataSettings.Layers.wallLayerMask;
            AddModule(RANDOM_ARC_INTERVAL, () =>
            {
                var dir = Random.onUnitSphere;
                if (!Physics.Raycast(transform.position, dir, out var res, CheckRange, terrainMask)) return;
                SpawnArc(res.point);
            });
        }
        public ProjectileContext Context => proj.context;

        BFGArc SpawnArc(Vector3 worldPos)
        {
            var arc = Instantiate(prefabArc);
            arc.Init(this);
            arc.transform.position = worldPos;
            arc.gameObject.SetActive(true);
            return arc;
        }
        BFGArc SpawnArc(DamageReceiver receiver)
        {
            if (charaMarked.TryGetValue(receiver, out BFGArc bb)) return bb;
            var arc = SpawnArc(receiver.transform.position);
            arc.BindVictim(receiver);
            return charaMarked[receiver] = arc;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            charaMarked = new Dictionary<DamageReceiver, BFGArc>();
        }
    }
}
