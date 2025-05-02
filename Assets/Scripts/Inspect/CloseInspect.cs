using DialogueTriggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInspect : MonoBehaviour
{
    public bool StartDialogue = false;
    [SerializeField]
    private DialogueTrigger dialogueTrigger;
    private void OnMouseDown()
    {
        if (StartDialogue)
            dialogueTrigger.PlayInteractDialogue();
        InspectManager.Instance.InspectIsActive = false;
        this.gameObject.SetActive(false);
    }
}
