using LiteNetLib;
using LiteNetLibManager;
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
    }
}
