using UnityEngine;

namespace Yg.Towers
{
    public class Tower : MonoBehaviour
    {
        [CustomHeader("Tower Settings")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CircleCollider2D _collider;

        public BaseTowerDataSO TowerData { get; private set; }

        public void Initialize(BaseTowerDataSO towerData)
        {
            TowerData = towerData;
            _spriteRenderer.sprite = TowerData.Icon;
            
            _collider = GetComponent<CircleCollider2D>();
            _collider.radius = towerData.Size;
        }
    }
}