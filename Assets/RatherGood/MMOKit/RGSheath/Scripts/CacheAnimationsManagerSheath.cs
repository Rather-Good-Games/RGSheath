using System.Collections;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    public static class CacheAnimationsManagerSheath
    {
        private static readonly Dictionary<int, ICacheAnimationsSheath> CacheAnims = new Dictionary<int, ICacheAnimationsSheath>();

        /// <summary>
        /// Create and set new `CacheAnimations` which created by `weaponAnimations` and `skillAnimations` data
        /// </summary>
        /// <typeparam name="TWeaponAnims"></typeparam>
        /// <typeparam name="TSkillAnims"></typeparam>
        /// <param name="id"></param>
        /// <param name="weaponAnimations"></param>
        /// <param name="skillAnimations"></param>
        public static void SetCacheAnimations<TWeaponAnims, TSkillAnims, TSheathAnims>(int id, IEnumerable<TWeaponAnims> weaponAnimations, IEnumerable<TSkillAnims> skillAnimations, IEnumerable<TSheathAnims> sheathAnimations)
        where TWeaponAnims : IWeaponAnims
        where TSkillAnims : ISkillAnims
        where TSheathAnims : ISheathAnims
        {
            CacheAnims[id] = new CacheAnimationssheath<TWeaponAnims, TSkillAnims, TSheathAnims>(weaponAnimations, skillAnimations, sheathAnimations);
        }

        /// <summary>
        /// Get `CacheAnimations` by specific ID
        /// </summary>
        /// <typeparam name="TWeaponAnims"></typeparam>
        /// <typeparam name="TSkillAnims"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CacheAnimationssheath<TWeaponAnims, TSkillAnims, TSheathAnims> GetCacheAnimations<TWeaponAnims, TSkillAnims, TSheathAnims>(int id)
            where TWeaponAnims : IWeaponAnims
            where TSkillAnims : ISkillAnims
            where TSheathAnims : ISheathAnims
        {
            return CacheAnims[id] as CacheAnimationssheath<TWeaponAnims, TSkillAnims, TSheathAnims>;
        }

        /// <summary>
        /// Create and set new `CacheAnimations` (If not exists), `CacheAnimations` will be created by `weaponAnimations` and `skillAnimations` data
        /// Then return `CacheAnimations`
        /// </summary>
        /// <typeparam name="TWeaponAnims"></typeparam>
        /// <typeparam name="TSkillAnims"></typeparam>
        /// <param name="id"></param>
        /// <param name="weaponAnimations"></param>
        /// <param name="skillAnimations"></param>
        /// <returns></returns>
        public static CacheAnimationssheath<TWeaponAnims, TSkillAnims, TSheathAnims> SetAndGetCacheAnimations<TWeaponAnims, TSkillAnims, TSheathAnims>(int id, IEnumerable<TWeaponAnims> weaponAnimations, IEnumerable<TSkillAnims> skillAnimations, IEnumerable<TSheathAnims> sheathAnimations)
            where TWeaponAnims : IWeaponAnims
            where TSkillAnims : ISkillAnims
            where TSheathAnims : ISheathAnims
        {
            if (!CacheAnims.ContainsKey(id))
                SetCacheAnimations(id, weaponAnimations, skillAnimations, sheathAnimations);
            return GetCacheAnimations<TWeaponAnims, TSkillAnims, TSheathAnims>(id);
        }

        /// <summary>
        /// Create and set new `CacheAnimations` (If not exists), `CacheAnimations` will be created by `weaponAnimations` and `skillAnimations` data
        /// Then return `WeaponAnimations`
        /// </summary>
        /// <typeparam name="TWeaponAnims"></typeparam>
        /// <typeparam name="TSkillAnims"></typeparam>
        /// <param name="id"></param>
        /// <param name="weaponAnimations"></param>
        /// <param name="skillAnimations"></param>
        /// <param name="dataId"></param>
        /// <param name="anims"></param>
        /// <returns></returns>
        public static bool SetAndTryGetCacheWeaponAnimations<TWeaponAnims, TSkillAnims, TSheathAnims>(int id, IEnumerable<TWeaponAnims> weaponAnimations, IEnumerable<TSkillAnims> skillAnimations, IEnumerable<TSheathAnims> sheathAnimations, int dataId, out TWeaponAnims anims)
            where TWeaponAnims : IWeaponAnims
            where TSkillAnims : ISkillAnims
            where TSheathAnims : ISheathAnims
        {
            return SetAndGetCacheAnimations(id, weaponAnimations, skillAnimations, sheathAnimations).CacheWeaponAnimations.TryGetValue(dataId, out anims);
        }

        public static bool SetAndTryGetCacheSheathAnimations<TWeaponAnims, TSkillAnims, TSheathAnims>(int id, IEnumerable<TWeaponAnims> weaponAnimations, IEnumerable<TSkillAnims> skillAnimations, IEnumerable<TSheathAnims> sheathAnimations, int dataId, out TSheathAnims anims)
        where TWeaponAnims : IWeaponAnims
        where TSkillAnims : ISkillAnims
        where TSheathAnims : ISheathAnims
        {
            return SetAndGetCacheAnimations(id, weaponAnimations, skillAnimations, sheathAnimations).CacheSheathAnimations.TryGetValue(dataId, out anims);
        }

        /// <summary>
        /// Create and set new `CacheAnimations` (If not exists), `CacheAnimations` will be created by `weaponAnimations` and `skillAnimations` data
        /// Then return `SkillAnimations`
        /// </summary>
        /// <typeparam name="TWeaponAnims"></typeparam>
        /// <typeparam name="TSkillAnims"></typeparam>
        /// <param name="id"></param>
        /// <param name="weaponAnimations"></param>
        /// <param name="skillAnimations"></param>
        /// <param name="dataId"></param>
        /// <param name="anims"></param>
        /// <returns></returns>
        public static bool SetAndTryGetCacheSkillAnimations<TWeaponAnims, TSkillAnims, TSheathAnims>(int id, IEnumerable<TWeaponAnims> weaponAnimations, IEnumerable<TSkillAnims> skillAnimations, IEnumerable<TSheathAnims> sheathAnimations, int dataId, out TSkillAnims anims)
            where TWeaponAnims : IWeaponAnims
            where TSkillAnims : ISkillAnims
            where TSheathAnims : ISheathAnims
        {
            return SetAndGetCacheAnimations(id, weaponAnimations, skillAnimations, sheathAnimations).CacheSkillAnimations.TryGetValue(dataId, out anims);
        }
    }
}
