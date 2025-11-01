using FMOD;
using FMODUnity;
using System.Collections.Generic;
using System.IO;

namespace ProjectileReflector
{
    public static class ModAudio
    {
        static string GetAudioPath(string name, int idx)
        {
            return Path.Combine(ModBehaviour.ROOT_PATH, "sfx", $"{name}{idx}.wav");
        }
        static List<Sound> LoadAudioBatch(string name)
        {
            var output = new List<Sound>();
            int ptr = 1;
            while (true)
            {
                var path = GetAudioPath(name, ptr);
                if (!File.Exists(path)) break;
                var status = RuntimeManager.CoreSystem.createSound(path, MODE.LOOP_OFF, out var sound);
                if (status > RESULT.OK)
                {
                    UnityEngine.Debug.LogWarning($"[ProjectileReflector] load {path} failed: {status}");
                    break;
                }
                output.Add(sound);
                ptr++;
            }
            UnityEngine.Debug.Log($"[ProjectileReflector] loaded {output.Count} files for type {name}");
            return output;
        }

        static bool inited = false;
        static List<Sound> audioActive, audioPassive;
        public static void InitAudio()
        {
            if (inited) return;
            inited = true;

            audioActive = LoadAudioBatch("active");
            audioPassive = LoadAudioBatch("passive");
        }

        static HashSet<string> playedThisFrame = new HashSet<string>();
        static void PlayRandomSoundIn(List<Sound> pool, string flag)
        {
            if (pool.Count <= 0 || playedThisFrame.Contains(flag)) return;
            playedThisFrame.Add(flag);

            var sound = pool[UnityEngine.Random.Range(0, pool.Count)];
            RuntimeManager.GetBus("bus:/Master/SFX").getChannelGroup(out var grp);
            RuntimeManager.CoreSystem.playSound(sound, grp, false, out var channel);
            channel.setVolume(ModConfigs.SFX_VOLUME);
        }
        public static void PlaySoundActive() => PlayRandomSoundIn(audioActive, "active");
        public static void PlaySoundPassive() => PlayRandomSoundIn(audioPassive, "passive");
        public static void ClearPlayedFlag() => playedThisFrame.Clear();
    }
}
