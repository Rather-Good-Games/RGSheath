using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class ShooterPlayerControllerRG : ShooterPlayerCharacterController
    {


        protected override void Update()
        {

            if (CurrentGameInstance.switchControllerModeWhenSheathed && CurrentGameInstance.enableRatherGoodSheath)
            {
                mode = (viewMode == ShooterControllerViewMode.Fps) ? ControllerMode.Combat : ((PlayerCharacterEntity.IsSheathed) ? ControllerMode.Adventure : ControllerMode.Combat);
            }

            base.Update();
        }
    }
}