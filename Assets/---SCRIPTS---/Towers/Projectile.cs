using UnityEngine;

public class Projectile : MonoBehaviour
{
    [CustomHeader("Settings")]
    [SerializeField] private float _proximityThreshold = .15f;
    [SerializeField] private float _travelSpeed = .1f;

    private Enemy _target;
    private float _damage;

    public void Initialize(Enemy target, float damage)
    {
        _target = target;
        _damage = damage;
    }

    private void Update()
    {
        if (_target == null) Destroy(gameObject);

        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _travelSpeed);

        if (Vector2.Distance(transform.position, _target.transform.position) <= _proximityThreshold)
            DealDamage();
    }

    private void DealDamage()
    {
        _target.TakeDamage(_damage);
        Destroy(gameObject);
    }
}
