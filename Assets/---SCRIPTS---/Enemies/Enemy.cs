using System;
using UnityEngine;
using Yg.Enemies;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnDeath;

    [CustomHeader("Settings")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private EnemyDataSO _enemyData;
    [SerializeField] private MoveAlongSpline _moveAlongSpline;
    [SerializeField] private FlashOnHit _flashOnHit;

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _enemyData.Health;
        _moveAlongSpline.OnTargetReached += MoveAlongSpline_OnTargetReached;
        _moveAlongSpline.Initialize(_enemyData.LoopTime);
    }

    private void OnDestroy()
    {
        _moveAlongSpline.OnTargetReached -= MoveAlongSpline_OnTargetReached;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _flashOnHit.FlashSprite();
            
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Death();
        }
    }

    private void MoveAlongSpline_OnTargetReached(MoveAlongSpline movementcomponent)
    {
        DealDamage();
    }

    private void DealDamage()
    {
        Debug.Log("Damage dealt");
        Death();
    }

    private void Death()
    {
        Debug.Log("Death");
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
