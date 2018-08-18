using UnityEngine;

namespace Assets
{
    public class SoundEvent : MonoBehaviour
    {
        public void PostEventWithGameobject(string soundEvent, GameObject gameObject)
        {
            AkSoundEngine.PostEvent(soundEvent, gameObject);
        }

        public void PostEvent(string soundEvent)
        {
            AkSoundEngine.PostEvent(soundEvent, gameObject);
        }
    }
}
