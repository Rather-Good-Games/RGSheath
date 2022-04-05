using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MultiplayerARPG
{
    //TODO: Better IK Ctrl. This doesn't do a good job of covering all variations
    [RequireComponent(typeof(CharacterPitchIK))]
    public class PitchIKMgr_RGSheath : MonoBehaviour
    {

        CharacterPitchIK characterPitchIK;

        [SerializeField] DamageType rightHandDamageType;
        [SerializeField] DamageType leftHandDamageType;

        [Tooltip("PItch IK settings when sheathed")]
        [SerializeField] pitchAdjustSettings defaultPitchAdjustSettings;

        [Tooltip("PItch IK settings when using mele type weapon")]
        [SerializeField] pitchAdjustSettings meleePitchAdjustSettings;

        [Tooltip("PItch IK settings when using missile type weapon")]
        [SerializeField] pitchAdjustSettings missilePitchAdjustSettings;

        //TODO: Others if needed
        private void Awake()
        {
            characterPitchIK = GetComponent<CharacterPitchIK>();
        }


        public void UpdatePitchIKBasedOnWeaponDamageType(bool isSheathed)
        {

            EquipWeapons equipWeapons = GameInstance.PlayingCharacterEntity?.EquipWeapons;

            if (equipWeapons == null)
                return;

            IWeaponItem rightHandWeaponItem = equipWeapons.GetRightHandWeaponItem();
            IWeaponItem leftHandWeaponItem = equipWeapons.GetLeftHandWeaponItem();
            IShieldItem leftHandShieldItem = equipWeapons.GetLeftHandShieldItem();

            characterPitchIK.enabled = !isSheathed;

            bool useRight = false;
            bool useLeft = false;

            if (rightHandWeaponItem != null)
            {
                rightHandDamageType = rightHandWeaponItem.WeaponType.DamageInfo.damageType;
                useRight = true;
            }

            if (leftHandWeaponItem != null)
            {
                leftHandDamageType = leftHandWeaponItem.WeaponType.DamageInfo.damageType;
                useLeft = true;
            }

            DamageType damageTypeSwitch = DamageType.Melee;
            if (useRight)
                damageTypeSwitch = rightHandDamageType;
            else if (useLeft)
                damageTypeSwitch = leftHandDamageType;
            else
            {
                //TODO: Something better
                //defaults = DamageType.Melee
            }

            pitchAdjustSettings set = defaultPitchAdjustSettings;

            switch (damageTypeSwitch)
            {
                case DamageType.Melee:
                    set = meleePitchAdjustSettings;
                    break;
                case DamageType.Missile:
                    set = missilePitchAdjustSettings;
                    break;
                default:

                    break;
            }

            characterPitchIK.axis = set.axis;
            characterPitchIK.enableWhileStanding = set.enableWhileStanding;
            characterPitchIK.enableWhileCrouching = set.enableWhileCrouching;
            characterPitchIK.enableWhileCrawling = set.enableWhileCrawling;
            characterPitchIK.enableWhileSwiming = set.enableWhileSwiming;
            characterPitchIK.pitchBone = set.pitchBone;
            characterPitchIK.rotateOffset = set.rotateOffset;
            characterPitchIK.inversePitch = set.inversePitch;
            characterPitchIK.lerpDamping = set.lerpDamping;
            characterPitchIK.axis = set.axis;
            characterPitchIK.maxAngle = set.maxAngle;

        }

        [System.Serializable]
        public class pitchAdjustSettings
        {

            public CharacterPitchIK.Axis axis = CharacterPitchIK.Axis.Z;
            public bool enableWhileStanding = true;
            public bool enableWhileCrouching = true;
            public bool enableWhileCrawling = true;
            public bool enableWhileSwiming = true;
            public HumanBodyBones pitchBone = HumanBodyBones.Chest;
            public Vector3 rotateOffset = new Vector3(125, 0, 0);
            public bool inversePitch = false;
            public float lerpDamping = 25f;
            [Range(0f, 180f)]
            public float maxAngle = 0f;


        }


    }
}