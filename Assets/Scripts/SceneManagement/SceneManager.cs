using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagement
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; set; }
        private static SceneList _sceneListObject;
        private static List<string> sceneList = new List<string>();
        [HideInInspector]
        public bool overUI = false;
        [SerializeField] private Image blackBackground;
        private Color _background;
        [SerializeField] private float fadeDuration;
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

        private void Start()
        {
            blackBackground.color= new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, 1);
            FadeIn(fadeDuration);
        }

        public void LoadNextScene(string sceneName)
        {
            try
            {
                FadeIn(fadeDuration);
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
        public void FadeIn(float t)
        {
            StartCoroutine(IFadeIn(t));
        }
        public void FadeOut(float t)
        {
            StartCoroutine(IFadeOut(t));
        }
        private IEnumerator IFadeOut(float t)
        {
            float alpha = blackBackground.color.a;
            Color fadeColor = blackBackground.color;
            while (alpha < 1)
            {
                yield return new WaitForSeconds(0.01f);
                alpha += 1 / (t * 60);
                fadeColor.a = alpha;
                blackBackground.color = fadeColor;
            }
        }

        private IEnumerator FadeInDelay()
        {
            float temp = fadeDuration;
            while (temp>0f)
            {
                yield return new WaitForSeconds(0.01f);
                temp -= 1 / (temp * 60);
            }

            IFadeIn(fadeDuration);
        }
        private IEnumerator IFadeIn(float t)
        {
            float alpha = blackBackground.color.a;
            Color fadeColor = blackBackground.color;
            while (alpha > 0)
            {
                yield return new WaitForSeconds(0.01f);
                alpha -= 1/(t*60);
                fadeColor.a = alpha;
                blackBackground.color = fadeColor;
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
