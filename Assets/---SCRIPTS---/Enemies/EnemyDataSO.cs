using UnityEngine;

namespace Yg.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/EnemyDataSO")]
    public class EnemyDataSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public float LoopTime { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
    }
}
