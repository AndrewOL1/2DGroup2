using Input;
using Inventory;
using SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;
    public BackgroundController backgroundController;
    [SerializeField]
    private InputReader _input;
    bool _isInteracting = false,_delay=false,isDialogueOpen=false;
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        bottomBar.PlayScene(currentScene);
        backgroundController.SetImage(currentScene.background);
        _input.InteractEvent += HandleInteract;
        _input.InteractEventCancelled += HandleInteractCancelled;
        StartDialogue(currentScene);
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
        yield return new WaitForSeconds(0.5f);
        _delay = false;
    }

    void Update()
    {
        if (isDialogueOpen)//now it only runs when called. It end when a SceneObject has the EndConversation bool as true.
        {
            Dialogue();
            
        }   
    }
    public void StartDialogue(StoryScene scene)
    {
        currentScene = scene;
        isDialogueOpen = true;
        //Add a fuction to active the visual componets 
    }
    private void Dialogue()
    {
        
            if (_isInteracting && !_delay)
            {
                if (bottomBar.IsCompleted())
                {
                    if (bottomBar.IsLastSentence())
                    {
                        // next scene is null then close visuals
                        currentScene = currentScene.nextScene;
                        bottomBar.PlayScene(currentScene);
                        //backgroundController.SwitchImage(currentScene.background);// not needed switching scenes instead
                        if (currentScene.loadNewScene)
                            SceneManager.Instance.LoadNextScene(currentScene.sceneName);
                        if(currentScene.EndConversation)
                            isDialogueOpen = false;//Will need a fucntion to deactive the visual componets
                    }
                    else
                    {
                        bottomBar.PlayNextSentence();
                    }
                }
                _delay = true;
                _isInteracting = false;
                StartCoroutine(Delay());
            }
        }

}  
