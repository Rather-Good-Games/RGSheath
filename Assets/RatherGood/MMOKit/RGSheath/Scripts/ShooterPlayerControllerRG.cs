using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{

    //RGSheath mod leace combat mode when sheathed

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