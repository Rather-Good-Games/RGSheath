using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public abstract partial class BaseCharacterModel
    {
        protected Dictionary<string, Dictionary<string, GameObject>> getCacheModels
        {
            get { return cacheModels; }   // get method
        }

        protected void CallDestroyCacheModel(string equipPosition)
        {
            DestroyCacheModel(equipPosition);
        }
    }
}
