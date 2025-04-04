using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] int keyID;
    [SerializeField] StoryScene dialogue;
    public void TryToOpenLock(int ID )
    {
        if(keyID==ID)
        {
            //open object visual component
            GameController.Instance.StartDialogue(dialogue);

        }
    }
}
