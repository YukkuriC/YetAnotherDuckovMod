using UnityEngine;

namespace ProjectileReflector.Skills
{
    public static class FxLib
    {
        public static GameObject CreateSlash(ItemAgent_MeleeWeapon meleeWeapon, CharacterMainControl player, float? scale = null)
        {
            var position = player.transform.position;
            position.y = meleeWeapon.transform.position.y;
            var rotation = Quaternion.LookRotation(player.modelRoot.forward, Vector3.up);
            return CreateSlash(meleeWeapon, position, rotation, scale);
        }
        public static GameObject CreateSlash(ItemAgent_MeleeWeapon meleeWeapon, Vector3 position, Quaternion rotation, float? scale = null)
        {
            if (meleeWeapon?.slashFx == null) return null;
            var obj = Object.Instantiate(meleeWeapon.slashFx, position, rotation);
            if (scale != null) obj.transform.localScale = Vector3.one * (float)scale;
            return obj;
        }
    }
}
