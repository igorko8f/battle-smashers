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
            Destroy(_currentWeapon.gameObject);
            _playerAnimationTrigger.UnEquipWeapon();
        }

        public void CollectWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;

            var weaponParent = GetWeaponPositionByType(_currentWeapon.Type);
            _currentWeapon.transform.SetParent(weaponParent);
            
            _playerAnimationTrigger.EquipWeapon(_currentWeapon.Type);
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