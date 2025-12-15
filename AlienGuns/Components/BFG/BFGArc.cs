using UnityEngine;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Components.BFG
{
    public class BFGArc : BehaviourWithTimerModules
    {
        const float ATTACK_INTERVAL = 0.1f;
        const float DAMAGE_MULT = 0.1f;

        public float keepRange = 10f;
        public float keepRangeStatic = 10f;
        public Transform srcMarker;

        Transform parentOrb;
        DamageInfo damage;
        DamageReceiver victim;

        void Awake()
        {
            AddModule(ATTACK_INTERVAL, () =>
            {
                damage.Attack(victim, srcMarker.position, noFriendlyFire: false, dashingEvades: false);
            });
        }

        public void Init(BFGCore parent)
        {
            parentOrb = parent.transform;
            damage = parent.Context.ToDamage();
            damage.damageValue *= DAMAGE_MULT;
            damage.critRate = 0;
            BFGCore.SwitchElement(ref damage);
        }
        public void BindVictim(DamageReceiver target)
        {
            victim = target;
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
            var dist = Vector3.Distance(parentPos, transform.position);
            if (dist > keepRange || (victim == null && dist > keepRangeStatic))
            {
                Destroy(gameObject);
                return;
            }

            // update marker
            srcMarker.position = parentPos;

            // update attack
            if (victim != null) base.Update();
        }

        public void ForceUpdateSubVisuals()
        {
            foreach (var sub in GetComponentsInChildren<BFGLineVisual>(true)) sub.ForceUpdate();
        }
        public void ResetSubVisualsForSave()
        {
            foreach (var sub in GetComponentsInChildren<BFGLineVisual>(true)) sub.ResetForSave();
        }
    }
}
