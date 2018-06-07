using UnityEngine;

namespace Assets._Scripts.Views
{
    public class PlayerTransform : MonoBehaviour
    {
        public Transform Player;
        public Transform Stick;

        public void Start()
        {
            if (Player == null)
                Player = GetComponent<Transform>();
        }
    }
}
