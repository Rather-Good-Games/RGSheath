using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    //TODO: Something else?
    //Create this partial to implement interface. However, kit doesn't use WeaponItem but need this class to implement IWeaponInterface anyway.

    public partial class WeaponItem : BaseEquipmentItem, IWeaponItem
    {

        [Category(100, "RG SHEATH")]

        [Header("Right hand sheath models")]

        [SerializeField] private EquipmentModel[] rightHandSheathEquipmentModels;
        public EquipmentModel[] RightHandSheathEquipmentModels
        {
            get { return rightHandSheathEquipmentModels; }
            set { rightHandSheathEquipmentModels = value; }
        }


        [Tooltip("Left Hand Sheath models. Also Shield")]
        [SerializeField] private EquipmentModel[] leftHandSheathEquipmentModels;

        public EquipmentModel[] LeftHandSheathEquipmentModels
        {
            get { return leftHandSheathEquipmentModels; }
            set { leftHandSheathEquipmentModels = value; }
        }


    }
}
