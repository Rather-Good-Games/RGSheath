using UnityEngine;

namespace MultiplayerARPG
{
    public partial class MonsterCharacterEntity 
    {



        public override bool CanAttack()
        {
            if (isSheathed)
                return false;
            return base.CanAttack();
        }

        protected override void OnEquipWeaponSetChange(bool isInitial, byte equipWeaponSet)
        {

            if (CurrentGameInstance.enableRatherGoodSheath)
                StartCoroutine(WeaponSheathOrChangeProcess(isInitial, equipWeaponSet));
            else
                base.OnEquipWeaponSetChange(isInitial, equipWeaponSet);

        }

    }
}