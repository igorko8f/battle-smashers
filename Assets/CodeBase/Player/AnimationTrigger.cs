using CodeBase.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Player
{
    public class AnimationTrigger : MonoBehaviour
    {
        [FormerlySerializedAs("playerAnimator")] [SerializeField] protected Animator animator;
        
        private readonly int _movement = Animator.StringToHash("Movement");
        private readonly int _weaponEquip = Animator.StringToHash("WeaponEquip");
        private readonly int _attack = Animator.StringToHash("Attack");

        public virtual void Move(float speed)
        {
            animator.SetFloat(_movement, speed);
        }

        public virtual void EquipWeapon(WeaponType weaponType)
        {
            animator.SetInteger(_weaponEquip, (int)weaponType);
        }

        public virtual void UnEquipWeapon()
        {
            animator.SetInteger(_weaponEquip, 0);
        }

        public virtual void Attack()
        {
            animator.SetTrigger(_attack);
        }
    }
}