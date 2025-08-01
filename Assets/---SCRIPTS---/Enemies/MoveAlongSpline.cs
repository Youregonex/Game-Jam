using System;
using UnityEngine;
using UnityEngine.Splines;
using Yg.Systems;

public class MoveAlongSpline : MonoBehaviour
{
    public event Action<MoveAlongSpline> OnTargetReached;

    [CustomHeader("Debug")]
    [SerializeField] private float _splineT;
    [SerializeField] private SplineContainer _currentSplineContainer;

    private float _loopTime;
    private float _splineLength;
    private bool _targetReached;

    public void Initialize(float loopTime)
    {
        _loopTime = loopTime;
        _splineT = 0f;
        _targetReached = false;

        _currentSplineContainer = PathHolder.Instance.GetInitialSplineContainer();
        if (_currentSplineContainer == null)
        {
            Debug.LogError("Initial spline container is null.");
            return;
        }

        _splineLength = SplineUtility.CalculateLength(
            _currentSplineContainer.Spline,
            _currentSplineContainer.transform.localToWorldMatrix
        );
    }

    private void Update()
    {
        MoveAlong();
    }

    public void SetNewSpline(SplineContainer splineContainer)
    {
        _currentSplineContainer = splineContainer;
        _splineT = 0f;

        _splineLength = SplineUtility.CalculateLength(
            _currentSplineContainer.Spline,
            _currentSplineContainer.transform.localToWorldMatrix
        );
    }

    private void MoveAlong()
    {
        if (_targetReached || _currentSplineContainer == null) return;

        float speed = 1f / _loopTime; // move t from 0 to 1 over loopTime
        _splineT += Time.deltaTime * speed;

        if (_splineT >= 1f)
        {
            _splineT = 0f;

            _currentSplineContainer = PathHolder.Instance.GetNextSplineContainer(_currentSplineContainer);

            if (_currentSplineContainer == null)
            {
                OnTargetReached?.Invoke(this);
                _targetReached = true;
                return;
            }

            _splineLength = SplineUtility.CalculateLength(
                _currentSplineContainer.Spline,
                _currentSplineContainer.transform.localToWorldMatrix
            );
        }

        // Evaluate world position at current t
        Vector3 localPos = SplineUtility.EvaluatePosition(_currentSplineContainer.Spline, _splineT);
        Vector3 worldPos = _currentSplineContainer.transform.TransformPoint(localPos);

        transform.position = worldPos;
    }
}
