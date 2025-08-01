using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    [SerializeField] private int sortingOffset = 0;
    [SerializeField] private bool runOnce = true;

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (runOnce)
        {
            Sort();
            enabled = false;
        }
        else
        {
            Sort();
        }
    }
    private void Sort()
    {
        _spriteRenderer.sortingOrder = -(int)(transform.position.y * 100) + sortingOffset;
    }
}
