using Controllers;
using Player;
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
        public void TryToOpenLock(int id )
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
        }
        private void Start()
        {
            _sound = GetComponent<MouseClickSound>();
        }
    }
}
