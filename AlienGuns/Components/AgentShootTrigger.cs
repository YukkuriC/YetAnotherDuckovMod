using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components
{
    [RequireComponent(typeof(ItemAgent_Gun))]
    public class AgentShootTrigger : MonoBehaviour
    {
        public ItemAgent_Gun agent;
        public Animator[] animators;
        public string triggerFire;

        void Awake()
        {
            if (agent == null) return;
            agent.OnShootEvent += OnShoot;
        }
        void OnDestroy()
        {
            if (agent == null) return;
            agent.OnShootEvent -= OnShoot;
        }
        void OnShoot()
        {
            foreach (var anim in animators) anim.SetTrigger(triggerFire);
        }
    }
}
