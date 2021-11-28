using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLibManager;
using LiteNetLib;
using MultiplayerARPG.GameData.Model.Playables;

namespace MultiplayerARPG
{

    [RequireComponent(typeof(PitchIKMgr_RGSheath))]
    //only works with PlayableCharacterModel
    public partial class PlayerCharacterEntity
    {

        [Category(5400, "RGSheath Stuff")]
        [SerializeField] protected SyncFieldBool isSheathed = new SyncFieldBool();

        private PlayableCharacterModel_Custom playableCharacterModel_ForSheath;

        PitchIKMgr_RGSheath pitchIKMgr_RGSheath;
        public bool IsSheathed
        {
            get { return isSheathed.Value; }
            set { isSheathed.SetValue(value); }
        }

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

            //Need to init here not awake doesnt seem to register
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

            pitchIKMgr_RGSheath.UpdatePitchIKBasedOnWeaponDamageType(IsSheathed);

        }

    }
}

