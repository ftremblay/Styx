using Assets._Scripts.States;
using RageCure.EventUtils;
using RageCure.ReduxUtils;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class GameManager : EventAggregator
    {
        //public Store<GameModel, GameAction> Store;

        public void Start()
        {
            AkSoundEngine.PostEvent("StartGame", gameObject);
        }

        #region Singleton
        public static GameManager Instance;

        public void Awake()
        {
            Instance = this;
        }

        #endregion
    }
}
