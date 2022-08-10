using System;
using System.Collections.Generic;
using CodeBase.Player;
using CodeBase.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemies
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private EnemyAI enemyPrefab;
        [SerializeField] private Transform[] enemySpawnPositions;

        private PlayerController _playerController;
        private LootSpawner _lootSpawner;

        private List<EnemyAI> _enemyList = new List<EnemyAI>();

        [Inject]
        private void Construct(PlayerController playerController, LootSpawner lootSpawner)
        {
            _playerController = playerController;
            _lootSpawner = lootSpawner;
        }

        private void Start()
        {
            _lootSpawner.SpawnInitialLoot(enemySpawnPositions.Length);
            SpawnEnemies();
        }

        public void RemoveEnemy(EnemyAI enemyAI)
        {
            _enemyList.Remove(enemyAI);
        }

        private void SpawnEnemies()
        {
            foreach (var spawnPoint in enemySpawnPositions)
            {
                var enemy = SpawnEnemy(spawnPoint.position);
                _enemyList.Add(enemy);
            }
        }

        private EnemyAI SpawnEnemy(Vector3 position)
        {
            var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.Initialize(_playerController, _lootSpawner, this);
            
            return enemy;
        }

        public Transform GetNearestEnemy(EnemyAI enemyAI, Vector3 enemyPosition, float distanceToPlayer, Transform playerTarget)
        {
            var min = float.MaxValue;
            Transform target = null;

            foreach (var enemy in _enemyList)
            {
                if (enemy == enemyAI)
                {
                    continue;
                }

                var distance = Vector3.Distance(enemyPosition, enemy.transform.position);
                if (distance < min)
                {
                    min = distance;
                    target = enemy.transform;
                }
            }

            if (distanceToPlayer < min)
            {
                target = playerTarget;
            }

            return target;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (var positions in enemySpawnPositions)
            {
                Gizmos.DrawSphere(positions.position , 0.5f);
            }
        }
    }
}