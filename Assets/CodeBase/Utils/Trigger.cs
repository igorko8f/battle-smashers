using System;
using UnityEngine;

namespace CodeBase.Utils
{
    [RequireComponent(typeof(Collider))]
    public class Trigger : MonoBehaviour
    {
        public event Action<GameObject> TriggerEnterEvent;
        public event Action<GameObject> TriggerExitEvent;

        [SerializeField] private LayerMask objectsLayersToTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (objectsLayersToTrigger == (objectsLayersToTrigger | (1 << other.gameObject.layer)))
            {
                TriggerEnterEvent?.Invoke(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (objectsLayersToTrigger == (objectsLayersToTrigger | (1 << other.gameObject.layer)))
            {
                TriggerExitEvent?.Invoke(other.gameObject);
            }
        }
    }
}