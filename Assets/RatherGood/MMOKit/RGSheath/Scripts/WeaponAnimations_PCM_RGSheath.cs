using UnityEngine;

namespace MultiplayerARPG.GameData.Model.Playables
{

    public partial struct WeaponAnimations : IWeaponAnims
    {
        [Header("Sheith animations for weapons")]

        public ActionAnimation rightHandSheithAnimations;
        public ActionAnimation rightHandUnSheithAnimations;

        public ActionAnimation leftHandSheithAnimations;
        public ActionAnimation leftHandUnSheithAnimations;

        public ActionAnimation dualWeildSheithAnimations;
        public ActionAnimation dualWeildUnSheithAnimations;

    }


}