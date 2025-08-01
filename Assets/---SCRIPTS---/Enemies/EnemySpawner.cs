using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

namespace Yg.Systems
{
    public class EnemySpawner : MonoBehaviour
    {
        [CustomHeader("Settings")]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _spawnAmount;
        [SerializeField] private float _spawnDelay;

        private Vector3 _spawnPosition;
        private List<Enemy> _enemyList = new();
        private Coroutine _currentSpawnCoroutine;

        private void Start()
        {
            _spawnPosition = PathHolder.Instance.GetInitialSplineContainer().Spline.EvaluatePosition(0f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                SpawnEnemies();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();

            for (int i = 0; i < _enemyList.Count; i++)
                _enemyList[i].OnDeath -= Enemy_OnDeath;
        }

        private void SpawnEnemies()
        {
            if (_currentSpawnCoroutine != null) return;

            StartCoroutine(SpawnEnemiesCoroutine());
        }

        private IEnumerator SpawnEnemiesCoroutine()
        {
            for (int i = 0; i < _spawnAmount; i++)
            {
                Enemy enemy = Instantiate(_enemyPrefab, _spawnPosition, Quaternion.identity);
                _enemyList.Add(enemy);
                enemy.OnDeath += Enemy_OnDeath;

                yield return new WaitForSeconds(_spawnDelay);
            }

            _currentSpawnCoroutine = null;
        }

        private void Enemy_OnDeath(Enemy enemy)
        {
            enemy.OnDeath -= Enemy_OnDeath;
            _enemyList.Remove(enemy);
        }
    }
}
