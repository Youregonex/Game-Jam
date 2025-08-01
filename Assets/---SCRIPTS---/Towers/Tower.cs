using System;
using UnityEngine;

namespace Yg.Towers
{
    public class Tower : MonoBehaviour
    {
        [CustomHeader("Tower Settings")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private Projectile _projectilePrefab;

        [CustomHeader("Debug")]
        [SerializeField] private float _currentAttackCooldown = 0f;

        private BaseTowerDataSO _towerData;
        private CircleCollider2D _collider;

        private readonly Collider2D[] _enemyBuffer = new Collider2D[20];
        public void Initialize(BaseTowerDataSO towerData)
        {
            _towerData = towerData;
            _spriteRenderer.sprite = _towerData.Icon;
            
            _collider = GetComponent<CircleCollider2D>();
            _collider.radius = towerData.Size;

            RefreshAttackCooldown();
        }

        private void Update()
        {
            if (_currentAttackCooldown > 0f)
                _currentAttackCooldown -= Time.deltaTime;
            else
                Attack();
        }

        private void Attack()
        {
            RefreshAttackCooldown();

            int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, _towerData.AttackRange, _enemyBuffer, _enemyLayerMask);
            
            if (hitCount == 0) return;

            SortEnemiesByDistance();

            if (_enemyBuffer[0].TryGetComponent(out Enemy enemy))
            {
                Projectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
                projectile.Initialize(enemy, _towerData.Damage);
            }
        }

        private void SortEnemiesByDistance()
        {
            System.Array.Sort(_enemyBuffer, (a, b) =>
            {
                if (a == null) return 1;
                if (b == null) return -1;

                float distA = Vector2.SqrMagnitude((Vector2)a.transform.position - (Vector2)transform.position);
                float distB = Vector2.SqrMagnitude((Vector2)b.transform.position - (Vector2)transform.position);

                return distA.CompareTo(distB);
            });
        }

        private void RefreshAttackCooldown() => _currentAttackCooldown = 1f / _towerData.AttackRate;


        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _towerData.AttackRange);
        }
    }
}