using UnityEngine;

namespace CodeBase.Input
{
    public class StandaloneInput : InputService
    {
        public override Vector3 GetAxis
        {
            get
            {
                var axis = JoystickAxis();
                if (axis == Vector3.zero)
                {
                    axis = UnityAxis();
                }

                return axis;
            }
        }
        
        private Vector3 UnityAxis()
        {
            return new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0f, UnityEngine.Input.GetAxis("Vertical"));
        }
    }
}