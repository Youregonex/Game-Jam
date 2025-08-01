using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

namespace Yg.Systems
{
    public class PathHolder : MonoBehaviour
    {
        [CustomHeader("Settings")]
        [SerializeField] private List<SplineContainer> _pathList;



    }
}
