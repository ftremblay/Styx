using UnityEngine;

namespace Assets._Scripts.Utils.Sound
{
    public class WwiseSoundEvent : MonoBehaviour {

	    public void Post (string soundEvent)
	    {
            if (string.IsNullOrEmpty(soundEvent))
                return;

		    AkSoundEngine.PostEvent(soundEvent, gameObject);
		    Debug.Log ("PrintEvent: " + soundEvent + " Called at: " + Time.time);
	    }
    }
}
