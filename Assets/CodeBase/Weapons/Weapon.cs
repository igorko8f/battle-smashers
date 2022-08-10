using CodeBase.Enemies;
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
        [SerializeField] private LayerMask enemyLayers;

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
            else if (to.TryGetComponent(out EnemyAI enemy))
            {
                enemy.CollectWeapon(this);
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
        
        public virtual void Shot(IAttackable self)
        {
            var enemies = Physics.OverlapSphere(transform.position, property.Distance, enemyLayers);
            foreach (var enemy in enemies)
            {
                if (enemy.TryGetComponent(out IAttackable attackable))
                {
                    if (self == attackable)
                    {
                        continue;
                    }
                    
                    if(fieldOfView.IsPositionInTheFieldOfView(enemy.transform.position))
                        attackable.Hit();
                }
            }
        }
    }
}
