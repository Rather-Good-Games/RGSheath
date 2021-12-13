using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG.GameData.Model.Playables
{
    public partial class PlayableCharacterModel_Custom : PlayableCharacterModel
    {
        [ArrayElementTitle("SheathweaponType")]
        public SheathAnimations[] SheathAnimations;

        public void StartShiethProcess(bool isSheathed, EquipWeapons newEquipWeapons = null)
        {
            weaponChangeInProcess = true;

            if (newEquipWeapons == null) //Only sheathing not changing weapons
                newEquipWeapons = equipWeapons;
            StartCoroutine(ShiethProcess(isSheathed, newEquipWeapons));
        }

        public bool weaponChangeInProcess = false;

        //TODO: When changing weapons, put old away before switching to new ones.
        //TODO: Hide/move models between sheath/un-sheath instead of a full destroy, rebuild and re-create
        IEnumerator ShiethProcess(bool isSheathed, EquipWeapons newEquipWeapons)
        {

            SheathAnimations shiethAnimations;
            ActionAnimation actionAnimationToPlay = new ActionAnimation();  //default no clip

            bool hasClip = false;

            if ((newEquipWeapons.GetRightHandWeaponItem() != null) && (newEquipWeapons.GetLeftHandWeaponItem() != null))
            {

                if (TryGetWeaponAnimationsCustom(newEquipWeapons.GetRightHandWeaponItem().WeaponType.DataId, out shiethAnimations))
                {
                    if (isSheathed)
                        actionAnimationToPlay = shiethAnimations.dualWeildSheathAnimations;
                    else
                        actionAnimationToPlay = shiethAnimations.dualWeildUnSheathAnimations;

                    hasClip = true;
                }
            }
            else if (newEquipWeapons.GetRightHandWeaponItem() != null)
            {
                if (TryGetWeaponAnimationsCustom(newEquipWeapons.GetRightHandWeaponItem().WeaponType.DataId, out shiethAnimations))
                {
                    if (isSheathed)
                        actionAnimationToPlay = shiethAnimations.rightHandSheathAnimations;
                    else
                        actionAnimationToPlay = shiethAnimations.rightHandUnSheathAnimations;

                    hasClip = true;
                }
            }
            else if (newEquipWeapons.GetLeftHandWeaponItem() != null)
            {
                if (TryGetWeaponAnimationsCustom(newEquipWeapons.GetLeftHandWeaponItem().WeaponType.DataId, out shiethAnimations))
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
            InstantiateEquipModel3("HiddenWeaponRight");
            InstantiateEquipModel3("HiddenWeaponLeft");
            SetEquipWeapons(newEquipWeapons);

            weaponChangeInProcess = false;

        }

        //Replaces: public override void SetEquipWeapons(EquipWeapons equipWeapons) in PlayableCharacterModel 
        public override void SetEquipWeapons(EquipWeapons newEquipWeapons)
        {

            base.SetEquipWeapons(newEquipWeapons);

            PlayerCharacterEntity pce = this.GetComponent<PlayerCharacterEntity>();

            IEquipmentItem rightHandItem = equipWeapons.GetRightHandEquipmentItem();
            IEquipmentItem leftHandItem = equipWeapons.GetLeftHandEquipmentItem();

            //Set sheathed models instead of normal weapon models
            if (pce.IsSheathed)
            {
                if (rightHandItem != null && rightHandItem.IsWeapon())
                    InstantiateEquipModel3(GameDataConst.EQUIP_POSITION_RIGHT_HAND, rightHandItem.DataId, equipWeapons.rightHand.level, (rightHandItem as IWeaponItem).RightHandSheathEquipmentModels, out rightHandEquipmentEntity);
                if (leftHandItem != null && leftHandItem.IsWeapon())
                    InstantiateEquipModel3(GameDataConst.EQUIP_POSITION_LEFT_HAND, leftHandItem.DataId, equipWeapons.leftHand.level, (leftHandItem as IWeaponItem).LeftHandSheathEquipmentModels, out leftHandEquipmentEntity);
                if (leftHandItem != null && leftHandItem.IsShield())
                    InstantiateEquipModel3(GameDataConst.EQUIP_POSITION_LEFT_HAND, leftHandItem.DataId, equipWeapons.leftHand.level, (leftHandItem as IWeaponItem).LeftHandSheathEquipmentModels, out leftHandEquipmentEntity);

                Behaviour.SetPlayingWeaponTypeId(null);  //Use defaults
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

            SetSecondWeaponSetWeapons();
        }
        public override void SetEquipItems(IList<CharacterItem> equipItems)
        {
            base.SetEquipItems(equipItems);
            SetSecondWeaponSetWeapons();
        }

        public void SetSecondWeaponSetWeapons()
        {
            PlayerCharacterEntity pce = this.GetComponent<PlayerCharacterEntity>();
            if (GameInstance.Singleton.enableRatherGoodSecondWeaponSet)
            {
                IEquipmentItem hiddenRightHandItem;
                IEquipmentItem hiddenLeftHandItem;
                EquipWeapons equipWeaponsHidden;
                if (pce.SelectableWeaponSets.Count == 2)
                    if (pce.SelectableWeaponSets.IndexOf(pce.EquipWeapons) == 0)
                    {
                        hiddenRightHandItem = pce.SelectableWeaponSets[1].GetRightHandEquipmentItem();
                        hiddenLeftHandItem = pce.SelectableWeaponSets[1].GetLeftHandEquipmentItem();
                        equipWeaponsHidden = pce.SelectableWeaponSets[1];

                        if (hiddenRightHandItem != null && hiddenRightHandItem.IsWeapon())
                            InstantiateEquipModel3("HiddenWeaponRight", hiddenRightHandItem.DataId, equipWeaponsHidden.rightHand.level, (hiddenRightHandItem as IWeaponItem).RightHandSheathEquipmentModels, out rightHandEquipmentEntity);
                        if (hiddenLeftHandItem != null && hiddenLeftHandItem.IsWeapon())
                            InstantiateEquipModel3("HiddenWeaponLeft", hiddenLeftHandItem.DataId, equipWeaponsHidden.leftHand.level, (hiddenLeftHandItem as IWeaponItem).LeftHandSheathEquipmentModels, out leftHandEquipmentEntity);
                        if (hiddenLeftHandItem != null && hiddenLeftHandItem.IsShield())
                            InstantiateEquipModel3("HiddenWeaponLeft", hiddenLeftHandItem.DataId, equipWeaponsHidden.leftHand.level, (hiddenLeftHandItem as IWeaponItem).LeftHandSheathEquipmentModels, out leftHandEquipmentEntity);
                    }
                    else if (pce.SelectableWeaponSets.IndexOf(pce.EquipWeapons) == 1)
                    {
                        hiddenRightHandItem = pce.SelectableWeaponSets[0].GetRightHandEquipmentItem();
                        hiddenLeftHandItem = pce.SelectableWeaponSets[0].GetLeftHandEquipmentItem();
                        equipWeaponsHidden = pce.SelectableWeaponSets[0];

                        if (hiddenRightHandItem != null && hiddenRightHandItem.IsWeapon())
                            InstantiateEquipModel3("HiddenWeaponRight", hiddenRightHandItem.DataId, equipWeaponsHidden.rightHand.level, (hiddenRightHandItem as IWeaponItem).RightHandSheathEquipmentModels, out rightHandEquipmentEntity);
                        if (hiddenLeftHandItem != null && hiddenLeftHandItem.IsWeapon())
                            InstantiateEquipModel3("HiddenWeaponLeft", hiddenLeftHandItem.DataId, equipWeaponsHidden.leftHand.level, (hiddenLeftHandItem as IWeaponItem).LeftHandSheathEquipmentModels, out leftHandEquipmentEntity);
                        if (hiddenLeftHandItem != null && hiddenLeftHandItem.IsShield())
                            InstantiateEquipModel3("HiddenWeaponLeft", hiddenLeftHandItem.DataId, equipWeaponsHidden.leftHand.level, (hiddenLeftHandItem as IWeaponItem).LeftHandSheathEquipmentModels, out leftHandEquipmentEntity);

                    }

            }
        }

        public bool TryGetWeaponAnimationsCustom(int dataId, out SheathAnimations anims)
        {
            return CacheAnimationsManagerSheath.SetAndTryGetCacheSheathAnimations(Id, weaponAnimations, skillAnimations, SheathAnimations, dataId, out anims);
        }

        public void InstantiateEquipModel3(string equipPosition)
        {
            if (equipmentEntities.ContainsKey(equipPosition))
            {
                CallDestroyCacheModel(equipPosition);
                equipmentEntities[equipPosition].Clear();
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

            CallDestroyCacheModel(equipPosition);
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
            getCacheModels[equipPosition] = tempInstantiatingModels;

            //Skip this?
            //if (onEquipmentModelsInstantiated != null)
            //    onEquipmentModelsInstantiated.Invoke(equipPosition);
        }


        public static void SetGlobalScale(Transform transform, Vector3 globalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
        }

        public new ActionAnimation[] GetRightHandAttackAnimations(int dataId)
        {
            WeaponAnimations anims;
            if (TryGetWeaponAnimations(dataId, out anims) && anims.rightHandAttackAnimations != null)
                return anims.rightHandAttackAnimations;
            return defaultAnimations.rightHandAttackAnimations;
        }

        public new bool TryGetWeaponAnimations(int dataId, out WeaponAnimations anims)
        {
            return CacheAnimationsManagerSheath.SetAndTryGetCacheWeaponAnimations(Id, weaponAnimations, skillAnimations, SheathAnimations, dataId, out anims);
        }
    }
}
