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

        [Header("Player Sheathing RG")]

        [SerializeField] protected SyncFieldBool isSheathed = new SyncFieldBool();

        private PlayableCharacterModel playableCharacterModel_ForSheath;

        //IWeaponItem rightHandWeaponItem;
        //IWeaponItem leftHandWeaponItem;
        //IShieldItem leftHandShieldItem;

        CharacterPitchIK characterPitchIK;

        public bool IsSheathed
        {
            get { return isSheathed.Value; }
            set { isSheathed.Value = value; }
        }

        [DevExtMethods("Awake")]
        protected void PlayerSheathAwake()
        {
            playableCharacterModel_ForSheath = GetComponent<PlayableCharacterModel>();

            characterPitchIK = GetComponent<CharacterPitchIK>();

            onStart += PlayerSheathInit;
            onUpdate += PlayerSheathOnUpdate;
            onEquipWeaponSetChange += UpdatePlayerWeaponItems;
            onSelectableWeaponSetsOperation += UpdatePlayerWeaponItems;
            isSheathed.onChange += OnIsSheathedChange;
        }



        [DevExtMethods("OnDestroy")]
        protected void PlayerSheathOnDestroy()
        {
            onStart -= PlayerSheathInit;
            onUpdate -= PlayerSheathOnUpdate;
            onEquipWeaponSetChange -= UpdatePlayerWeaponItems;
            onSelectableWeaponSetsOperation -= UpdatePlayerWeaponItems;
            isSheathed.onChange -= OnIsSheathedChange;
        }


        public override bool CanDoActions()
        {
            return !this.IsDead() && !IsReloading && !IsAttacking && !IsUsingSkill && !IsReloading && !IsPlayingActionAnimation();
        }

        protected void PlayerSheathInit()
        {
            SetupSheathEquipWeapons(GameInstance.PlayingCharacterEntity.EquipWeapons);
            RegisterNetFunction<uint>(Cmd_SheathWeapons);
            RegisterNetFunction<uint>(Cmd_UnsheathWeapons);
        }


        // Updates layers on weapon changes only
        private void UpdatePlayerWeaponItems(byte equipWeaponSet)
        {
            UpdatePlayerWeaponItems();
        }

        private void UpdatePlayerWeaponItems(LiteNetLibSyncList.Operation operation, int index)
        {
            UpdatePlayerWeaponItems();
        }

        void UpdatePlayerWeaponItems()
        {
            if (!IsOwnerClient)
                return;

            if (!GameInstance.PlayingCharacterEntity)
                return;

            if (IsSheathed)
            {
                CallNetFunction(Cmd_SheathWeapons, FunctionReceivers.All, ObjectId);
            }
            else
            {
                CallNetFunction(Cmd_UnsheathWeapons, FunctionReceivers.All, ObjectId);
            }

        }


        /// <summary>
        /// Gets the current players equiped weapon(s) data and instatiated gameobject/s
        /// </summary>
        /// <param name = "equipWeapons" ></ param >
        private void SetupSheathEquipWeapons(EquipWeapons equipWeapons)
        {
            //rightHandWeaponItem = equipWeapons.GetRightHandWeaponItem();
            //leftHandWeaponItem = equipWeapons.GetLeftHandWeaponItem();
            //leftHandShieldItem = equipWeapons.GetLeftHandShieldItem();
        }

        /// <summary>
        /// Checks for Sheath key / button input
        /// </summary>
        protected void PlayerSheathOnUpdate()
        {
            if (!IsOwnerClient || !CurrentGameInstance.enableRatherGoodSheath)
                return;

            if (InputManager.GetButtonDown(CurrentGameInstance.sheathButtonName))
            {
                if (!IsSheathed)
                {
                    CallNetFunction(Cmd_SheathWeapons, FunctionReceivers.All, ObjectId);
                }
                else
                {
                    CallNetFunction(Cmd_UnsheathWeapons, FunctionReceivers.All, ObjectId);
                }
                IsSheathed = !IsSheathed;
            }
        }



        private void Cmd_SheathWeapons(uint playerObjectId)
        {

            if (!Manager.TryGetEntityByObjectId(playerObjectId, out PlayerCharacterEntity player))
            {
                Debug.LogError("Sheath: Player not Found.");
                return;
            }

            SetupSheathEquipWeapons(player.EquipWeapons);

            StartShiethProcess();


        }

        private void Cmd_UnsheathWeapons(uint playerObjectId)
        {

            if (!Manager.TryGetEntityByObjectId(playerObjectId, out PlayerCharacterEntity player))
            {
                Debug.LogError("Sheath: Player not Found.");
                return;
            }

            SetupSheathEquipWeapons(player.EquipWeapons);

            StartShiethProcess();
        }



        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="isInitial"></param>
        /// <param name="isOpen"></param>
        private void OnIsSheathedChange(bool isInitial, bool isOpen)
        {

            if (CurrentGameInstance.disablePitchIKWhenSheithed)
                characterPitchIK.enabled = !isOpen;

            //UpdatePitchIKBasedOnWeaponDamageType();
        }



        void StartShiethProcess()
        {
            if (playableCharacterModel_ForSheath != null)
            {
                playableCharacterModel_ForSheath.StartShiethProcess(IsSheathed);
            }

        }



    }
}




