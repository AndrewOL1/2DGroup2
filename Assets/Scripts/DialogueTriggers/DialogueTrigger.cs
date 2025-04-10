using UnityEngine;

namespace DialogueTriggers
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private bool OnSceneEnter, OnInteract;
        private bool _stop = false;
        [SerializeField] private StoryScene startingDialogue;

        private void Start()
        {
            if (OnSceneEnter == OnInteract)
            {
                _stop = true;
                Debug.Log("Please select one and only one option for "+ this.gameObject.name+" DialogueTrigger");
            }

            if (OnSceneEnter)
            {
                GameController.Instance.StartDialogue(startingDialogue);
            }
        }

        public void PlayInteractDialogue()
        {
            GameController.Instance.StartDialogue(startingDialogue);
        }
    }
}
