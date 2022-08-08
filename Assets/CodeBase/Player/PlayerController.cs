using System;
using CodeBase.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CharacterData))]
    [RequireComponent(typeof(ObjectsHolder))]
    [RequireComponent(typeof(PlayerAnimationTrigger))]
    [RequireComponent(typeof(PlayerEffectsTrigger))]

    public class PlayerController : MonoBehaviour
    { 
        private IInputService _inputService;

        private PlayerEffectsTrigger _playerEffectsTrigger;
        private PlayerAnimationTrigger _playerAnimationTrigger;
        
        private Rigidbody _rigidbody;
        private CharacterData _playerData;
        private ObjectsHolder _objectsHolder;
    
        private Vector3 _initialRotation;

        private bool _blockInput = false;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _playerData = GetComponent<CharacterData>();
            _rigidbody = GetComponent<Rigidbody>();
            _objectsHolder = GetComponent<ObjectsHolder>();
            _playerEffectsTrigger = GetComponent<PlayerEffectsTrigger>();
            _playerAnimationTrigger = GetComponent<PlayerAnimationTrigger>();
        }
    
        private void Start()
        {
            _objectsHolder.SetCharacterData(_playerData);
            _initialRotation = transform.localEulerAngles;
            
            _inputService.OnPlayerAttack += Attack;
        }

        private void OnDisable()
        {
            _inputService.OnPlayerAttack -= Attack;
        }

        private void FixedUpdate()
        {
            if (_blockInput)
            {
                return;
            }

            Move();
        }

        private void Move()
        {
            var axis = _inputService.GetAxis;
            var direction = axis.normalized;
            var defaultMagnitude = Mathf.Clamp01(axis.magnitude + 0.3f);

            if (direction.magnitude > 0)
            {
                RotateTowardsDirection(direction);
                MoveTowardsDirection(_inputService.ChangePlayerSpeed ? defaultMagnitude : 1);

                if (direction.magnitude > 0.1f)
                {
                    _playerEffectsTrigger.PlayEffects();
                }
            }
            else
            {
                _playerEffectsTrigger.StopEffects();
            }
        
            _playerAnimationTrigger.Move(direction.magnitude);
        }

        private void RotateTowardsDirection(Vector3 direction)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.localEulerAngles = _initialRotation + new Vector3(0f, targetAngle, 0f);
        }
    
        private void MoveTowardsDirection(float magnitude)
        {
            var positionToMove = _rigidbody.position + (transform.forward * magnitude * _playerData.MovementSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(positionToMove);
        }

        private void Attack()
        {
            
        }
    }
}
