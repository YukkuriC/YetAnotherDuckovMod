using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components.BFG
{
    public class BFGLineVisual : MonoBehaviour
    {
        public LineRenderer line;
        public Transform src, dst;
        public float width = 0.2f;
        public float turbulanceScale = 0.3f;
        public float minStep = 0.5f;

        void OnAwake()
        {
            line.startWidth = line.endWidth = width;
        }
        void Update()
        {
            if (Time.timeScale <= 0) return;
            var srcPos = src.position;
            var dstPos = dst.position;
            var list = BuildArc(srcPos, dstPos, true).ToArray();
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
                foreach (var sub in BuildArc(mid, end, outputEnd)) yield return sub;
            }
            else
            {
                yield return start;
            }
            if (outputEnd) yield return end;
        }
    }
}
