using Input;
using Inventory;
using SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;
    //public BackgroundController backgroundController;
    [SerializeField]
    private InputReader input;
    private 
    bool _isInteracting = false,_delay=false,_isDialogueOpen=false;
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
        //bottomBar.PlayScene(currentScene);
        //backgroundController.SetImage(currentScene.background);
        input.InteractEvent += HandleInteract;
        input.InteractEventCancelled += HandleInteractCancelled;
        //StartDialogue(currentScene);
    }
    private void HandleInteract()
    {
        _isInteracting=true;
    }
    private void HandleInteractCancelled()
    {
        _isInteracting=false;
    }
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
