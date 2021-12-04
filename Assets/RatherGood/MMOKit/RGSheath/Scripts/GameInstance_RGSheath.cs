using System.Collections;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class GameInstance
    {

        [Header("Rather Good Sheath")]
        [Tooltip("Enable sheathing.")]
        public bool enableRatherGoodSheath = true;

        [Tooltip("If using special PitchIKMgr_RGSheath.")]
        public bool disablePitchIKWhenSheathed = true;

        [Tooltip("For hotkey assignment.")]
        public string sheathButtonName = "Sheath";

    }
}