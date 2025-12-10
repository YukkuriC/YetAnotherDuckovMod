using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components
{
    public abstract class BehaviourWithTimerModules : MonoBehaviour
    {
        protected List<TimerModule> modules = new List<TimerModule>();
        public void AddModule(float _interval, Action _onTimed) => modules.Add(new TimerModule(_interval, _onTimed));

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
            float timer, interval;
            Action onTimed;

            public TimerModule(float _interval, Action _onTimed)
            {
                interval = _interval;
                onTimed = _onTimed;
            }

            public void OnEnable()
            {
                timer = 0;
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
