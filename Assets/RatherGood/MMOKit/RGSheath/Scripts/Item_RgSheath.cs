using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MultiplayerARPG
{

    public partial class Item
    {
        [Header("Right hand sheith models")]
        [Category(100, "RG SHEITH")]

        //[Header("Right hand sheith models")]
        public EquipmentModel[] rightHandSheithEquipmentModels;

        public EquipmentModel[] RightHandSheithEquipmentModels
        {
            get { return rightHandSheithEquipmentModels; }
        }

        [Tooltip("Left Hand Sheith models. Also Shield")]
        public EquipmentModel[] leftHandSheithEquipmentModels;

        public EquipmentModel[] LeftHandSheithEquipmentModels
        {
            get { return leftHandSheithEquipmentModels; }
        }


    }
}
