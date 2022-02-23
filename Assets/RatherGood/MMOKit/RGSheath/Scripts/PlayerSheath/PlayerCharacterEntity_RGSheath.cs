using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLibManager;
using LiteNetLib;
using MultiplayerARPG.GameData.Model.Playables;

namespace MultiplayerARPG
{

    //only works with PlayableCharacterModel
    public partial class PlayerCharacterEntity
    {

        [DevExtMethods("Awake")]
        protected void PlayerSheathAwake()
        {

            if (!CurrentGameInstance.enableRatherGoodSheath)
                return;

            IsSheathed = GameInstance.Singleton.enableCharacterSelectSheath;

            onUpdate += PlayerSheathOnUpdate;

        }

        [DevExtMethods("OnDestroy")]
        protected void PlayerSheathOnDestroy()
        {
            onUpdate -= PlayerSheathOnUpdate;

        }

        public override bool CanAttack()
        {
            if (isSheathed)
                return false;
            return base.CanAttack();
        }

        protected void PlayerSheathOnUpdate()
        {
            if (!IsOwnerClient || !CurrentGameInstance.enableRatherGoodSheath)
                return;

            if (InputManager.GetButtonDown(CurrentGameInstance.sheathButtonName))
            {
                this.CallServerSheathWeapon(!IsSheathed);
            }
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




