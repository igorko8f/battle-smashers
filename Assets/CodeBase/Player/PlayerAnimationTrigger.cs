using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        
        private readonly int _movement = Animator.StringToHash("Movement");
        private readonly int _weaponEquip = Animator.StringToHash("WeaponEquip");

        public void Move(float speed)
        {
            playerAnimator.SetFloat(_movement, speed);
        }

        public void EquipWeapon(WeaponType weaponType)
        {
            playerAnimator.SetInteger(_weaponEquip, (int)weaponType);
        }

        public void UnEquipWeapon()
        {
            playerAnimator.SetInteger(_weaponEquip, 0);
        }
    }
}