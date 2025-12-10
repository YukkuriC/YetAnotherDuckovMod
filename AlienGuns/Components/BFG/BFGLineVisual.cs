using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components.BFG
{
    public class BFGLineVisual : MonoBehaviour
    {
        public LineRenderer line;
        public Transform src, dst;

        void Update()
        {
            var srcPos = src.position;
            var dstPos = dst.position;
            line.SetPositions(new Vector3[] { srcPos, dstPos });
        }
    }
}
