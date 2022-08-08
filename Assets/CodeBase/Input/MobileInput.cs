using UnityEngine;

namespace CodeBase.Input
{
    public class MobileInput : InputService
    {
        public override Vector3 GetAxis =>
            JoystickAxis();
    }
}