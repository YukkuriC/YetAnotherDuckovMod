using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components.BFG
{
    public class BFGLineVisual : BehaviourWithTimerModules
    {
        public LineRenderer line;
        public Transform src, dst;
        public float turbulanceScale = 0.3f;
        public float minStep = 0.5f;
        public float refreshInterval = 0.5f;
        public float showChance = 1;

        bool stopWhenTimeFreeze = true;
        Vector3[] baseLine, offsets, arc;
        TimerModule mRefreshArc;
        bool activeThisTurn;

        void Awake()
        {
            mRefreshArc = AddModule(refreshInterval, () =>
            {
                activeThisTurn = line.enabled = Random.value < showChance;
                if (!activeThisTurn) return;
                var srcPos = src.position;
                var dstPos = dst.position;
                arc = BuildArc(srcPos, dstPos, true).ToArray();
                line.positionCount = arc.Length;
                baseLine = BuildBaseLine(srcPos, dstPos, arc.Length);
                offsets = new Vector3[arc.Length];
                for (int i = 0; i < arc.Length; i++) offsets[i] = arc[i] - baseLine[i];
            });
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            mRefreshArc.onTimed();
        }
        protected override void Update()
        {
            base.Update();
            if (!activeThisTurn) return;
            if (stopWhenTimeFreeze && Time.timeScale <= 0) return;
            var progress = mRefreshArc.timer / refreshInterval;
            baseLine = BuildBaseLine(src.position, dst.position, arc.Length);
            for (int i = 0; i < arc.Length; i++) arc[i] = baseLine[i] + offsets[i] * progress;
            line.SetPositions(arc);
        }

        IEnumerable<Vector3> BuildArc(Vector3 start, Vector3 end, bool outputEnd = false)
        {
            var dist = Vector3.Distance(start, end);
            if (dist > minStep)
            {
                var mid = (start + end) / 2;
                mid += Random.insideUnitSphere * (dist * turbulanceScale);
                foreach (var sub in BuildArc(start, mid)) yield return sub;
                foreach (var sub in BuildArc(mid, end)) yield return sub;
            }
            else
            {
                yield return start;
            }
            if (outputEnd) yield return end;
        }
        Vector3[] BuildBaseLine(Vector3 start, Vector3 end, int count)
        {
            if (count <= 2) return new Vector3[] { start, end };
            var res = new Vector3[count];
            res[0] = start;
            var dir = (end - start) / (count - 1);
            for (int i = 1; i < count; i++) res[i] = res[i - 1] + dir;
            return res;
        }

        public void ForceUpdate()
        {
            if (mRefreshArc == null) Awake();
            OnEnable();
            stopWhenTimeFreeze = false;
            Update();
            stopWhenTimeFreeze = true;
        }
        public void ResetForSave()
        {
            var srcPos = src.position;
            var dstPos = dst.position;
            line.positionCount = 3;
            line.SetPositions(new Vector3[] { srcPos, (srcPos + dstPos) / 2, dstPos });
        }
    }
}
