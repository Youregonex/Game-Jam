using UnityEngine;
using Yg.Towers;

namespace Yg.Player
{
    public class PlayerTowerPlacer : MonoBehaviour
    {
        [CustomHeader("Settings")]
        [SerializeField] private PlayerHandObject _handObject;
        [SerializeField] private Tower _towerPrefab;
        [SerializeField] private Transform _towerParent;

        [CustomHeader("Debug")]
        [SerializeField] private BaseTowerDataSO _testData;

        private BaseTowerDataSO _currentTowerData;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                TryPlaceTower();

            if (Input.GetKeyDown(KeyCode.Mouse1))
                ResetData();

            if (Input.GetKeyDown(KeyCode.X))            
                SetCurrentTowerData(_testData);            
        }

        private void SetCurrentTowerData(BaseTowerDataSO towerData)
        {
            _currentTowerData = towerData;
            EnableHandObject();
        }

        private void TryPlaceTower()
        {
            if (_currentTowerData == null || !_handObject.CanPlace) return;

            Tower tower = Instantiate(_towerPrefab, _handObject.transform.position, Quaternion.identity, _towerParent);
            tower.Initialize(_currentTowerData);
            ResetData();
        }

        private void ResetData()
        {
            _currentTowerData = null;
            _handObject.gameObject.SetActive(false);
        }
        private void EnableHandObject()
        {
            _handObject.transform.position = Utilities.GetMouseWorldPosition();
            _handObject.gameObject.SetActive(true);
            _handObject.SetData(_currentTowerData.Icon, _currentTowerData.Size);
        }
    }
}
