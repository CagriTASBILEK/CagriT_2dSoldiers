using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            InitializeGame();
        }
        private void InitializeGame()
        {
            GridManager.Instance.Initialize(); 
            FactoryManager.Initialize();
        }
    }
}