//[SerializeField] DamageType rightHandDamageType;
//[SerializeField] DamageType leftHandDamageType;

//[SerializeField] pitchAdjustSettings defaultPitchAdjustSettings;

//[SerializeField] pitchAdjustSettings meleePitchAdjustSettings;

//[SerializeField] pitchAdjustSettings missilePitchAdjustSettings;
//void UpdatePitchIKBasedOnWeaponDamageType()
//{

//    bool useRight = false;
//    bool useLeft = false;

//    if (rightHandWeaponItem != null)
//    {
//        rightHandDamageType = rightHandWeaponItem.WeaponType.DamageInfo.damageType;
//        useRight = true;
//    }

//    if (leftHandWeaponItem != null)
//    {
//        leftHandDamageType = leftHandWeaponItem.WeaponType.DamageInfo.damageType;
//        useLeft = true;
//    }

//    DamageType damageTypeSwitch = DamageType.Melee;
//    if (useRight)
//        damageTypeSwitch = rightHandDamageType;
//    else if (useLeft)
//        damageTypeSwitch = leftHandDamageType;
//    else
//    {
//        //set defaults
//    }

//    pitchAdjustSettings set = defaultPitchAdjustSettings;

//    switch (damageTypeSwitch)
//    {
//        case DamageType.Melee:
//            set = meleePitchAdjustSettings;
//            break;
//        case DamageType.Missile:
//            set = missilePitchAdjustSettings;
//            break;
//        default:

//            break;
//    }


//    characterPitchIK.axis = set.axis;
//    characterPitchIK.enableWhileStanding = set.enableWhileStanding;
//    characterPitchIK.enableWhileCrouching = set.enableWhileCrouching;
//    characterPitchIK.enableWhileCrawling = set.enableWhileCrawling;
//    characterPitchIK.enableWhileSwiming = set.enableWhileSwiming;
//    characterPitchIK.pitchBone = set.pitchBone;
//    characterPitchIK.rotateOffset = set.rotateOffset;
//    characterPitchIK.inversePitch = set.inversePitch;
//    characterPitchIK.lerpDamping = set.lerpDamping;
//    characterPitchIK.axis = set.axis;
//    characterPitchIK.maxAngle = set.maxAngle;

//}

//[System.Serializable]
//public class pitchAdjustSettings
//{

//    public CharacterPitchIK.Axis axis = CharacterPitchIK.Axis.Z;
//    public bool enableWhileStanding = true;
//    public bool enableWhileCrouching = true;
//    public bool enableWhileCrawling = true;
//    public bool enableWhileSwiming = true;
//    public HumanBodyBones pitchBone = HumanBodyBones.Chest;
//    public Vector3 rotateOffset = new Vector3(125, 0, 0);
//    public bool inversePitch = false;
//    public float lerpDamping = 25f;
//    [Range(0f, 180f)]
//    public float maxAngle = 0f;


//}