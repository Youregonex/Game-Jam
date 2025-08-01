using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

namespace Yg.Systems
{
    public class PathHolder : MonoBehaviour
    {
        public static PathHolder Instance { get; private set; }

        [CustomHeader("Settings")]
        [SerializeField] private SplineContainer _initialSpline;
        [SerializeField] private List<SplineContainer> _splinePathList;

        private void Awake()
        {
            if (Instance != null) Destroy(Instance);

            Instance = this;
        }

        public SplineContainer GetInitialSplineContainer()
        {
            return _initialSpline;
        }

        public SplineContainer GetNextSplineContainer(SplineContainer splineContainer)
        {
            int index;
            
            if (splineContainer == _initialSpline)
                index = _splinePathList.Count;
            else
                index = _splinePathList.IndexOf(splineContainer);

            return index == 0 ? null : _splinePathList[index - 1];
        }
    }
}
