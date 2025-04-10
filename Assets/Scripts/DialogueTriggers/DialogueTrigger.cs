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
                if (!PlayerPrefs.HasKey(startingDialogue.name))
                {
                    GameController.Instance.StartDialogue(startingDialogue);
                    PlayerPrefs.SetInt(startingDialogue.name, 1);
                    PlayerPrefs.Save();
                }
            }
        }

        public void PlayInteractDialogue()
        {
            GameController.Instance.StartDialogue(startingDialogue);
        }
    }
}
