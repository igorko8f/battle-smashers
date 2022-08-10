using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Player
{
    public partial class WeaponsHolder
    {
        [System.Serializable]
        private struct WeaponPositionsByType
        {
            [SerializeField] private WeaponType type;
            [SerializeField] private Transform position;

            public Transform GetWeaponPosition(WeaponType weaponType)
            {
                return weaponType == type ? position : null;
            }
        }
    }
}