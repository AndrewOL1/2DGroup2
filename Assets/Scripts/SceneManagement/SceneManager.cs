using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; set; }
        private static SceneList _sceneListObject;
        private static List<string> sceneList = new List<string>();
        [HideInInspector]
        public bool overUI = false;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance != this)
                Destroy(gameObject);
            
        }

        public void LoadNextScene(string sceneName)
        {
            try
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
            catch (System.Exception e)
            {
                Debug.Log("Rebuild Scene object. Tools/get scene list");
            }
        }

        private static void FindSceneObjects()
        {
            SceneTransition[] sceneTransitions = FindObjectsOfType<SceneTransition>();
            foreach (var sceneTransitionObject in sceneTransitions)
            {
                sceneTransitionObject.SetScenes();
            }
        }
#if UNITY_EDITOR
        [MenuItem("Tools/Get Scene List")]
        private static void GetSceneList()
        {
            _sceneListObject= AssetDatabase.LoadAssetAtPath<SceneList>("Assets/Scripts/SceneManagement/SceneList.asset");
            if (_sceneListObject == null)
            {
                _sceneListObject = ScriptableObject.CreateInstance<SceneList>();
                AssetDatabase.CreateAsset(_sceneListObject, "Assets/Scripts/SceneManagement/SceneList.asset");
            }
            _sceneListObject.scenes.Clear();
            foreach (var scenes in EditorBuildSettings.scenes)
            {
                if (!scenes.enabled) continue;
                string sceneName = Path.GetFileNameWithoutExtension(scenes.path);
                _sceneListObject.scenes.Add(sceneName);
                Debug.Log($"Scene Found: {sceneName}");
            }
            EditorUtility.SetDirty(_sceneListObject);
            AssetDatabase.SaveAssets();
            Debug.Log("Scene list saved!");
            FindSceneObjects();
        }
        #endif
    }
}
