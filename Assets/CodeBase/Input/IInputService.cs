using System;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Input
{
    public interface IInputService : IService
    {
        bool ChangePlayerSpeed { get; }
        Vector3 GetAxis { get; }
        event Action OnPlayerAttack;
    }
}