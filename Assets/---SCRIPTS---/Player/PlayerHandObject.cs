using System.Collections.Generic;
using UnityEngine;

namespace Yg.Player
{
    public class PlayerHandObject : MonoBehaviour
    {
        [CustomHeader("Settings")]
        [SerializeField] private Color _canPlaceColor;
        [SerializeField] private Color _canNotPlaceColor;

        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _collider;
        private Camera _mainCamera;

        private readonly HashSet<Collider2D> _collisionSet = new();

        public bool CanPlace
        {
            get
            {
                if (_collisionSet.Count == 0) return true;
                return false;
            }
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<CircleCollider2D>();

            _mainCamera = Camera.main;

            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.position = Utilities.GetMouseWorldPosition();
            _spriteRenderer.color = CanPlace ? _canPlaceColor : _canNotPlaceColor;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_collisionSet.Contains(collision)) return;
            _collisionSet.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_collisionSet.Contains(collision)) _collisionSet.Remove(collision);
        }

        public void SetData(Sprite icon, float radius)
        {
            _spriteRenderer.sprite = icon;
            _collider.radius = radius;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _collider.radius);
        }
    }
}
