using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MultiplayerARPG.GameData.Model.Playables
{
    public partial class PlayableCharacterModel_Custom : PlayableCharacterModel
    {


        protected bool isSheathed;
        public void StartShiethProcess(bool isSheathed)
        {
            this.isSheathed = isSheathed;

            StartCoroutine(ShiethProcess());
        }


        //TODO: When changing weapons for put old away before switching to new ones.
        //TODO: Hide/move models between sheath/un-sheath instead of a full rebuild destroy and re-create
        IEnumerator ShiethProcess()
        {

            IWeaponItem rightHandWeaponItem = equipWeapons.GetRightHandWeaponItem();
            IWeaponItem leftHandWeaponItem = equipWeapons.GetLeftHandWeaponItem();
            //TODO: Sheild
            //IShieldItem leftHandShieldItem;

            WeaponAnimations shiethAnimations;
            ActionAnimation actionAnimationToPlay = new ActionAnimation();  //default no clip

            bool hasClip = false;

            if ((rightHandWeaponItem != null) && (leftHandWeaponItem != null))
            {

                if (TryGetWeaponAnimations(rightHandWeaponItem.WeaponType.DataId, out shiethAnimations))
                {
                    if (isSheathed)
                        actionAnimationToPlay = shiethAnimations.dualWeildSheathAnimations;
                    else
                        actionAnimationToPlay = shiethAnimations.dualWeildUnSheathAnimations;

                    hasClip = true;
                }
            }
            else if (rightHandWeaponItem != null)
            {
                if (TryGetWeaponAnimations(rightHandWeaponItem.WeaponType.DataId, out shiethAnimations))
                {
                    if (isSheathed)
                        actionAnimationToPlay = shiethAnimations.rightHandSheathAnimations;
                    else
                        actionAnimationToPlay = shiethAnimations.rightHandUnSheathAnimations;

                    hasClip = true;
                }
            }
            else if (leftHandWeaponItem != null)
            {
                if (TryGetWeaponAnimations(leftHandWeaponItem.WeaponType.DataId, out shiethAnimations))
                {
                    if (isSheathed)
                        actionAnimationToPlay = shiethAnimations.leftHandSheathAnimations;
                    else
                        actionAnimationToPlay = shiethAnimations.leftHandUnSheathAnimations;

                    hasClip = true;
                }
            }


            if (hasClip && (actionAnimationToPlay.state.clip != null))
            {
                PlayActionAnimationDirectly(actionAnimationToPlay);

                //At trigger weapons will switch.GetTriggerDurations will always return 0.5 if not initialized   
                yield return new WaitForSeconds(actionAnimationToPlay.GetTriggerDurations()[0]);
            }

            //Switch to new weapons after duration 
            SetEquipWeapons(equipWeapons);

        }

        //Replaces: public override void SetEquipWeapons(EquipWeapons equipWeapons) in PlayableCharacterModel 
        public override void SetEquipWeapons(EquipWeapons equipWeapons)
        {

            //********* copy from BaseCharacterModel******

            this.equipWeapons = equipWeapons;

            IEquipmentItem rightHandItem = equipWeapons.GetRightHandEquipmentItem();
            IEquipmentItem leftHandItem = equipWeapons.GetLeftHandEquipmentItem();

            // Clear equipped item models
            tempAddingKeys.Clear();
            if (rightHandItem != null)
                tempAddingKeys.Add(GameDataConst.EQUIP_POSITION_RIGHT_HAND);
            if (leftHandItem != null)
                tempAddingKeys.Add(GameDataConst.EQUIP_POSITION_LEFT_HAND);

            tempCachedKeys.Clear();
            tempCachedKeys.AddRange(cacheModels.Keys);  //set protected            //protected readonly Dictionary<string, Dictionary<string, GameObject>> cacheModels = new Dictionary<string, Dictionary<string, GameObject>>();
            foreach (string equipPosition in tempCachedKeys)
            {
                // Destroy cache model by the position which not existed in new equipment position (unequipped items)
                //if (!tempAddingKeys.Contains(equipPosition) &&
                //    (equipPosition.Equals(GameDataConst.EQUIP_POSITION_RIGHT_HAND) ||
                //    equipPosition.Equals(GameDataConst.EQUIP_POSITION_LEFT_HAND)))
                //DestroyCacheModel(equipPosition); //set "protected void DestroyCacheModel(string equipPosition)"

                if ((equipPosition.Equals(GameDataConst.EQUIP_POSITION_RIGHT_HAND) ||
                  equipPosition.Equals(GameDataConst.EQUIP_POSITION_LEFT_HAND)))
                    DestroyCacheModel(equipPosition); //set "protected void DestroyCacheModel(string equipPosition)"
            }

            //Set sheathed models instead of normal weapon models
            if (isSheathed)
            {
                if (rightHandItem != null && rightHandItem.IsWeapon())
                    InstantiateEquipModel3(GameDataConst.EQUIP_POSITION_RIGHT_HAND, rightHandItem.DataId, equipWeapons.rightHand.level, (rightHandItem as IWeaponItem).RightHandSheathEquipmentModels, out rightHandEquipmentEntity);
                if (leftHandItem != null && leftHandItem.IsWeapon())
                    InstantiateEquipModel3(GameDataConst.EQUIP_POSITION_LEFT_HAND, leftHandItem.DataId, equipWeapons.leftHand.level, (leftHandItem as IWeaponItem).LeftHandSheathEquipmentModels, out leftHandEquipmentEntity);
                if (leftHandItem != null && leftHandItem.IsShield())
                    InstantiateEquipModel3(GameDataConst.EQUIP_POSITION_LEFT_HAND, leftHandItem.DataId, equipWeapons.leftHand.level, (leftHandItem as IWeaponItem).LeftHandSheathEquipmentModels, out leftHandEquipmentEntity);

                //HACK, TODO: 
                Behaviour.SetPlayingWeaponTypeId(null);  //Use defaults?
                //Behaviour.currentWeaponTypeId = weaponAnimations[0].weaponType.Id;

                equippedWeaponType = null;
            }
            else
            {
                if (rightHandItem != null && rightHandItem.IsWeapon())
                    InstantiateEquipModel3(GameDataConst.EQUIP_POSITION_RIGHT_HAND, rightHandItem.DataId, equipWeapons.rightHand.level, rightHandItem.EquipmentModels, out rightHandEquipmentEntity);
                if (leftHandItem != null && leftHandItem.IsWeapon())
                    InstantiateEquipModel3(GameDataConst.EQUIP_POSITION_LEFT_HAND, leftHandItem.DataId, equipWeapons.leftHand.level, (leftHandItem as IWeaponItem).OffHandEquipmentModels, out leftHandEquipmentEntity);
                if (leftHandItem != null && leftHandItem.IsShield())
                    InstantiateEquipModel3(GameDataConst.EQUIP_POSITION_LEFT_HAND, leftHandItem.DataId, equipWeapons.leftHand.level, leftHandItem.EquipmentModels, out leftHandEquipmentEntity);

                IWeaponItem weaponItem = equipWeapons.GetRightHandWeaponItem();
                if (weaponItem == null)
                    weaponItem = equipWeapons.GetLeftHandWeaponItem();
                // Set equipped weapon type, it will be used to get animations by id
                equippedWeaponType = null;
                if (weaponItem != null)
                    equippedWeaponType = weaponItem.WeaponType;
                if (Behaviour != null)
                    Behaviour.SetPlayingWeaponTypeId(weaponItem);

            }
        }

        public void InstantiateEquipModel3(string equipPosition, int itemDataId, int itemLevel, EquipmentModel[] equipmentModels, out BaseEquipmentEntity foundEquipmentEntity)
        {
            foundEquipmentEntity = null;

            if (!equipmentEntities.ContainsKey(equipPosition))
                equipmentEntities.Add(equipPosition, new List<BaseEquipmentEntity>());

            // Temp variables
            int i;
            GameObject tempEquipmentObject;
            BaseEquipmentEntity tempEquipmentEntity;

            //Always destroy and recreate

            DestroyCacheModel(equipPosition);
            itemIds[equipPosition] = itemDataId;
            equipmentEntities[equipPosition].Clear();

            if (equipmentModels == null || equipmentModels.Length == 0)
                return;

            Dictionary<string, GameObject> tempInstantiatingModels = new Dictionary<string, GameObject>();
            EquipmentContainer tempContainer;
            EquipmentModel tempEquipmentModel;
            for (i = 0; i < equipmentModels.Length; ++i)
            {
                tempEquipmentModel = equipmentModels[i];
                if (string.IsNullOrEmpty(tempEquipmentModel.equipSocket))
                    continue;
                if (!CacheEquipmentModelContainers.TryGetValue(tempEquipmentModel.equipSocket, out tempContainer))
                    continue;
                if (tempEquipmentModel.useInstantiatedObject)
                {
                    // Activate the instantiated object
                    if (!tempContainer.ActivateInstantiatedObject(tempEquipmentModel.instantiatedObjectIndex))
                        continue;
                    tempContainer.SetActiveDefaultModel(false);
                    tempEquipmentObject = tempContainer.instantiatedObjects[tempEquipmentModel.instantiatedObjectIndex];
                    tempEquipmentEntity = tempEquipmentObject.GetComponent<BaseEquipmentEntity>();
                    tempInstantiatingModels.Add(tempEquipmentModel.equipSocket, null);
                }
                else
                {
                    if (tempEquipmentModel.model == null)
                        continue;
                    // Instantiate model, setup transform and activate game object
                    tempContainer.DeactivateInstantiatedObjects();
                    tempContainer.SetActiveDefaultModel(false);
                    tempEquipmentObject = Instantiate(tempEquipmentModel.model, tempContainer.transform);
                    tempEquipmentObject.transform.localPosition = tempEquipmentModel.localPosition;
                    tempEquipmentObject.transform.localEulerAngles = tempEquipmentModel.localEulerAngles;

                    //Use global scale?
                    SetGlobalScale(tempEquipmentObject.transform, (tempEquipmentModel.localScale.Equals(Vector3.zero) ? Vector3.one : tempEquipmentModel.localScale));

                    //tempEquipmentObject.transform.localScale = tempEquipmentModel.localScale.Equals(Vector3.zero) ? Vector3.one : tempEquipmentModel.localScale;

                    tempEquipmentObject.transform.localScale = tempEquipmentModel.localScale.Equals(Vector3.zero) ? Vector3.one : tempEquipmentModel.localScale;
                    tempEquipmentObject.gameObject.SetActive(true);
                    tempEquipmentObject.gameObject.SetLayerRecursively(CurrentGameInstance.characterLayer.LayerIndex, true);
                    tempEquipmentObject.RemoveComponentsInChildren<Collider>(false);
                    tempEquipmentEntity = tempEquipmentObject.GetComponent<BaseEquipmentEntity>();
                    AddingNewModel(tempEquipmentObject, tempContainer);
                    tempInstantiatingModels.Add(tempEquipmentModel.equipSocket, tempEquipmentObject);
                }
                // Setup equipment entity (if exists)
                if (tempEquipmentEntity != null)
                {
                    tempEquipmentEntity.Setup(this, equipPosition, itemLevel);
                    equipmentEntities[equipPosition].Add(tempEquipmentEntity);
                    if (foundEquipmentEntity == null)
                        foundEquipmentEntity = tempEquipmentEntity;
                }
            }
            // Cache Models
            cacheModels[equipPosition] = tempInstantiatingModels;

            //Skip this?
            //if (onEquipmentModelsInstantiated != null)
            //    onEquipmentModelsInstantiated.Invoke(equipPosition);
        }


        public static void SetGlobalScale(Transform transform, Vector3 globalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
        }


        //TODO: Try to fix weapon charge clip not using transition duration (For bow pull with 0.003 S duration animation. Still not exatly right here. )
        public override void PlayWeaponChargeClip(int dataId, bool isLeftHand)
        {
            isDoingAction = true;
            WeaponAnimations weaponAnimations;
            if (TryGetWeaponAnimations(dataId, out weaponAnimations))
            {
                if (isLeftHand && weaponAnimations.leftHandChargeState.clip != null)
                {
                    Behaviour.PlayAction(weaponAnimations.leftHandChargeState, weaponAnimations.leftHandChargeState.animSpeedRate, weaponAnimations.leftHandChargeState.transitionDuration);
                    return;
                }
                if (!isLeftHand && weaponAnimations.rightHandChargeState.clip != null)
                {
                    Behaviour.PlayAction(weaponAnimations.rightHandChargeState, weaponAnimations.rightHandChargeState.animSpeedRate, weaponAnimations.rightHandChargeState.transitionDuration);
                    return;
                }
            }

            //if (isLeftHand)
            //    Behaviour.PlayAction(defaultAnimations.leftHandChargeState, weaponAnimations.leftHandChargeState.animSpeedRate, weaponAnimations.leftHandChargeState.transitionDuration);
            //else
            //    Behaviour.PlayAction(defaultAnimations.rightHandChargeState, weaponAnimations.rightHandChargeState.animSpeedRate, weaponAnimations.rightHandChargeState.transitionDuration);
        }

    }
}

