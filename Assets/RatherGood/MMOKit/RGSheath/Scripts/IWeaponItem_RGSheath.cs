using UnityEngine;

namespace MultiplayerARPG
{
    public partial interface IWeaponItem : IEquipmentItem
    {

        EquipmentModel[] RightHandSheithEquipmentModels { get; }

        EquipmentModel[] LeftHandSheithEquipmentModels { get; }

    }
}
