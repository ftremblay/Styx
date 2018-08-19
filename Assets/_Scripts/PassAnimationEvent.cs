
using Styx.Entities;
using Styx.Managers;
using UnityEngine;
using static Styx.Entities.PlayerModule;

namespace Assets._Scripts
{
    public class PassAnimationEvent : MonoBehaviour
    {

        public void Post()
        {
            Debug.Log("Animation event");
        }
    }
}
