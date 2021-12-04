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
        
        //TODO: Fix Charge animation with 0 time
        //TODO: Check multiplayer. need RPC call?
        [Category(5400, "RGSheath Stuff")]
        [SerializeField] protected SyncFieldBool isSheathed = new SyncFieldBool();
        public bool IsSheathed
        {
            get { return isSheathed.Value; }
            set { isSheathed.SetValue(value); }
        }

        private PlayableCharacterModel_Custom playableCharacterModel_ForSheath;

        //If using PithIK mod (not required).
        PitchIKMgr_RGSheath pitchIKMgr_RGSheath;

        [DevExtMethods("Awake")]
        protected void PlayerSheathAwake()
        {
            playableCharacterModel_ForSheath = GetComponent<PlayableCharacterModel_Custom>();

            pitchIKMgr_RGSheath = GetComponent<PitchIKMgr_RGSheath>();

            onStart += PlayerSheathInit;
            onUpdate += PlayerSheathOnUpdate;
        }

        protected void PlayerSheathInit()
        {
            //Need to init here and not awake doesn't register
            isSheathed.onChange += OnIsSheathedChange;
            onEquipWeaponSetChange += EquipWeaponSetChange;
        }


        [DevExtMethods("OnDestroy")]
        protected void PlayerSheathOnDestroy()
        {
            onStart -= PlayerSheathInit;
            onUpdate -= PlayerSheathOnUpdate;
            onEquipWeaponSetChange -= EquipWeaponSetChange;
            isSheathed.onChange -= OnIsSheathedChange;
        }

        public override bool CanDoActions()
        {
            return !this.IsDead() && !IsReloading && !IsAttacking && !IsUsingSkill && !IsReloading && !IsPlayingActionAnimation();
        }

        //Change CanAttack to virtual
        public override bool CanAttack()
        {
            if (isSheathed)
                return false;
            return base.CanAttack();
        }
        private void EquipWeaponSetChange(byte equipWeaponSet)
        {
            UpdatePlayerWeaponItems();
        }

        void UpdatePlayerWeaponItems()
        {
            StartShiethProcess();
        }

        protected void PlayerSheathOnUpdate()
        {
            if (!IsOwnerClient || !CurrentGameInstance.enableRatherGoodSheath)
                return;

            if (InputManager.GetButtonDown(CurrentGameInstance.sheathButtonName))
            {
                IsSheathed = !IsSheathed;
            }
        }

        private void OnIsSheathedChange(bool isInitial, bool isOpen)
        {
            StartShiethProcess();
        }
        void StartShiethProcess()
        {
            playableCharacterModel_ForSheath.StartShiethProcess(IsSheathed);

            //If using MMORPG PithIK and PitchIKMgr_RGSheath mod (not required)
            if ((pitchIKMgr_RGSheath != null) && GameInstance.Singleton.disablePitchIKWhenSheathed)
                pitchIKMgr_RGSheath.UpdatePitchIKBasedOnWeaponDamageType(IsSheathed);

        }

    }
}

