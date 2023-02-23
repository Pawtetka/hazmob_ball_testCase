using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class ScenesLoader : MonoBehaviour
    {
        private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
        
        #region Singleton
        public static ScenesLoader Instance = null;
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            //If an instance already exists, destroy whatever this object is to enforce the singleton.
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            DontDestroyOnLoad (gameObject);
        }
        #endregion

        public void LoadScene(Scenes scene)
        {
            scenesToLoad.Add(SceneManager.LoadSceneAsync((int)scene));
        }

        public void LoadSceneAdditive(Scenes scene)
        {
            scenesToLoad.Add(SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive));
        }
    }
}