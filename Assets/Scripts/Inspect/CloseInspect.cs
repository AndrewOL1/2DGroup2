using Controllers;
using DialogueTriggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Inspect
{
    public class CloseInspect : MonoBehaviour,IPointerClickHandler
    {
        public bool startDialogue = false;
        [SerializeField]
        private DialogueTrigger dialogueTrigger;
        [SerializeField]
        StoryScene storyScene;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (startDialogue)
                dialogueTrigger.PlayInteractDialogue();
            InspectManager.Instance.TurnOffInspect();
            if (InspectManager.Instance.delayedDialogue)
            {
                GameController.Instance.StartDialogue(storyScene);
                InspectManager.Instance.delayedDialogue = false;
            }

            this.gameObject.SetActive(false);
        }
    }
}
