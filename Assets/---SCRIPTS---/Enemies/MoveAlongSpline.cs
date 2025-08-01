using System;
using UnityEngine;
using UnityEngine.Splines;
using Yg.Systems;

public class MoveAlongSpline : MonoBehaviour
{
    public event Action<MoveAlongSpline> OnTargetReached;

    [CustomHeader("Settings")]
    [SerializeField] private float _loopTime;

    [CustomHeader("Debug")]
    [SerializeField] private float _currentLoopTime;
    [SerializeField] private SplineContainer _currentSplineContainer;

    private bool _targetReached = false;

    public void Initialize(float loopTime)
    {
        _currentSplineContainer = PathHolder.Instance.GetInitialSplineContainer();

        _loopTime = loopTime;
        _currentLoopTime = 0f;
    }
    
    private void Update()
    {
        Movement();
    }

    public void SetNewSpline(SplineContainer splineContainer)
    {
        _currentSplineContainer = splineContainer;
    }

    private void Movement()
    {
        if (_targetReached) return;

        if (_currentLoopTime >= 1f)
        {
            _currentLoopTime = 0f;
            _currentSplineContainer = PathHolder.Instance.GetNextSplineContainer(_currentSplineContainer);

            if (_currentSplineContainer == null)
            {
                OnTargetReached?.Invoke(this);
                _targetReached = true;
                return;
            }
        }

        _currentLoopTime += Time.deltaTime / _loopTime;

        var spline = _currentSplineContainer.Spline;
        float t = Mathf.Clamp01(_currentLoopTime);

        // Local position along the spline
        var localPos = SplineUtility.EvaluatePosition(spline, t);

        // Convert local position to world position
        var worldPos = _currentSplineContainer.transform.TransformPoint(localPos);

        transform.position = worldPos;
    }



}
