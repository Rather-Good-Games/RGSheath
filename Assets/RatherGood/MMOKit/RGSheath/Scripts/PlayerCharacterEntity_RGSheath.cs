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

        private PlayableCharacterModel_Custom playableCharacterModel_ForSheath;


        PitchIKMgr_RGSheath pitchIKMgr_RGSheath; //If using PithIK mod (not required).

        [DevExtMethods("Awake")]
        protected void PlayerSheathAwake()
        {
            playableCharacterModel_ForSheath = GetComponent<PlayableCharacterModel_Custom>();
            pitchIKMgr_RGSheath = GetComponent<PitchIKMgr_RGSheath>();
            IsSheathed = GameInstance.Singleton.enableCharacterSelectSheath;
            onStart += PlayerSheathInit;
            onUpdate += PlayerSheathOnUpdate;
        }

        protected void PlayerSheathInit()
        {
            //Need to init here and not awake doesn't register
            onSheathChange += StartShiethProcess;
        }


        [DevExtMethods("OnDestroy")]
        protected void PlayerSheathOnDestroy()
        {
            onStart -= PlayerSheathInit;
            onUpdate -= PlayerSheathOnUpdate;
            onSheathChange -= StartShiethProcess;
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
                this.CallServerSheathWeapon();
            }
        }


        /// <summary>
        /// Sheath process does not need to trigger weapon change.
        /// </summary>
        /// <param name="isOpen"></param>
        void StartShiethProcess(bool isOpen)
        {
            playableCharacterModel_ForSheath.StartShiethProcess(isOpen); //null equipWeaponSet will only sheith/unsheath current weapons

            if ((pitchIKMgr_RGSheath != null) && GameInstance.Singleton.disablePitchIKWhenSheathed)
                pitchIKMgr_RGSheath.UpdatePitchIKBasedOnWeaponDamageType(isSheathed);

        }


        protected override void OnEquipWeaponSetChange(bool isInitial, byte equipWeaponSet)
        {

            if (CurrentGameInstance.enableRatherGoodSheath)
                StartCoroutine(WeaponSheathOrChangeProcess(isInitial, equipWeaponSet));
            else
                base.OnEquipWeaponSetChange(isInitial, equipWeaponSet);

        }

        private IEnumerator WeaponSheathOrChangeProcess(bool isInitial, byte equipWeaponSet)
        {
            EquipWeapons newEquipWeapons = SelectableWeaponSets[equipWeaponSet];

            playableCharacterModel_ForSheath.StartShiethProcess(isSheathed, newEquipWeapons);

            while (playableCharacterModel_ForSheath.weaponChangeInProcess)
            {
                yield return null;
            }

            //this will trigger the OnEquipWeaponSetChange event after swap for toher listenera
            base.OnEquipWeaponSetChange(isInitial, equipWeaponSet);

            //If using MMORPG PithIK and PitchIKMgr_RGSheath mod (not required)
            if ((pitchIKMgr_RGSheath != null) && GameInstance.Singleton.disablePitchIKWhenSheathed)
                pitchIKMgr_RGSheath.UpdatePitchIKBasedOnWeaponDamageType(isSheathed);

        }

    }
}




