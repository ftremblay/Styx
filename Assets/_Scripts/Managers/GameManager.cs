using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public void Start()
        {
            AkSoundEngine.PostEvent("StartGame", gameObject);
        }
    }
}
