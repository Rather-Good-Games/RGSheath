using LiteNetLib;
using LiteNetLibManager;
using MultiplayerARPG.GameData.Model.Playables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class BaseCharacterEntity
    {
        public event System.Action<bool> onSheathChange;

        [Category(5400, "RGSheath Stuff")]
        [SerializeField] protected SyncFieldBool isSheathed = new SyncFieldBool();

        public bool IsSheathed
        {
            get { return isSheathed.Value; }
            set { isSheathed.SetValue(value); }
        }

        [DevExtMethods("Awake")]
        protected void Setup()
        {
            if (!CurrentGameInstance.enableRatherGoodSheath)
                return;

            playableCharacterModel_ForSheath = GetComponent<PlayableCharacterModel_Custom>();
            pitchIKMgr_RGSheath = GetComponent<PitchIKMgr_RGSheath>();
            //IsSheathed = GameInstance.Singleton.enableCharacterSelectSheath;

            onStart += SheathInit;
            //onUpdate += SheathOnUpdate;

            onSetup += OnSetupSheathChange;
            onSetupNetElements += OnSetupElements;
        }

        [DevExtMethods("OnDestroy")]
        protected void DeSetup()
        {
            onSetup -= OnDeSetupSheathChange;
            onSetupNetElements -= OnSetupElements;
        }

        protected void OnSetupSheathChange()
        {
            isSheathed.onChange += OnSheathChange;
        }

        protected void OnSetupElements()
        {
            isSheathed.deliveryMethod = DeliveryMethod.ReliableOrdered;
            isSheathed.syncMode = LiteNetLibSyncField.SyncMode.ServerToClients;
        }

        protected void OnDeSetupSheathChange()
        {
            isSheathed.onChange -= OnSheathChange;
        }


        protected virtual void OnSheathChange(bool isInitial, bool sheath)
        {
            isRecaching = true;

            if (onSheathChange != null)
                onSheathChange.Invoke(sheath);
        }

        //Move from player entity

        protected PlayableCharacterModel_Custom playableCharacterModel_ForSheath;

        PitchIKMgr_RGSheath pitchIKMgr_RGSheath; //If using PithIK mod (not required).

        [DevExtMethods("OnDestroy")]
        protected void SheathOnDestroy()
        {
            onStart -= SheathInit;
            //onUpdate -= SheathOnUpdate;
            onSheathChange -= StartShiethProcess;
        }

        protected void SheathInit()
        {
           
            onSheathChange += StartShiethProcess; //Need to init here and not awake doesn't register
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

        protected IEnumerator WeaponSheathOrChangeProcess(bool isInitial, byte equipWeaponSet)
        {

            if ((SelectableWeaponSets == null) || (SelectableWeaponSets.Count == 0))
                yield break;

            EquipWeapons newEquipWeapons = SelectableWeaponSets[equipWeaponSet];

            playableCharacterModel_ForSheath.StartShiethProcess(isSheathed, newEquipWeapons);

            while (playableCharacterModel_ForSheath.weaponChangeInProcess)
            {
                yield return null;
            }

            //this will trigger the OnEquipWeaponSetChange event after swap for other listeners
            //OnEquipWeaponSetChange(isInitial, equipWeaponSet);

            //If using MMORPG PithIK and PitchIKMgr_RGSheath mod (not required)
            if ((pitchIKMgr_RGSheath != null) && GameInstance.Singleton.disablePitchIKWhenSheathed)
                pitchIKMgr_RGSheath.UpdatePitchIKBasedOnWeaponDamageType(isSheathed);

        }



        public bool CallServerSheathWeapon(bool isSheathed)
        {
            RPC(ServerSheathWeapon, isSheathed);
            return true;
        }

        [ServerRpc]
        protected void ServerSheathWeapon(bool isSheathed)
        {
#if !CLIENT_BUILD
            if (!CurrentGameInstance.enableRatherGoodSheath)
                return;

            IsSheathed = isSheathed;
#endif
        }





    }
}
