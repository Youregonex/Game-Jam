using UnityEngine;

public class PortalDelayedAnimation : MonoBehaviour
{
    private const string ANIMATION_IDLE = "Idle";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        float randomStart = UnityEngine.Random.Range(0f, 1f);
        _animator.Play(ANIMATION_IDLE, 0, randomStart);
    }
}
