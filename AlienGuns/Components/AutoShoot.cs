
using UnityEngine;

namespace YukkuriC.AlienGuns.Components
{
    public class AutoShoot : MonoBehaviour
    {
        public Projectile bullet;
        public float interval = 1;
        public ProjectileContext context = default;
        [SerializeField]
        string contextSerializeFixer = null;
        public bool doRemoveLastShot = false;

        float timer;
        ItemAgent_Gun gun;
        CharacterMainControl holder;
        Projectile lastShot;

        public void RecordContextFix()
        {
            contextSerializeFixer = JsonUtility.ToJson(context);
        }
        void Awake()
        {
            if (contextSerializeFixer != null)
            {
                try
                {
                    context = JsonUtility.FromJson<ProjectileContext>(contextSerializeFixer);
                }
                catch (System.Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
        void OnEnable()
        {
            timer = 0;
            gun = GetComponent<ItemAgent_Gun>();
            holder = gun?.Holder;
            context.fromCharacter = holder;
        }
        void Update()
        {
            if (holder == null) return;
            if (timer < interval) timer += Time.deltaTime;
            else if (!holder.Running && !holder.Dashing)
            {
                timer -= interval;
                DoShoot();
            }
        }
        void DoShoot()
        {
            var src = gun.muzzle.transform.position;
            var dst = holder.GetCurrentAimPoint();
            if (doRemoveLastShot && lastShot != null && lastShot.isActiveAndEnabled) lastShot.Release();
            lastShot = BulletLib.ShootOneBullet(bullet, context, src, (dst - src).normalized);
        }
    }
}
