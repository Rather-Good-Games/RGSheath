using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MultiplayerARPG
{
    public class ShooterPlayerControllerRG : ShooterPlayerCharacterController
    {
        //TODO: Combat mode when charging weapon all movement slow except forward sprint. Movement looks odd.

        [Header("RG Debug me")]
        public bool switchControllerModeWhenSheathed = true;

        protected override void Update()
        {
            if (switchControllerModeWhenSheathed && CurrentGameInstance.enableRatherGoodSheath)
            {
                if (PlayerCharacterEntity.IsSheathed)
                    mode = ControllerMode.Adventure;
                else
                    mode = ControllerMode.Combat;
            }

            base.Update();
        }
    }
}