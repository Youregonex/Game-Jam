using UnityEngine;

namespace Yg.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        private const string ANIMATOR_PARAMETER_X = "X";
        private const string ANIMATOR_PARAMETER_Y = "Y";

        private Animator _animator;

        private float _positionCheckInterval = .2f;
        private Vector2 _lastPosition;

        private float _currentInterval = 0f;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator component not found on the GameObject.");
            }

            _currentInterval = _positionCheckInterval;
        }

        private void Update()
        {
            _currentInterval -= Time.deltaTime;

            if (_currentInterval <= 0f)
            {
                UpdatePosition();
                _currentInterval = _positionCheckInterval;
            }
        }

        private void UpdatePosition()
        {
            if (_lastPosition == Vector2.zero)
            {
                _lastPosition = transform.position;
                return;
            }

            Vector2 normalizedDirection = ((Vector2)transform.position - _lastPosition).normalized;

            _animator.SetFloat(ANIMATOR_PARAMETER_X, normalizedDirection.x);
            _animator.SetFloat(ANIMATOR_PARAMETER_Y, normalizedDirection.y);

            _lastPosition = transform.position;
        }
    }
}
