using System.Collections;
using System.Collections.Generic;
using Controllers;
using Player;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    StoryScene storyScene;
    [SerializeField]
    GameObject letter;
    void Start()
    {
        if (PlayerController.Instance.playerData.fixViolin && PlayerController.Instance.playerData.openChest)
        {
            GameController.Instance.StartDialogue(storyScene);
            Instantiate(letter);
        }
    }

}
