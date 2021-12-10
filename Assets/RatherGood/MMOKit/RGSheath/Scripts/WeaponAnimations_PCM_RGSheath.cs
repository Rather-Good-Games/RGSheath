using MultiplayerARPG;
using UnityEngine;

namespace MultiplayerARPG.GameData.Model.Playables
{ 
    [System.Serializable]
    public struct SheathAnimations : ISheathAnims
    {
        public WeaponType SheathweaponType;

        [Header("Sheath animations for weapons")]

        public ActionAnimation rightHandSheathAnimations;
        public ActionAnimation rightHandUnSheathAnimations;

        public ActionAnimation leftHandSheathAnimations;
        public ActionAnimation leftHandUnSheathAnimations;

        public ActionAnimation dualWeildSheathAnimations;
        public ActionAnimation dualWeildUnSheathAnimations;

        public WeaponType Data { get { return SheathweaponType; } }


    }
}