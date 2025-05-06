using System.Collections;
using Input;
using Inspect;
using Inventory;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        [HideInInspector]
        public StoryScene currentScene;
        public BottomBarController bottomBar;
        [SerializeField]
        private InputReader input;
        private 
            bool _isInteracting = false,_delay=false,_isDialogueOpen=false;

        [SerializeField] private GameObject firstLetter;
        public static GameController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
                PlayerPrefs.DeleteAll();
            }
            else if (Instance != this)
                Destroy(gameObject);
        }

        void Start()
        {
            input.InteractEvent += HandleInteract;
            input.InteractEventCancelled += HandleInteractCancelled;
        }
        #region Handle Interact
        private void HandleInteract()
        {
            _isInteracting=true;
        }
        private void HandleInteractCancelled()
        {
            _isInteracting=false;
        }
        #endregion
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.2f);
            _delay = false;
        }

        void Update()
        {
            if (_isDialogueOpen)//now it only runs when called. It end when a SceneObject has the EndConversation bool as true.
            {
                Dialogue();
            }   
        }
        public void StartDialogue(StoryScene scene)
        {
            currentScene = scene;
            _isDialogueOpen = true;
            ToggleVisibility();
            bottomBar.PlayScene(currentScene);
            _delay = true;
            StartCoroutine(Delay());
        }

        private void ToggleVisibility()
        {
            this.gameObject.GetComponent<Canvas>().enabled = !this.gameObject.GetComponent<Canvas>().enabled;
        }

        public bool GetIsDialogueOpen()
        {
            return _isDialogueOpen;
        }
        private void Dialogue()
        {
            if (_isInteracting && !_delay)
            {
                if (bottomBar.IsCompleted())
                {
                    if (bottomBar.IsLastSentence())
                    {
                        _isDialogueOpen = false;
                        ToggleVisibility();
                        if(bottomBar.currentScene.EndConversation)
                            InspectManager.Instance.InspectAndStoreInInventory(firstLetter,firstLetter.GetComponent<InventoryItem>().Size);
                    }
                    else
                        bottomBar.PlayNextSentence();
                }
                else 
                {
                    bottomBar.FinishSentence();
                }

            
                _delay = true;
                _isInteracting = false;
                StartCoroutine(Delay());
            }
        }

    }
}  
