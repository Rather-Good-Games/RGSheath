using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    //Create this partial to implement interface. However this class seems to be unused in kit

    public partial class WeaponItem : BaseEquipmentItem, IWeaponItem
    {


        [Header("Right hand sheith models")]
        [Category(100, "RG SHEITH")]
        [SerializeField]
        private EquipmentModel[] rightHandSheithEquipmentModels;
        public EquipmentModel[] RightHandSheithEquipmentModels
        {
            get { return rightHandSheithEquipmentModels; }
            set { rightHandSheithEquipmentModels = value; }
        }


        [Tooltip("Left Hand Sheith models. Also Shield")]
        public EquipmentModel[] leftHandSheithEquipmentModels;

        public EquipmentModel[] LeftHandSheithEquipmentModels
        {
            get { return leftHandSheithEquipmentModels; }
            set { leftHandSheithEquipmentModels = value; }
        }


    }
}
