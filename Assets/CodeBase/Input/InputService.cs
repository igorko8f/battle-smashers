using System;
using UnityEngine;

namespace CodeBase.Input
{
    public abstract class InputService : IInputService
    {
        public bool ChangePlayerSpeed =>
            SimpleJoystick.Instance.ChangePLayerSpeed;

        public abstract Vector3 GetAxis { get; }

        public event Action OnPlayerAttack;
        
        protected Vector3 JoystickAxis()
        {
            return new Vector3(SimpleJoystick.Instance.Horizontal(), 0f, SimpleJoystick.Instance.Vertical());
        }
    }
}