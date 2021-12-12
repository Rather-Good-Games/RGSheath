using LiteNetLibManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public abstract partial class BasePlayerCharacterEntity
    {
        public bool CallServerSheathWeapon()
        {
            RPC(ServerSheathWeapon);
            return true;
        }

        [ServerRpc]
        protected void ServerSheathWeapon()
        {
#if !CLIENT_BUILD
            if (!CurrentGameInstance.enableRatherGoodSheath)
                return;

            IsSheathed = !IsSheathed;
#endif
        }
    }
}
