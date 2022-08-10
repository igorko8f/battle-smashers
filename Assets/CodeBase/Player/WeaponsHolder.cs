using CodeBase.Utils;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.Player
{
    public partial class WeaponsHolder : MonoBehaviour
    {
        [SerializeField] private WeaponPositionsByType[] weaponPositions;

        private AnimationTrigger _animationTrigger;
            
        protected Weapon CurrentWeapon;
        protected IAttackable Self;

        public void Initialize(AnimationTrigger animationTrigger, IAttackable self)
        {
            _animationTrigger = animationTrigger;
            Self = self;
        }
        
        public void ClearWeapon()
        {
            CurrentWeapon.ClearFieldOfView();
            
            Destroy(CurrentWeapon.gameObject);
            _animationTrigger.UnEquipWeapon();
        }

        public void CollectWeapon(Weapon weapon)
        {
            if(CurrentWeapon != null)
                ClearWeapon();
            
            CurrentWeapon = weapon;
            CurrentWeapon.SetupFieldOfView(transform);

            var weaponParent = GetWeaponPositionByType(CurrentWeapon.Type);
            CurrentWeapon.transform.SetParent(weaponParent);
            CurrentWeapon.transform.ResetChildLocalTransform();

            _animationTrigger.EquipWeapon(CurrentWeapon.Type);
        }

        public void Shot()
        {
            if (CurrentWeapon != null)
            {
                CurrentWeapon.Shot(Self);
                _animationTrigger.Attack();
            }
                
        }
        
        private Transform GetWeaponPositionByType(WeaponType type)
        {
            foreach (var weaponPosition in weaponPositions)
            {
                var position = weaponPosition.GetWeaponPosition(type);
                if (position != null)
                {
                    return position;
                }
            }

            return null;
        }
    }
}