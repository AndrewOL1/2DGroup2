using System;
using Controllers;
using DialogueTriggers;
using Player;
using UnityEditor;
using UnityEngine;

namespace Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        public int ID;
        public string Name;
        private DialogueTrigger _dialogueTrigger;
        private MouseClickSound sound;
        [SerializeField]
        private Texture2D _cursorTexture;
        private Texture2D _cursor;
        private bool _grabbed = false;
        private void Start()
        {
            if(InventoryManager.Instance.CheckIfInInventory(ID))
                this.gameObject.SetActive(false);
            _dialogueTrigger = GetComponent<DialogueTrigger>();
            
                sound = GetComponent<MouseClickSound>();
            _cursor= AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Sprites/cursor1.png");
            
        }
        private void OnMouseOver()
        {
            if (!_grabbed)
                Cursor.SetCursor(_cursorTexture, new Vector2(16, 16), CursorMode.Auto);
        }

        private void OnMouseExit()
        {
            Cursor.SetCursor(_cursor, new Vector2(8, 0), CursorMode.Auto);
        }

        private void OnMouseDown()
        {
            if(GameController.Instance.GetIsDialogueOpen())return;
            sound.PlaySound();
            _grabbed = true;
            //check player state and decide to interact or pick up
            InventoryManager.Instance.AddItemToInventory(this,ID,Name);//might need to set it to send its prefab so i can be spawned again
            _dialogueTrigger.PlayInteractDialogue();
            Cursor.SetCursor(_cursor, new Vector2(8, 0), CursorMode.Auto);
            Destroy(gameObject);//maybe turn off visual and the collider
        }
    }
}
