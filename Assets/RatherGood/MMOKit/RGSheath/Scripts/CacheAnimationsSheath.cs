using System.Collections;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    public interface ICacheAnimationsSheath { }

    public class CacheAnimationssheath<TWeaponAnims, TSkillAnims, TSheathAnims> : ICacheAnimationsSheath
        where TWeaponAnims : IWeaponAnims
        where TSkillAnims : ISkillAnims
        where TSheathAnims : ISheathAnims
    {
        public Dictionary<int, TWeaponAnims> CacheWeaponAnimations { get; protected set; }
        public Dictionary<int, TSkillAnims> CacheSkillAnimations { get; protected set; }
        public Dictionary<int, TSheathAnims> CacheSheathAnimations { get; protected set; }

        public CacheAnimationssheath(IEnumerable<TWeaponAnims> weaponAnimations, IEnumerable<TSkillAnims> skillAnimations, IEnumerable<TSheathAnims> sheathAnimations)
        {
            CacheWeaponAnimations = new Dictionary<int, TWeaponAnims>();
            foreach (TWeaponAnims weaponAnimation in weaponAnimations)
            {
                if (weaponAnimation.Data == null) continue;
                CacheWeaponAnimations[weaponAnimation.Data.DataId] = weaponAnimation;
            }

            CacheSkillAnimations = new Dictionary<int, TSkillAnims>();
            foreach (TSkillAnims skillAnimation in skillAnimations)
            {
                if (skillAnimation.Data == null) continue;
                CacheSkillAnimations[skillAnimation.Data.DataId] = skillAnimation;
            }

            CacheSheathAnimations = new Dictionary<int, TSheathAnims>();
            foreach (TSheathAnims sheathAnimation in sheathAnimations)
            {
                if (sheathAnimation.Data == null) continue;
                CacheSheathAnimations[sheathAnimation.Data.DataId] = sheathAnimation;
            }
        }
    }
}