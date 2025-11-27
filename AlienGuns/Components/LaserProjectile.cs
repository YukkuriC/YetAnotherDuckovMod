using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components
{
    public class LaserProjectile : Projectile
    {
        void Update()
        {
            var start = context.firstFrameCheckStartPoint;
            var hits = Physics.SphereCastAll(start, radius, direction, context.distance, hitLayers, QueryTriggerInteraction.Ignore).ToList();
            hits.Sort((x, y) => (int)Mathf.Sign(x.distance - y.distance));
        }
    }
}
