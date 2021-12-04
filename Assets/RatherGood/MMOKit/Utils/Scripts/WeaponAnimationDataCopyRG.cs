using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG.GameData.Model.Playables
{

    /// <summary>
    /// Helper class that holds animation data ina  scriptable object for copy covienece. long arrays in editor can't view on character. Unity bug/conveinence class
    /// </summary>

    [CreateAssetMenu(fileName = "WeaponAnimationDataCopyRG", menuName = "RatherGoodGames/WeaponAnimationDataCopyRG", order = 1)]
    public class WeaponAnimationDataCopyRG : ScriptableObject
    {
        #region LocalStuffToCopy


        [Header("Use ToCopy to/from animations on Character")]

        public WeaponType weaponType;

        [Header("Movements while standing")]
        public AnimState idleState;
        public MoveStates moveStates;
        public MoveStates sprintStates;
        public MoveStates walkStates;

        [Header("Movements while crouching")]
        public AnimState crouchIdleState;
        public MoveStates crouchMoveStates;

        [Header("Movements while crawling")]
        public AnimState crawlIdleState;
        public MoveStates crawlMoveStates;

        [Header("Movements while swimming")]
        public AnimState swimIdleState;
        public MoveStates swimMoveStates;

        [Header("Jump")]
        public AnimState jumpState;

        [Header("Fall")]
        public AnimState fallState;

        [Header("Landed")]
        public AnimState landedState;

        [Header("Hurt")]
        public ActionState hurtState;

        [Header("Dead")]
        public AnimState deadState;

        [Header("Pickup")]
        public ActionState pickupState;

        [Header("Attack animations")]
        public ActionState rightHandChargeState;
        public ActionState leftHandChargeState;

        [ArrayElementTitle("clip")]
        public ActionAnimation[] rightHandAttackAnimations;
        [ArrayElementTitle("clip")]
        public ActionAnimation[] leftHandAttackAnimations;

        [Header("Reload(Gun) animations")]
        public ActionAnimation rightHandReloadAnimation;
        public ActionAnimation leftHandReloadAnimation;


        [Header("Sheath animations for weapons")]

        public ActionAnimation rightHandSheathAnimations;
        public ActionAnimation rightHandUnSheathAnimations;

        public ActionAnimation leftHandSheathAnimations;
        public ActionAnimation leftHandUnSheathAnimations;

        public ActionAnimation dualWeildSheathAnimations;
        public ActionAnimation dualWeildUnSheathAnimations;

        #endregion LocalStuffToCopy

        #region Buttons

        [InspectorButton(nameof(AddFromHereToPCMArray))]
        public bool addFromHereToPCMArray = false;
        public void AddFromHereToPCMArray()
        {
            AddFromHereToPCMArrayF();
        }

        [Header("PlayableCharacterModel Array")]
        public int arrayIndexToCopyFrom = 0;


        [InspectorButton(nameof(CopyFromPCMArrayIndexToLocal))]
        public bool copyFromPCMArrayIndexToLocal = false;
        public void CopyFromPCMArrayIndexToLocal()
        {
            CopyFromPCMArrayIndexToLocalF();
        }

        [InspectorButton(nameof(ClearWeaponAnimationsList))]
        public bool clearWeaponAnimationsList = false;
        public void ClearWeaponAnimationsList()
        {
            weaponAnimations = new WeaponAnimations[0];
        }

        [ArrayElementTitle("weaponType")]
        public WeaponAnimations[] weaponAnimations;

        [Header("AnimatorCharacterModel Array")]

        public int arrayOLDIndexToCopyFrom = 0;

        [InspectorButton(nameof(CopyToLocalFromACMArrayIndex))]
        public bool copyToLocalFromACMArrayIndex = false;
        public void CopyToLocalFromACMArrayIndex()
        {
            CopyToLocalFromACMArrayIndexF();
        }

        [ArrayElementTitle("weaponType")]
        public MultiplayerARPG.WeaponAnimations[] weaponAnimationsAnimatorCharacterModel;

        [Tooltip("Use me to copy stuff then throw me away.")]
        public MultiplayerARPG.WeaponAnimations tempWeaponAnimationsAnimatorCharacterModel;

        public void CopyToLocalFromACMArrayIndexF()
        {

            if (weaponAnimationsAnimatorCharacterModel == null)
                return;

            if (arrayOLDIndexToCopyFrom < 0 || arrayOLDIndexToCopyFrom > weaponAnimationsAnimatorCharacterModel.Length - 1)
                return;

            CopyFromToHere(ConvertToPlayableWeaponAnimations(weaponAnimationsAnimatorCharacterModel[arrayOLDIndexToCopyFrom]));

        }



        #endregion Buttons

        #region PCMCopyFunctions

        public void AddFromHereToPCMArrayF()
        {
            if (weaponAnimations == null)
                weaponAnimations = new WeaponAnimations[0];

            Array.Resize<WeaponAnimations>(ref weaponAnimations, weaponAnimations.Length + 1);

            WeaponAnimations newAnim = new WeaponAnimations()
            {
                weaponType = weaponType,
                idleState = idleState,
                moveStates = moveStates,
                sprintStates = sprintStates,
                walkStates = walkStates,
                crouchIdleState = crouchIdleState,
                crouchMoveStates = crouchMoveStates,
                crawlIdleState = crawlIdleState,
                crawlMoveStates = crawlMoveStates,
                swimIdleState = swimIdleState,
                swimMoveStates = swimMoveStates,
                jumpState = jumpState,
                fallState = fallState,
                landedState = landedState,

                hurtState = hurtState,
                deadState = deadState,
                pickupState = pickupState,
                rightHandChargeState = rightHandChargeState,
                leftHandChargeState = leftHandChargeState,
                rightHandAttackAnimations = rightHandAttackAnimations,
                leftHandAttackAnimations = leftHandAttackAnimations,
                rightHandReloadAnimation = rightHandReloadAnimation,
                leftHandReloadAnimation = leftHandReloadAnimation,

                //Sheath

                rightHandSheathAnimations = rightHandSheathAnimations,
                rightHandUnSheathAnimations = rightHandUnSheathAnimations,

                leftHandSheathAnimations = leftHandSheathAnimations,
                leftHandUnSheathAnimations = leftHandUnSheathAnimations,

                dualWeildSheathAnimations = dualWeildSheathAnimations,
                dualWeildUnSheathAnimations = dualWeildUnSheathAnimations,

            };


            weaponAnimations[weaponAnimations.Length - 1] = newAnim;

        }


        public void CopyFromPCMArrayIndexToLocalF()
        {
            if (weaponAnimations == null)
                return;

            if (arrayIndexToCopyFrom < 0 || arrayIndexToCopyFrom > weaponAnimations.Length - 1)
                return;

            WeaponAnimations getAnim = weaponAnimations[arrayIndexToCopyFrom];

            CopyFromToHere(getAnim);

        }


        void CopyFromToHere(WeaponAnimations getAnim)
        {

            weaponType = getAnim.weaponType;
            idleState = getAnim.idleState;
            moveStates = getAnim.moveStates;
            sprintStates = getAnim.sprintStates;
            walkStates = getAnim.walkStates;
            crouchIdleState = getAnim.crouchIdleState;
            crouchMoveStates = getAnim.crouchMoveStates;
            crawlIdleState = getAnim.crawlIdleState;
            crawlMoveStates = getAnim.crawlMoveStates;
            swimIdleState = getAnim.swimIdleState;
            swimMoveStates = getAnim.swimMoveStates;
            jumpState = getAnim.jumpState;
            fallState = getAnim.fallState;
            landedState = getAnim.landedState;

            hurtState = getAnim.hurtState;
            deadState = getAnim.deadState;
            pickupState = getAnim.pickupState;
            rightHandChargeState = getAnim.rightHandChargeState;
            leftHandChargeState = getAnim.leftHandChargeState;
            rightHandAttackAnimations = getAnim.rightHandAttackAnimations;
            leftHandAttackAnimations = getAnim.leftHandAttackAnimations;
            rightHandReloadAnimation = getAnim.rightHandReloadAnimation;
            leftHandReloadAnimation = getAnim.leftHandReloadAnimation;

            //Sheath

            rightHandSheathAnimations = getAnim.rightHandSheathAnimations;
            rightHandUnSheathAnimations = getAnim.rightHandUnSheathAnimations;

            leftHandSheathAnimations = getAnim.leftHandSheathAnimations;
            leftHandUnSheathAnimations = getAnim.leftHandUnSheathAnimations;

            dualWeildSheathAnimations = getAnim.dualWeildSheathAnimations;
            dualWeildUnSheathAnimations = getAnim.dualWeildUnSheathAnimations;

        }



        #endregion PCMCopyFunctions

        #region ACMCopyFunctions


        private Playables.WeaponAnimations ConvertToPlayableWeaponAnimations(MultiplayerARPG.WeaponAnimations oldWeaponAnim)
        {

            List<MultiplayerARPG.ActionAnimation> oldLeftHandAttackAnims = new List<MultiplayerARPG.ActionAnimation>();
            List<Playables.ActionAnimation> newLeftHandAttackAnims = new List<Playables.ActionAnimation>();
            List<MultiplayerARPG.ActionAnimation> oldRightHandAttackAnims = new List<MultiplayerARPG.ActionAnimation>();
            List<Playables.ActionAnimation> newRightHandAttackAnims = new List<Playables.ActionAnimation>();


            // Prepare left-hand attack animations
            oldLeftHandAttackAnims.Clear();
            oldLeftHandAttackAnims.AddRange(oldWeaponAnim.leftHandAttackAnimations);
            newLeftHandAttackAnims.Clear();
            foreach (MultiplayerARPG.ActionAnimation oldLeftHandAttackAnim in oldLeftHandAttackAnims)
            {
                newLeftHandAttackAnims.Add(ConvertToPlayableActionAnimation(oldLeftHandAttackAnim));
            }
            // Prepare right-hand attack animations
            oldRightHandAttackAnims.Clear();
            oldRightHandAttackAnims.AddRange(oldWeaponAnim.rightHandAttackAnimations);
            newRightHandAttackAnims.Clear();
            foreach (MultiplayerARPG.ActionAnimation oldRightHandAttackAnim in oldRightHandAttackAnims)
            {
                newRightHandAttackAnims.Add(ConvertToPlayableActionAnimation(oldRightHandAttackAnim));
            }
            return new Playables.WeaponAnimations()
            {
                weaponType = oldWeaponAnim.weaponType,
                idleState = new Playables.AnimState()
                {
                    clip = oldWeaponAnim.idleClip,
                    animSpeedRate = oldWeaponAnim.idleAnimSpeedRate,
                },
                crouchIdleState = new Playables.AnimState()
                {
                    clip = oldWeaponAnim.crouchIdleClip,
                    animSpeedRate = oldWeaponAnim.crouchIdleAnimSpeedRate,
                },
                crawlIdleState = new Playables.AnimState()
                {
                    clip = oldWeaponAnim.crawlIdleClip,
                    animSpeedRate = oldWeaponAnim.crawlIdleAnimSpeedRate,
                },
                swimIdleState = new Playables.AnimState()
                {
                    clip = oldWeaponAnim.swimIdleClip,
                    animSpeedRate = oldWeaponAnim.swimIdleAnimSpeedRate,
                },
                jumpState = new Playables.AnimState()
                {
                    clip = oldWeaponAnim.jumpClip,
                    animSpeedRate = oldWeaponAnim.jumpAnimSpeedRate,
                },
                fallState = new Playables.AnimState()
                {
                    clip = oldWeaponAnim.fallClip,
                    animSpeedRate = oldWeaponAnim.fallAnimSpeedRate,
                },
                landedState = new Playables.AnimState()
                {
                    clip = oldWeaponAnim.landedClip,
                    animSpeedRate = oldWeaponAnim.landedAnimSpeedRate,
                },
                hurtState = new Playables.ActionState()
                {
                    clip = oldWeaponAnim.hurtClip,
                    animSpeedRate = oldWeaponAnim.hurtAnimSpeedRate,
                },
                deadState = new Playables.AnimState()
                {
                    clip = oldWeaponAnim.deadClip,
                    animSpeedRate = oldWeaponAnim.deadAnimSpeedRate,
                },
                pickupState = new Playables.ActionState()
                {
                    clip = oldWeaponAnim.pickupClip,
                    animSpeedRate = oldWeaponAnim.pickupAnimSpeedRate,
                },
                rightHandChargeState = new Playables.ActionState()
                {
                    clip = oldWeaponAnim.rightHandChargeClip,
                    animSpeedRate = 1,
                },
                leftHandChargeState = new Playables.ActionState()
                {
                    clip = oldWeaponAnim.leftHandChargeClip,
                    animSpeedRate = 1,
                },
                rightHandReloadAnimation = ConvertToPlayableActionAnimation(oldWeaponAnim.rightHandReloadAnimation),
                leftHandReloadAnimation = ConvertToPlayableActionAnimation(oldWeaponAnim.leftHandReloadAnimation),
                rightHandAttackAnimations = newRightHandAttackAnims.ToArray(),
                leftHandAttackAnimations = newLeftHandAttackAnims.ToArray(),
                moveStates = new Playables.MoveStates()
                {
                    forwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.moveClip,
                        animSpeedRate = oldWeaponAnim.moveAnimSpeedRate,
                    },
                    backwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.moveBackwardClip,
                        animSpeedRate = oldWeaponAnim.moveAnimSpeedRate,
                    },
                    leftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.moveLeftClip,
                        animSpeedRate = oldWeaponAnim.moveAnimSpeedRate,
                    },
                    rightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.moveRightClip,
                        animSpeedRate = oldWeaponAnim.moveAnimSpeedRate,
                    },
                    forwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.moveForwardLeftClip,
                        animSpeedRate = oldWeaponAnim.moveAnimSpeedRate,
                    },
                    forwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.moveForwardRightClip,
                        animSpeedRate = oldWeaponAnim.moveAnimSpeedRate,
                    },
                    backwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.moveBackwardLeftClip,
                        animSpeedRate = oldWeaponAnim.moveAnimSpeedRate,
                    },
                    backwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.moveBackwardRightClip,
                        animSpeedRate = oldWeaponAnim.moveAnimSpeedRate,
                    },
                },
                sprintStates = new Playables.MoveStates()
                {
                    forwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.sprintClip,
                        animSpeedRate = oldWeaponAnim.sprintAnimSpeedRate,
                    },
                    backwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.sprintBackwardClip,
                        animSpeedRate = oldWeaponAnim.sprintAnimSpeedRate,
                    },
                    leftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.sprintLeftClip,
                        animSpeedRate = oldWeaponAnim.sprintAnimSpeedRate,
                    },
                    rightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.sprintRightClip,
                        animSpeedRate = oldWeaponAnim.sprintAnimSpeedRate,
                    },
                    forwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.sprintForwardLeftClip,
                        animSpeedRate = oldWeaponAnim.sprintAnimSpeedRate,
                    },
                    forwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.sprintForwardRightClip,
                        animSpeedRate = oldWeaponAnim.sprintAnimSpeedRate,
                    },
                    backwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.sprintBackwardLeftClip,
                        animSpeedRate = oldWeaponAnim.sprintAnimSpeedRate,
                    },
                    backwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.sprintBackwardRightClip,
                        animSpeedRate = oldWeaponAnim.sprintAnimSpeedRate,
                    },
                },
                walkStates = new Playables.MoveStates()
                {
                    forwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.walkClip,
                        animSpeedRate = oldWeaponAnim.walkAnimSpeedRate,
                    },
                    backwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.walkBackwardClip,
                        animSpeedRate = oldWeaponAnim.walkAnimSpeedRate,
                    },
                    leftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.walkLeftClip,
                        animSpeedRate = oldWeaponAnim.walkAnimSpeedRate,
                    },
                    rightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.walkRightClip,
                        animSpeedRate = oldWeaponAnim.walkAnimSpeedRate,
                    },
                    forwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.walkForwardLeftClip,
                        animSpeedRate = oldWeaponAnim.walkAnimSpeedRate,
                    },
                    forwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.walkForwardRightClip,
                        animSpeedRate = oldWeaponAnim.walkAnimSpeedRate,
                    },
                    backwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.walkBackwardLeftClip,
                        animSpeedRate = oldWeaponAnim.walkAnimSpeedRate,
                    },
                    backwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.walkBackwardRightClip,
                        animSpeedRate = oldWeaponAnim.walkAnimSpeedRate,
                    },
                },
                crouchMoveStates = new Playables.MoveStates()
                {
                    forwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crouchMoveClip,
                        animSpeedRate = oldWeaponAnim.crouchMoveAnimSpeedRate,
                    },
                    backwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crouchMoveBackwardClip,
                        animSpeedRate = oldWeaponAnim.crouchMoveAnimSpeedRate,
                    },
                    leftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crouchMoveLeftClip,
                        animSpeedRate = oldWeaponAnim.crouchMoveAnimSpeedRate,
                    },
                    rightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crouchMoveRightClip,
                        animSpeedRate = oldWeaponAnim.crouchMoveAnimSpeedRate,
                    },
                    forwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crouchMoveForwardLeftClip,
                        animSpeedRate = oldWeaponAnim.crouchMoveAnimSpeedRate,
                    },
                    forwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crouchMoveForwardRightClip,
                        animSpeedRate = oldWeaponAnim.crouchMoveAnimSpeedRate,
                    },
                    backwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crouchMoveBackwardLeftClip,
                        animSpeedRate = oldWeaponAnim.crouchMoveAnimSpeedRate,
                    },
                    backwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crouchMoveBackwardRightClip,
                        animSpeedRate = oldWeaponAnim.crouchMoveAnimSpeedRate,
                    },
                },
                crawlMoveStates = new Playables.MoveStates()
                {
                    forwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crawlMoveClip,
                        animSpeedRate = oldWeaponAnim.crawlMoveAnimSpeedRate,
                    },
                    backwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crawlMoveBackwardClip,
                        animSpeedRate = oldWeaponAnim.crawlMoveAnimSpeedRate,
                    },
                    leftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crawlMoveLeftClip,
                        animSpeedRate = oldWeaponAnim.crawlMoveAnimSpeedRate,
                    },
                    rightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crawlMoveRightClip,
                        animSpeedRate = oldWeaponAnim.crawlMoveAnimSpeedRate,
                    },
                    forwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crawlMoveForwardLeftClip,
                        animSpeedRate = oldWeaponAnim.crawlMoveAnimSpeedRate,
                    },
                    forwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crawlMoveForwardRightClip,
                        animSpeedRate = oldWeaponAnim.crawlMoveAnimSpeedRate,
                    },
                    backwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crawlMoveBackwardLeftClip,
                        animSpeedRate = oldWeaponAnim.crawlMoveAnimSpeedRate,
                    },
                    backwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.crawlMoveBackwardRightClip,
                        animSpeedRate = oldWeaponAnim.crawlMoveAnimSpeedRate,
                    },
                },
                swimMoveStates = new Playables.MoveStates()
                {
                    forwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.swimMoveClip,
                        animSpeedRate = oldWeaponAnim.swimMoveAnimSpeedRate,
                    },
                    backwardState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.swimMoveBackwardClip,
                        animSpeedRate = oldWeaponAnim.swimMoveAnimSpeedRate,
                    },
                    leftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.swimMoveLeftClip,
                        animSpeedRate = oldWeaponAnim.swimMoveAnimSpeedRate,
                    },
                    rightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.swimMoveRightClip,
                        animSpeedRate = oldWeaponAnim.swimMoveAnimSpeedRate,
                    },
                    forwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.swimMoveForwardLeftClip,
                        animSpeedRate = oldWeaponAnim.swimMoveAnimSpeedRate,
                    },
                    forwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.swimMoveForwardRightClip,
                        animSpeedRate = oldWeaponAnim.swimMoveAnimSpeedRate,
                    },
                    backwardLeftState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.swimMoveBackwardLeftClip,
                        animSpeedRate = oldWeaponAnim.swimMoveAnimSpeedRate,
                    },
                    backwardRightState = new Playables.AnimState()
                    {
                        clip = oldWeaponAnim.swimMoveBackwardRightClip,
                        animSpeedRate = oldWeaponAnim.swimMoveAnimSpeedRate,
                    },
                }
            };
        }

        private Playables.ActionAnimation ConvertToPlayableActionAnimation(MultiplayerARPG.ActionAnimation actionAnimation)
        {
            return new Playables.ActionAnimation()
            {
                state = new Playables.ActionState()
                {
                    clip = actionAnimation.clip,
                    animSpeedRate = actionAnimation.animSpeedRate,
                },
                triggerDurationRates = actionAnimation.multiHitTriggerDurationRates == null || actionAnimation.multiHitTriggerDurationRates.Length == 0 ? new float[] { actionAnimation.triggerDurationRate } : actionAnimation.multiHitTriggerDurationRates,
                durationType = actionAnimation.durationType,
                fixedDuration = actionAnimation.fixedDuration,
                extendDuration = actionAnimation.extendDuration,
                audioClips = actionAnimation.audioClips,
            };
        }

        #endregion ACMCopyFunctions

    }
}