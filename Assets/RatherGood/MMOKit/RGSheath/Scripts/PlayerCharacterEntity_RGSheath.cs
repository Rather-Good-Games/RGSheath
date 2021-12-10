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

        //If using PithIK mod (not required).
        PitchIKMgr_RGSheath pitchIKMgr_RGSheath;

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
            onEquipWeaponSetChange += EquipWeaponSetChange;
        }


        [DevExtMethods("OnDestroy")]
        protected void PlayerSheathOnDestroy()
        {
            onStart -= PlayerSheathInit;
            onUpdate -= PlayerSheathOnUpdate;
            onEquipWeaponSetChange -= EquipWeaponSetChange;
            onSheathChange -= StartShiethProcess;
        }

        public override bool CanDoActions()
        {
            return !this.IsDead() && !isSheathed && !IsReloading && !IsAttacking && !IsUsingSkill && !IsReloading && !IsPlayingActionAnimation();
        }

        private void EquipWeaponSetChange(byte equipWeaponSet)
        {
            StartShiethProcess(isSheathed.Value);
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

        void StartShiethProcess(bool isOpen)
        {
            playableCharacterModel_ForSheath.StartShiethProcess(isOpen);

            //If using MMORPG PithIK and PitchIKMgr_RGSheath mod (not required)
            if ((pitchIKMgr_RGSheath != null) && GameInstance.Singleton.disablePitchIKWhenSheathed)
                pitchIKMgr_RGSheath.UpdatePitchIKBasedOnWeaponDamageType(isOpen);

        }

    }
}

