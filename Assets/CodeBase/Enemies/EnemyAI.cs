using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Player;
using CodeBase.Weapons;
using DG.Tweening;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Enemies
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(CharacterData))]
    [RequireComponent(typeof(EnemyWeaponsHolder))]
    [RequireComponent(typeof(EnemyEffectsTrigger))]
    [RequireComponent(typeof(EnemyAnimationTrigger))]
    public class EnemyAI : MonoBehaviour, IAttackable
    {
        private PlayerController _playerController;
        private LootSpawner _lootSpawner;
        private EnemyFactory _enemyFactory;
        
        private CharacterData _data;
        private EnemyWeaponsHolder _weaponsHolder;
        private EnemyEffectsTrigger _enemyEffects;
        private EnemyAnimationTrigger _enemyAnimation;
        private AIPath _aiPath;

        private Transform _currentTarget = null;
        
        public void Initialize(PlayerController playerController, LootSpawner lootSpawner, EnemyFactory factory)
        {
            _playerController = playerController;
            _lootSpawner = lootSpawner;
            _enemyFactory = factory;

            _aiPath = GetComponent<AIPath>();
            _data = GetComponent<CharacterData>();
            _weaponsHolder = GetComponent<EnemyWeaponsHolder>();
            _enemyEffects = GetComponent<EnemyEffectsTrigger>();
            _enemyAnimation = GetComponent<EnemyAnimationTrigger>();

            _weaponsHolder.Initialize(_enemyAnimation, this);
            StopMoving();
            FindEnemyTarget();
        }

        public void CollectWeapon(Weapon weapon)
        {
            _weaponsHolder.CollectWeapon(weapon);
        }

        private void FindEnemyTarget()
        {
            if (_weaponsHolder.WeaponInEmpty())
            {
                _currentTarget = _lootSpawner.GetNearestLoot(transform.position);
            }
            else
            {
                var distanceToPlayer = Vector3.Distance(transform.position, _playerController.transform.position);
                _currentTarget = _enemyFactory.GetNearestEnemy(this, transform.position, distanceToPlayer, _playerController.transform);
            }

            GoToTarget();
        }

        private void GoToTarget()
        {
            if (_currentTarget == null)
            {
                return;
            }

            _aiPath.canMove = true;
            _aiPath.SetPath(null);
            _aiPath.destination = _currentTarget.position;
            _enemyAnimation.Move(1f);
            
            StartCoroutine(CheckEnemyTargetDistance());
        }

        private IEnumerator CheckEnemyTargetDistance()
        {
            while (true)
            {
                yield return null;
                if (ReachedDestination())
                {
                    StopMoving();
                    FindEnemyTarget();
                    yield break;
                }
            }
        }

        private bool ReachedDestination()
        {
            return _aiPath.reachedEndOfPath && !_aiPath.pathPending;
        }

        private void StopMoving()
        {
            _aiPath.canMove = false;
            _enemyAnimation.Move(0f);
            Attack();
        }

        private void Attack()
        {
            _weaponsHolder.Shot();
        }

        public void Hit()
        {
            _enemyFactory.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}