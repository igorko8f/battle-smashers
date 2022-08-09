using System;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class LootCollectable : MonoBehaviour
    {
        public event Action<LootCollectable> LootCollectedEvent;
        
        [SerializeField] private Transform visualParent;
        [SerializeField] private Trigger trigger;

        private Loot _currentLoot;
        
        public void Initialize(Loot loot)
        {
            _currentLoot = loot;
            _currentLoot.Initialize();
            _currentLoot.transform.SetParent(visualParent);
            _currentLoot.transform.ResetChildLocalTransform();

            trigger.TriggerEnterEvent += OnEnterTrigger;
        }

        private void DestroyLoot()
        {
            trigger.TriggerEnterEvent -= OnEnterTrigger;

            _currentLoot = null;
            
            Destroy(gameObject);
        }

        private void OnEnterTrigger(GameObject triggeredObject)
        {
            GiveLoot(triggeredObject);
        }

        private void GiveLoot(GameObject triggeredObject)
        {
            _currentLoot.Collect(triggeredObject);
            LootCollectedEvent?.Invoke(this);
            
            DestroyLoot();
        }
    }
}