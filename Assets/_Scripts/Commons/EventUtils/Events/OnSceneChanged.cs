using System;
using UnityEngine.SceneManagement;

namespace RageCure.EventUtils.Events
{
    public class OnSceneChanged
    {
        public string SceneName { get; private set; }

        public static OnSceneChanged Create(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName) == null)
                throw new Exception("SceneChangedMessage : scene index is out of range");

            return new OnSceneChanged(sceneName);
        }

        private OnSceneChanged(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}
