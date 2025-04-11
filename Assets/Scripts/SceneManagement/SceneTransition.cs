using System;
using UnityEngine;
using System.Collections.Generic;
using Controllers;
using UnityEditor;


namespace SceneManagement
{
    public class SceneTransition : MonoBehaviour
    { 
        [HideInInspector]
        [SerializeField]private string selectedScene;
        public string SelectedScene => selectedScene;
        [HideInInspector]
        [SerializeField]
        public SceneList sceneList;
        private MouseClickSound _sound;
#if UNITY_EDITOR
        public void SetScenes()
        {
            sceneList = AssetDatabase.LoadAssetAtPath<SceneList>("Assets/Scripts/SceneManagement/SceneList.asset");
        }
#endif
        private void Start()
        {
            _sound = GetComponent<MouseClickSound>();
        }
        private void OnMouseOver()
        {
            //highlight
        }

        private void OnMouseDown()
        {
            if(GameController.Instance.GetIsDialogueOpen())return;
            if(SceneManager.Instance.overUI)return;
            _sound.PlaySound();
            MoveToNextScene();
        }

        private void MoveToNextScene()
        {
            SceneManager.Instance.LoadNextScene(selectedScene);
        }
    }
}
