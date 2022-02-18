﻿using System.Collections;
using UnityEngine;
using System.Collections.Generic;


namespace MultiplayerARPG
{
    public partial class GameInstance
    {

        [Header("Rather Good Sheath")]
        [Tooltip("Enable sheathing.")]
        public bool enableRatherGoodSheath = true;

        [Tooltip("Enable 2nd EquipWeaponset Show on back")]
        public bool enableRatherGoodSecondWeaponSet = true;

        [Tooltip("Weapons Sheated in CharacterSelect")]
        public bool enableCharacterSelectSheath = true;

        [Tooltip("If using special PitchIKMgr_RGSheath.")]
        public bool disablePitchIKWhenSheathed = true;

        [Tooltip("For ShooterPlayerControllerRG Mod will switch \"Adventure\" when sheathed and \"Conbat\" when fighting or FPV.")]
        public bool switchControllerModeWhenSheathed = true;

        [Tooltip("For hotkey assignment.")]
        public string sheathButtonName = "Sheath";

    }
}