using CodeBase.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Weapons
{
    [CreateAssetMenu(fileName = "weaponProperty", menuName = "SO/Weapons/WeaponProperty", order = 0)]
    public class WeaponProperty : ScriptableObject
    {
        [Title("Weapon Type")]
        public WeaponType Type;
        [ShowIf(nameof(Type), WeaponType.Distance)] public Bullet Bullet;

        [Title("Field Of View")] 
        public float Distance = 1f;
        public float Angle = 1f;
    }
}