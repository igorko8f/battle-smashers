using CodeBase.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Camera
{
    public class CameraInitializer : MonoBehaviour
    {
        private Cinemachine.CinemachineVirtualCamera _virtualCamera;

        [Inject]
        private void Construct(PlayerController controller)
        {
            _virtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
            var playerTransform = controller.transform;
        
            _virtualCamera.Follow = playerTransform;
            _virtualCamera.LookAt = playerTransform;
        }
    }
}
