using System;
using UnityEngine;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Components.BFG
{
    public class BFGArc : BehaviourWithTimerModules
    {
        const float ATTACK_INTERVAL = 0.1f;
        const float DAMAGE_MULT = 0.1f;

        public float keepRange = 10f;
        public Transform srcMarker;

        float attackTimer;
        Transform parentOrb;
        DamageInfo damage;
        Health victim;

        void Awake()
        {
            AddModule(ATTACK_INTERVAL, () =>
            {
                victim.Hurt(damage);
            });
        }

        public void Init(BFGCore parent)
        {
            parentOrb = parent.transform;
            damage = parent.Context.ToDamage();
            damage.damageValue *= DAMAGE_MULT;
        }
        public void BindVictim(DamageReceiver target)
        {
            victim = target.health;
            transform.SetParent(target.transform);
        }

        protected override void Update()
        {
            // check valid
            if (parentOrb == null || !parentOrb.gameObject.activeInHierarchy)
            {
                Destroy(gameObject);
                return;
            }
            var parentPos = parentOrb.position;
            if (Vector3.Distance(parentPos, transform.position) > keepRange)
            {
                Destroy(gameObject);
                return;
            }

            // update marker
            srcMarker.position = parentPos;

            // update attack
            if (victim != null) base.Update();
        }
    }
}
