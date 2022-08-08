using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterData))]
[RequireComponent(typeof(ObjectsHolder))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private ParticleSystem moveParticles;

    private SimpleJoystick _joystick => SimpleJoystick.Instance;

    private Rigidbody _rigidbody;
    private CharacterData _playerData;
    private ObjectsHolder _objectsHolder;
    
    private Vector3 _initialRotation;
    private AudioSource _audioSource;
    
    private bool _blockInput = false;
    private bool _objectTaken = false;
    
    private readonly int Movement = Animator.StringToHash("Movement");
    private readonly int ObjectsInHands = Animator.StringToHash("ObjectsInHands");

    private void Awake()
    {
        _playerData = GetComponent<CharacterData>();
        _rigidbody = GetComponent<Rigidbody>();
        _objectsHolder = GetComponent<ObjectsHolder>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _objectsHolder.SetCharacterData(_playerData);
        _initialRotation = transform.localEulerAngles;
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
        var horizontal = _joystick.Horizontal();
        var vertical = _joystick.Vertical();
        var direction = new Vector3(horizontal, 0, vertical).normalized;
        var defaultMagnitude = Mathf.Clamp01(new Vector3(horizontal, 0, vertical).magnitude + 0.3f);

        if (direction.magnitude > 0)
        {
            RotateTowardsDirection(direction);
            MoveTowardsDirection(_joystick.ChangePLayerSpeed ? defaultMagnitude : 1);

            if (direction.magnitude > 0.1f)
            {
                PlayEffects();
            }
        }
        else
        {
            StopEffects();
        }
        
        playerAnimator.SetFloat(Movement, direction.magnitude);
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

    private void PlayEffects()
    {
        if (moveParticles.isPlaying == false || moveParticles.isEmitting == false)
        {
            moveParticles.Play();
            if (ES3.Load("SOUND_SAVEKEY", true))
            {
                _audioSource.Play();
            }
        }
    }

    private void StopEffects()
    {
        if (moveParticles.isPlaying || moveParticles.isEmitting)
        {
            moveParticles.Stop();
            _audioSource.Stop();
        }
    }
}
