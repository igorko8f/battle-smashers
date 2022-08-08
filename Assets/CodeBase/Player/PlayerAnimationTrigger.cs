using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        
        private readonly int _movement = Animator.StringToHash("Movement");
        private readonly int _weaponEquip = Animator.StringToHash("WeaponEquip");
        private readonly int _attack = Animator.StringToHash("Attack");

        public virtual void Move(float speed)
        {
            playerAnimator.SetFloat(_movement, speed);
        }

        public virtual void EquipWeapon(WeaponType weaponType)
        {
            playerAnimator.SetInteger(_weaponEquip, (int)weaponType);
        }

        public virtual void UnEquipWeapon()
        {
            playerAnimator.SetInteger(_weaponEquip, 0);
        }

        public virtual void Attack(WeaponType weaponType)
        {
            playerAnimator.SetInteger(_attack, (int)weaponType);
        }
    }
}