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

        public bool disablePitchIKWhenSheathed = true;

        public string sheathButtonName = "Sheath";

    }
}