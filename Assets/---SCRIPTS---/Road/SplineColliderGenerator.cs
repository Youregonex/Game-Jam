using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class SplineColliderGenerator : MonoBehaviour
{
    [SerializeField] private float spacing = 0.1f;
    [SerializeField] private float _colliderRadius = 0.05f;

    private void Start()
    {
        Spline spline = GetComponent<SplineContainer>().Spline;
        float4x4 splineLocalToWorld = float4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        // Calculate length
        float length = SplineUtility.CalculateLength(spline, splineLocalToWorld);

        int segments = Mathf.CeilToInt(length / spacing);

        for (int i = 0; i < segments; i++)
        {
            float t0 = i / (float)segments;
            float t1 = (i + 1) / (float)segments;

            Vector3 p0 = spline.EvaluatePosition(t0);
            Vector3 p1 = spline.EvaluatePosition(t1);
            p0.z = 0f;
            p1.z = 0f;

            var obj = new GameObject($"SegmentCollider_{i}");
            obj.transform.parent = transform;

            CircleCollider2D col = obj.AddComponent<CircleCollider2D>();
            col.radius = _colliderRadius;

            obj.transform.position = (p0 + p1) / 2f;
        }
    }
}
