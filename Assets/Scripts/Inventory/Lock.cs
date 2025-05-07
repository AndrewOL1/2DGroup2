using Controllers;
using Player;
using UnityEditor;
using UnityEngine;

namespace Inventory
{
    public class Lock : MonoBehaviour
    {
        [SerializeField] int keyID;
        [SerializeField] StoryScene dialogue;
        [SerializeField] StoryScene errorDialogue;
        [SerializeField] GameObject key;
        [SerializeField] bool spawnItem;
        [SerializeField] Vector3 spawnPosition;
        private MouseClickSound _sound;
        [SerializeField]
        private Texture2D _cursorTexture;
        private Texture2D _cursor;

        private void TryToOpenLock(int id )
        {
            if(keyID==id)
            {
                //open object visual component
                Debug.Log("Lock opened");
                GameController.Instance.StartDialogue(dialogue);
                if (spawnItem)
                    Instantiate(key, spawnPosition, Quaternion.identity);

            }
            else
            {
                GameController.Instance.StartDialogue(errorDialogue);
            }
        }
        private void OnMouseDown()
        {
            if(GameController.Instance.GetIsDialogueOpen())return;
            _sound.PlaySound();
            TryToOpenLock(PlayerController.Instance.playerData.id);
            InventoryManager.Instance.RemoveItemFromInventory(PlayerController.Instance.playerData.id);
        }
        private void OnMouseOver()
        {
            Cursor.SetCursor(_cursorTexture, new Vector2(16, 16), CursorMode.Auto);
        }
        private void OnMouseExit()
        {
            Cursor.SetCursor(_cursor, new Vector2(8, 0), CursorMode.Auto);
        }
        private void Start()
        {
            _sound = GetComponent<MouseClickSound>();
            _cursor= AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Sprites/cursor1.png");
        }
    }
}
