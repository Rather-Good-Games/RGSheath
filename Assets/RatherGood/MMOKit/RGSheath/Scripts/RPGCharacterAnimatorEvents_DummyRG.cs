using UnityEngine;
using UnityEngine.Events;



namespace RatherGood.DummyAnimEvents
{
    /// <summary>
    /// Place on character animator to supress errors from spamming the console.
    /// TODO: HACK: Class catches animator events form Explosive RPG character and Emerald AI and does nothng with them. 
    /// Really should delete the events. PITA can't update assets.
    /// </summary>
    public class RPGCharacterAnimatorEvents_DummyRG : MonoBehaviour
    {

        [Header("Dummy animator event catchers for RPG character. (later delete or something) ")]
        public UnityEvent OnHit = new UnityEvent();
        public UnityEvent OnShoot = new UnityEvent();
        public UnityEvent OnFootR = new UnityEvent();
        public UnityEvent OnFootL = new UnityEvent();
        public UnityEvent OnLand = new UnityEvent();
        public UnityEvent OnWeaponSwitch = new UnityEvent();

        [Header("Emerald AI Dummy animator event catchers for events added to support. (later delete or something) ")]
        public UnityEvent OnEmeraldAttackEvent = new UnityEvent();

        public UnityEvent OnPlayWarningSound = new UnityEvent();

        public UnityEvent OnPlayAttackSound = new UnityEvent();

        public UnityEvent OnPlayDeathSound = new UnityEvent();

        public UnityEvent OnEquipWeapon = new UnityEvent();

        public UnityEvent OnUnEquipWeapon = new UnityEvent();

        [Header("Dummy other events useless. (later delete or something) ")]
        public UnityEvent OnWalkFootstepSound = new UnityEvent();

        //public AnimatorMoveEvent OnMove = new AnimatorMoveEvent();

        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void WalkFootstepSound()
        {
            //OnWalkFootstepSound.Invoke();
        }
        public void Hit()
        {
            //OnHit.Invoke();
        }

        public void Shoot()
        {
            // OnShoot.Invoke();
        }

        public void FootR()
        {
            //OnFootR.Invoke();
        }

        public void FootL()
        {
            //OnFootL.Invoke();
        }

        public void Land()
        {
            //OnLand.Invoke();
        }

        public void WeaponSwitch()
        {
            //OnWeaponSwitch.Invoke();
        }
        public void EquipWeapon()
        {
            OnEquipWeapon.Invoke();
        }

        public void UnequipWeapon()
        {
            OnUnEquipWeapon.Invoke();
        }

        public void EmeraldAttackEvent()
        {
            OnEmeraldAttackEvent.Invoke();
        }



    }
}