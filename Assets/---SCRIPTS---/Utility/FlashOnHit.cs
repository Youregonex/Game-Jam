using UnityEngine;
using System.Collections;

public class FlashOnHit : MonoBehaviour
{
    [CustomHeader("Config")]
    [SerializeField] private Material _flashMaterial;
    [SerializeField] private float _flashDuration = .125f;

    [CustomHeader("Debug Fields")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Material _initialMaterial;

    private Coroutine _flashCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialMaterial = _spriteRenderer.material;
    }

    public void FlashSprite()
    {
        if (_flashCoroutine != null) return;
        StartCoroutine(FlashSpriteCoroutine());
    }

    private IEnumerator FlashSpriteCoroutine()
    {
        _spriteRenderer.material = _flashMaterial;

        yield return new WaitForSeconds(_flashDuration);

        _spriteRenderer.material = _initialMaterial;
        _flashCoroutine = null;
    }
}
