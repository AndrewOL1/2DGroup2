using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Controllers;
using Inspect;
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
        [SerializeField]
        private Texture2D _cursorTexture;
        private Texture2D _cursor;
#if UNITY_EDITOR
        public void SetScenes()
        {
            sceneList = AssetDatabase.LoadAssetAtPath<SceneList>("Assets/Scripts/SceneManagement/SceneList.asset");
        }
#endif
        private void Start()
        {
            _sound = GetComponent<MouseClickSound>();
            _cursor= AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Sprites/cursor1.png");
        }
        private void OnMouseOver()
        {
            Cursor.SetCursor(_cursorTexture, new Vector2(16, 16), CursorMode.Auto);
        }

        private void OnMouseExit()
        {
            Cursor.SetCursor(_cursor, new Vector2(8, 0), CursorMode.Auto);
        }

        private void OnMouseDown()
        {
            if(GameController.Instance.GetIsDialogueOpen())return;
            if(InspectManager.Instance.GetIsInspectActive())return;
            if(SceneManager.Instance.overUI)return;
            _sound.PlaySound();
            SceneManager.Instance.FadeOut(.5f);
            StartCoroutine(MoveToNextScene());
        }

        private IEnumerator MoveToNextScene()
        {
            yield return new WaitForSeconds(.5f);
            Cursor.SetCursor(_cursor, new Vector2(8, 0), CursorMode.Auto);
            SceneManager.Instance.LoadNextScene(selectedScene);
        }
    }
}
