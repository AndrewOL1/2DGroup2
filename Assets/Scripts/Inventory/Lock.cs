using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] int keyID;
    [SerializeField] StoryScene dialogue;
    [SerializeField] StoryScene errorDialogue;
    public void TryToOpenLock(int ID )
    {
        if(keyID==ID)
        {
            //open object visual component
            Debug.Log("Lock opened");
            GameController.Instance.StartDialogue(dialogue);

        }
        else
        {
            GameController.Instance.StartDialogue(errorDialogue);
        }
    }
    private void OnMouseDown()
    {
        TryToOpenLock(PlayerController.Instance.playerData.id);
    }
}
