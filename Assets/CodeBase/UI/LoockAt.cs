using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI
{
    public class LoockAt : MonoBehaviour
    {
        private Transform cameraTransform;

        private void Awake()
        {
            cameraTransform = UnityEngine.Camera.main.transform;
        }

        private void Update()
        {
            if (cameraTransform == null)
            {
                return;
            }

            transform.DOLookAt(cameraTransform.position, Time.deltaTime);
        }
    }
}
