using CodeBase.Utils;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.Player
{
    public partial class PlayerWeaponsHolder : MonoBehaviour
    {
        [SerializeField] private WeaponPositionsByType[] weaponPositions;

        private PlayerAnimationTrigger _playerAnimationTrigger;
            
        private Weapon _currentWeapon;

        public void Initialize(PlayerAnimationTrigger playerAnimationTrigger)
        {
            _playerAnimationTrigger = playerAnimationTrigger;
        }
        
        public void ClearWeapon()
        {
            _currentWeapon.ClearFieldOfView();
            
            Destroy(_currentWeapon.gameObject);
            _playerAnimationTrigger.UnEquipWeapon();
        }

        public void CollectWeapon(Weapon weapon)
        {
            if(_currentWeapon != null)
                ClearWeapon();
            
            _currentWeapon = weapon;
            _currentWeapon.SetupFieldOfView(transform);

            var weaponParent = GetWeaponPositionByType(_currentWeapon.Type);
            _currentWeapon.transform.SetParent(weaponParent);
            _currentWeapon.transform.ResetChildLocalTransform();

            _playerAnimationTrigger.EquipWeapon(_currentWeapon.Type);
        }

        public void Shot()
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.Shot();
                _playerAnimationTrigger.Attack();
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