using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public WeaponProperty property;
        
        [SerializeField] private FieldOfView fieldOfView;

        public WeaponType Type => 
            property.Type;

        public virtual void Shot()
        {
            
        }
    }
}
