using CodeBase.Player;
using CodeBase.Utils;
using UnityEngine;
using Zenject;

namespace CodeBase.Weapons
{
    public class Weapon : Loot
    {
        public WeaponProperty property;
        
        [SerializeField] private FieldOfView fieldOfView;

        private PlayerController _playerController;

        public WeaponType Type => 
            property.Type;

        public override void Initialize() => 
            fieldOfView.Initialize(property.Distance, property.Angle);

        public override void Collect(GameObject to)
        {
            if (to.TryGetComponent(out PlayerController player))
            {
                player.CollectWeapon(this);
            }
        }

        public void SetupFieldOfView(Transform parent)
        {
            fieldOfView.transform.SetParent(parent);
            fieldOfView.transform.ResetChildLocalTransform();
            fieldOfView.ShowFieldOfView();
        }
        
        public void ClearFieldOfView()
        {
            Destroy(fieldOfView.gameObject);
        }
        
        public virtual void Shot()
        {
        }
    }
}
