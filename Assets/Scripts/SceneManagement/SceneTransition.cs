using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


namespace SceneManagement
{
    public class SceneTransition : MonoBehaviour
    { 
        [SerializeField]private string selectedScene;
        public string SelectedScene => selectedScene;
        [SerializeField]
        public SceneList sceneList;
        private MouseClickSound sound;
#if UNITY_EDITOR
        public void SetScenes()
        {
            sceneList = AssetDatabase.LoadAssetAtPath<SceneList>("Assets/Scripts/SceneManagement/SceneList.asset");
        }
#endif
        private void Start()
        {
            sound = GetComponent<MouseClickSound>();
        }
        private void OnMouseOver()
        {
            //highlight
        }

        private void OnMouseDown()
        {
            if(GameController.Instance.GetIsDialogueOpen())return;
            if(SceneManager.Instance.overUI)return;
            sound.playSound();
            MoveToNextScene();
        }

        private void MoveToNextScene()
        {
            SceneManager.Instance.LoadNextScene(selectedScene);
        }
    }
}
