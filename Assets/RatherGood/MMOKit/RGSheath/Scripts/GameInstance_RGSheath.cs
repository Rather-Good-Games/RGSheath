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
        [Tooltip("Enable Emotes. Removes update and chat checks if false.")]
        public bool enableRatherGoodSheath = true;

        public bool disablePitchIKWhenSheithed = true;

        public string sheathButtonName = "Sheath";

    }
}