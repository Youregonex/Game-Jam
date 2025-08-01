using UnityEngine;
using UnityEngine.Splines;

namespace Yg.Misc
{
    [RequireComponent(typeof(SplineContainer))]
    public class SplineCircleAdjuster : MonoBehaviour
    {
        [CustomHeader("Settings")]
        [SerializeField] private Vector2 _splineCenter;
        [SerializeField] private float _radius;
        [SerializeField] private int _totalPoints;

        private SplineContainer _splineContainer;

        private void OnValidate()
        {
            if (_splineContainer == null)
            {
                _splineContainer = GetComponent<SplineContainer>();
            }

            Spline spline = _splineContainer.Spline;
            spline.Clear();

            Vector3 position;

            for (int i = 0; i < _totalPoints; i++)
            {
                float angle = i * Mathf.PI * 2f / _totalPoints;
                float x = _splineCenter.x + _radius * Mathf.Cos(angle);
                float y = _splineCenter.y + _radius * Mathf.Sin(angle);
                position = new Vector3(x, y, 0);

                BezierKnot knot = new(position);
                spline.Add(knot);
            }

            spline.Closed = true;

            for (int i = 0; i < spline.Count; i++)
                spline.SetTangentMode(i, TangentMode.AutoSmooth);           
        }
    }
}
