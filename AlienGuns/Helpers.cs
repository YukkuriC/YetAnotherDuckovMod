using Duckov.Utilities;
using ItemStatsSystem.Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using YukkuriC.AlienGuns.Events;

namespace YukkuriC.AlienGuns
{
    public static class Helpers
    {

        #region items
        public static void BindCustomFire(this ItemSetting_Gun gun, Action<Projectile> sub)
        {
            gun.bulletPfb = FxLib.Bullets.BulletDelegate;
            AlienGunFireEvents.EventsById[gun.Item.TypeID] = sub;
        }
        public static ItemSetting_Gun GetGun(this Projectile proj)
        {
            var agent = proj.context.fromCharacter?.CurrentHoldItemAgent;
            Debug.Log(agent);
            return agent is ItemAgent_Gun gun ? gun.GunItemSetting : null;
        }
        #endregion

        #region resources
        static Assembly _myDll;
        static string _myDllName;
        static string _myDllPath;
        static void InitDll()
        {
            if (_myDll == null)
            {
                _myDll = Assembly.GetExecutingAssembly();
                _myDllName = _myDll.GetName().Name;
                _myDllPath = Path.GetDirectoryName(_myDll.Location);
            }
        }
        public static Stream GetResourceStream(string path)
        {
            InitDll();
            return _myDll.GetManifestResourceStream($"{_myDllName}.assets.{path}");
        }
        public static byte[] GetResourceData(string path)
        {
            var stream = GetResourceStream(path);
            if (stream == null) return null;
            var raw = new byte[stream.Length];
            stream.Read(raw, 0, raw.Length);
            return raw;
        }
        public static byte[] GetLooseData(string path)
        {
            InitDll();
            var fullPath = Path.Combine(_myDllPath, path);
            if (!File.Exists(fullPath)) return null;
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var raw = new byte[stream.Length];
                stream.Read(raw, 0, raw.Length);
                return raw;
            }
        }
        public static T ToResourceJson<T>(this string path) where T : class
        {
            var raw = GetResourceData(path);
            if (raw == null) return null;
            var text = Encoding.UTF8.GetString(raw);
            return JsonConvert.DeserializeObject<T>(text);
        }
        public static Texture2D ToResourceTexture(this string path, int width = 256, int height = 256)
        {
            var raw = GetResourceData(path);
            if (raw == null) return null;
            var texture = new Texture2D(width, height);
            texture.LoadImage(raw);
            return texture;
        }
        #endregion
    }
}
