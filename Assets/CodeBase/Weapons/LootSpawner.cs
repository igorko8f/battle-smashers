using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.Weapons
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private float lootSpawnRate;
        [SerializeField] private int maximumLootCount;
        [SerializeField] private LayerMask avoidLayers;
        [SerializeField] private Transform spawnZoneMinPoint;
        [SerializeField] private Transform spawnZoneMaxPoint;

        [SerializeField] private LootCollectable collectablePrefab;
        [SerializeField] private Loot[] lootToSpawn;

        private List<LootCollectable> _spawnedLoot = new List<LootCollectable>();

        private Coroutine _spawnLoopRoutine = null;

        private void OnDisable()
        {
            ClearEvents();
        }

        public void SpawnInitialLoot(int lootCount)
        {
            for (int i = 0; i < lootCount; i++)
            {
                SpawnRandomLoot();
            }
            
            StartSpawnLoop();
        }
        
        public void StartSpawnLoop()
        {
            if (_spawnLoopRoutine != null)
            {
                return;
            }

            _spawnLoopRoutine = StartCoroutine(SpawnLootLoop());
        }

        public void StopSpawnLoop()
        {
            if (_spawnLoopRoutine != null)
            {
                StopCoroutine(_spawnLoopRoutine);
            }

            _spawnLoopRoutine = null;
        }

        private IEnumerator SpawnLootLoop()
        {
            var delay = new WaitForSeconds(lootSpawnRate);
            
            while (true)
            {
                yield return delay;
                SpawnRandomLoot();
            }
        }

        public void RemoveLootCollectable(LootCollectable collectable)
        {
            _spawnedLoot.Remove(collectable);
        }

        public Transform GetNearestLoot(Vector3 enemyPosition)
        {
            var min = float.MaxValue;
            Transform loot = null;
            
            foreach (var lootCollectable in _spawnedLoot)
            {
                var distance = Vector3.Distance(enemyPosition, lootCollectable.transform.position);
                if (distance < min)
                {
                    min = distance;
                    loot = lootCollectable.transform;
                }
            }

            return loot;
        }

        private void SpawnRandomLoot()
        {
            if (_spawnedLoot.Count >= maximumLootCount)
            {
                return;
            }

            var randomPoint = GetRandomPosition();
            var lootCollectable = Instantiate(collectablePrefab, randomPoint, Quaternion.identity);
            var randomLoot = lootToSpawn[Random.Range(0, lootToSpawn.Length)];
            var spawnedLoot = Instantiate(randomLoot);

            _spawnedLoot.Add(lootCollectable);
            
            lootCollectable.Initialize(spawnedLoot);
            lootCollectable.LootCollectedEvent += RemoveLootCollectable;
        }

        private Vector3 GetRandomPosition()
        {
            var randomXPosition = Random.Range(spawnZoneMinPoint.position.x, spawnZoneMaxPoint.position.x);
            var randomZPosition = Random.Range(spawnZoneMinPoint.position.z, spawnZoneMaxPoint.position.z);

            var randomPoint = new Vector3(randomXPosition, spawnZoneMinPoint.position.y, randomZPosition);

            var results = new Collider[2];
            var size = Physics.OverlapSphereNonAlloc(randomPoint, 2f, results, avoidLayers);
            
            if (size > 0)
            {
                return GetRandomPosition();
            }

            return randomPoint;
        }

        private void ClearEvents()
        {
            foreach (var lootCollectable in _spawnedLoot)
            {
                lootCollectable.LootCollectedEvent -= RemoveLootCollectable;
            }
        }

        private void OnDrawGizmos()
        {
            var min = spawnZoneMinPoint.position;
            var max = spawnZoneMaxPoint.position;
            var y = min.y;
            
            Gizmos.color = Color.black;
            Gizmos.DrawLine(new Vector3(min.x, y, min.z), new Vector3(min.x, y, max.z));
            Gizmos.DrawLine(new Vector3(min.x, y, max.z), new Vector3(max.x, y, max.z));
            Gizmos.DrawLine(new Vector3(max.x, y, max.z), new Vector3(max.x, y, min.z));
            Gizmos.DrawLine(new Vector3(max.x, y, min.z), new Vector3(min.x, y, min.z));
        }
    }
}