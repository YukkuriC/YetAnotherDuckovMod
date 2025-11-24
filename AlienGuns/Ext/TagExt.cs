using Duckov.Utilities;
using ItemStatsSystem.Items;
using System.Collections.Generic;
using UnityEngine;

namespace YukkuriC.AlienGuns.Ext
{
    public static class TagExt
    {
        static Dictionary<string, Color> specialColors = new Dictionary<string, Color>()
        {
            ["Repairable"] = new Color(0.2924528f, 0.2924528f, 0.2924528f)
        };
        static Dictionary<string, Tag> _cachedTags = new Dictionary<string, Tag>();
        public static Tag AsTag(this string str)
        {
            if (!_cachedTags.TryGetValue(str, out var ret))
            {
                _cachedTags[str] = ret = ScriptableObject.CreateInstance<Tag>();
                ret.name = str;
                ret.show = true;
                if (specialColors.TryGetValue(str, out var color)) ret.color = color;
            }
            return ret;
        }
        public static TagCollection Remove(this TagCollection tags, params string[] tagsRaw)
        {
            foreach (var tag in tagsRaw) tags.Remove(tag.AsTag());
            return tags;
        }
        public static TagCollection Add(this TagCollection tags, params string[] tagsRaw)
        {
            foreach (var tag in tagsRaw) tags.Add(tag.AsTag());
            return tags;
        }
        public static Slot With(this Slot slot, params string[] tags)
        {
            foreach (var tag in tags) slot.requireTags.Add(tag.AsTag());
            return slot;
        }
        public static Slot Without(this Slot slot, params string[] tags)
        {
            foreach (var tag in tags) slot.excludeTags.Add(tag.AsTag());
            return slot;
        }
    }
}
