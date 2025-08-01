using UnityEngine;
using System;

namespace Yg.Towers
{
    [CreateAssetMenu(menuName = "Towers/BaseTowerData", fileName = "BaseTowerData")]
    public class BaseTowerDataSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Tower Prefab { get; private set; }
        [field: SerializeField] public float Size { get; private set; }
        [field: SerializeField] public float Cost { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackRate { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
    }
}
