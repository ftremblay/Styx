using RageCure.ReduxUtils;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public Store<GameState> Store;

        public void Start()
        {
            //TODO: Finish experimenting with Redux pattern
            //Store = Store<GameState>.CreateStore(null, GetComponent<GameState>());
            AkSoundEngine.PostEvent("StartGame", gameObject);
        }

        #region Singleton
        private static GameManager Instance;

        public void Awake()
        {
            Instance = this;
        }
        #endregion
    }

    public class GameState : ScriptableObject
    {

    }
}
