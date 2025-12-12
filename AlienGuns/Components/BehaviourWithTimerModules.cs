using System;
using System.Collections.Generic;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components
{
    public abstract class BehaviourWithTimerModules : MonoBehaviour
    {
        protected List<TimerModule> modules = new List<TimerModule>();
        public TimerModule AddModule(float _interval, Action _onTimed)
        {
            var module = new TimerModule(_interval, _onTimed);
            modules.Add(module);
            return module;
        }

        protected virtual void OnEnable()
        {
            foreach (var m in modules) m.OnEnable();
        }
        protected virtual void Update()
        {
            foreach (var m in modules) m.Update();
        }

        public class TimerModule
        {
            public float timer, interval;
            public Action onTimed;

            public TimerModule(float _interval, Action _onTimed)
            {
                interval = _interval;
                onTimed = _onTimed;
            }

            public void OnEnable()
            {
                timer = UnityEngine.Random.value * interval;
            }
            public void Update()
            {
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    timer -= interval;
                    onTimed();
                }
            }
        }
    }
}
