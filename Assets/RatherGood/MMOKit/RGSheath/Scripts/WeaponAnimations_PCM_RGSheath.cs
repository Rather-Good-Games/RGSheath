using UnityEngine;

namespace MultiplayerARPG.GameData.Model.Playables
{

    public partial struct WeaponAnimations : IWeaponAnims
    {
        [Header("Sheath animations for weapons")]

        public ActionAnimation rightHandSheathAnimations;
        public ActionAnimation rightHandUnSheathAnimations;

        public ActionAnimation leftHandSheathAnimations;
        public ActionAnimation leftHandUnSheathAnimations;

        public ActionAnimation dualWeildSheathAnimations;
        public ActionAnimation dualWeildUnSheathAnimations;

    }


}