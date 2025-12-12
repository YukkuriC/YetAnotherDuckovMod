using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components.BFG
{
    public class BFGLineVisual : MonoBehaviour
    {
        public LineRenderer line;
        public Transform src, dst;
        public float turbulanceScale = 0.3f;
        public float minStep = 0.5f;

        bool stopWhenTimeFreeze = true;

        void Update()
        {
            if (stopWhenTimeFreeze && Time.timeScale <= 0) return;
            var srcPos = src.position;
            var dstPos = dst.position;
            var list = BuildArc(srcPos, dstPos, true).ToArray();
            line.positionCount = list.Length;
            line.SetPositions(list);
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

        public void ForceUpdate()
        {
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
