using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

namespace Yg.Systems
{ 
    public class DayNightChanger : MonoBehaviour
    {
        [CustomHeader("Settings")]
        [SerializeField] private Light2D _globalLight;
        [SerializeField] private Gradient _lightGradient;

        [CustomHeader("DOTween Settings")]
        [SerializeField] private float _cycleChangeDuration = 2f;

        private EDayCycle _currentCycle;

        private Coroutine _currentCoroutine;
        private Tween _currentTween;

        private float t = 0f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                ChangeDayCycle();
        }

        public void ChangeDayCycle()
        {
            StartCoroutine(ChangeDayCycleCoroutine());
        }

        private IEnumerator ChangeDayCycleCoroutine()
        {
            if (_currentCoroutine != null) yield break;

            switch(_currentCycle)
            {
                case EDayCycle.Day:
                    CycleChangeAnimation(1f);
                    ChangeCurrentCycle(EDayCycle.Night);
                    break;

                case EDayCycle.Night:
                    CycleChangeAnimation(0f);
                    ChangeCurrentCycle(EDayCycle.Day);
                    break;

                default: break;
            }           

            yield return _currentTween;
            _currentCoroutine = null;
        }

        private void CycleChangeAnimation(float targetValue)
        {
            _currentTween = DOTween
                .To(
                () => t,
                x =>
                {
                    t = x;
                    _globalLight.color = _lightGradient.Evaluate(t);
                },
                targetValue,
                _cycleChangeDuration)
                .OnComplete(() => _currentTween = null);
        }

        private void ChangeCurrentCycle(EDayCycle currentCycle)
        {
            _currentCycle = currentCycle;
        }
    }
}
