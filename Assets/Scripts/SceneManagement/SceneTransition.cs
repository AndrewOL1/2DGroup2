using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace SceneManagement
{
    public class SceneTransition : MonoBehaviour
    { 
        private string selectedScene;
        public string SelectedScene => selectedScene;
        [SerializeField]
        public SceneList sceneList;
#if UNITY_EDITOR
        public void SetScenes()
        {
            sceneList = AssetDatabase.LoadAssetAtPath<SceneList>("Assets/Scripts/SceneManagement/SceneList.asset");
        }
        #endif
        private void OnMouseOver()
        {
            //highlight
        }

        private void OnMouseDown()
        {
            MoveToNextScene();
        }

        private void MoveToNextScene()
        {
            SceneManagement.SceneManager.Instance.LoadNextScene(selectedScene);
        }
    }
}
