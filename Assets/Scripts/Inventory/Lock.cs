using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] int keyID;
    [SerializeField] StoryScene dialogue;
    [SerializeField] StoryScene errorDialogue;
    [SerializeField] GameObject Key;
    [SerializeField] bool spawnItem;
    private MouseClickSound sound;
    public void TryToOpenLock(int ID )
    {
        if(keyID==ID)
        {
            //open object visual component
            Debug.Log("Lock opened");
            GameController.Instance.StartDialogue(dialogue);
            if (spawnItem)
                Instantiate(Key, new Vector3(-5, -2.2939999f, 0), Quaternion.identity);

        }
        else
        {
            GameController.Instance.StartDialogue(errorDialogue);
        }
    }
    private void OnMouseDown()
    {
        sound.playSound();
        TryToOpenLock(PlayerController.Instance.playerData.id);
    }
    private void Start()
    {
        sound = GetComponent<MouseClickSound>();
    }
}
