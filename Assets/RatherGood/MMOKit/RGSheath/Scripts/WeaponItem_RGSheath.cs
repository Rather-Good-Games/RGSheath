using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    //Create this partial to implement interface. However this class seems to be unused in kit

    public partial class WeaponItem : BaseEquipmentItem, IWeaponItem
    {


        [Header("Right hand sheath models")]
        [Category(100, "RG SHEATH")]
        [SerializeField]
        private EquipmentModel[] rightHandSheathEquipmentModels;
        public EquipmentModel[] RightHandSheathEquipmentModels
        {
            get { return rightHandSheathEquipmentModels; }
            set { rightHandSheathEquipmentModels = value; }
        }


        [Tooltip("Left Hand Sheath models. Also Shield")]
        public EquipmentModel[] leftHandSheathEquipmentModels;

        public EquipmentModel[] LeftHandSheathEquipmentModels
        {
            get { return leftHandSheathEquipmentModels; }
            set { leftHandSheathEquipmentModels = value; }
        }


    }
}
