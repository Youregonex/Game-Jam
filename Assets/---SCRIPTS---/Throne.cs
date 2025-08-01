using System;
using UnityEngine;

public class Throne : MonoBehaviour
{
    private const string ANIMATION_HIT = "HIT";
    public static Throne Instance { get; private set; }

    [CustomHeader("Settings")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private Animator _animator;

    private float _currentHealth;
    private bool _isDead = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of Throne detected. Destroying the new instance.");
            Destroy(gameObject);
        }

        _currentHealth = _health;
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        Debug.Log($"Throne took {damage} damage, health left: {_currentHealth}");
            
        _animator.SetTrigger(ANIMATION_HIT);

        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            _isDead = true;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Loss");
    }
}
