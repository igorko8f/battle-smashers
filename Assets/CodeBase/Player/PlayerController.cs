using CodeBase.Input;
using CodeBase.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CharacterData))]
    [RequireComponent(typeof(AnimationTrigger))]
    [RequireComponent(typeof(EffectsTrigger))]
    [RequireComponent(typeof(WeaponsHolder))]

    public class PlayerController : MonoBehaviour, IAttackable
    { 
        private IInputService _inputService;

        private EffectsTrigger _effectsTrigger;
        private AnimationTrigger _animationTrigger;
        private WeaponsHolder _weaponsHolder;
        
        private Rigidbody _rigidbody;
        private CharacterData _playerData;

        private Vector3 _initialRotation;
        private bool _blockInput = false;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _playerData = GetComponent<CharacterData>();
            _rigidbody = GetComponent<Rigidbody>();
            
            _effectsTrigger = GetComponent<EffectsTrigger>();
            _animationTrigger = GetComponent<AnimationTrigger>();
            _weaponsHolder = GetComponent<WeaponsHolder>();
            
            _weaponsHolder.Initialize(_animationTrigger, this);
        }
    
        private void Start()
        {
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

        public void CollectWeapon(Weapon weapon)
        {
            _weaponsHolder.CollectWeapon(weapon);
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
                    _effectsTrigger.PlayEffects();
                }
            }
            else
            {
                _effectsTrigger.StopEffects();
            }
        
            _animationTrigger.Move(direction.magnitude);
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
            _weaponsHolder.Shot();
        }

        public void Hit()
        {
            gameObject.SetActive(false);
        }
    }
}
