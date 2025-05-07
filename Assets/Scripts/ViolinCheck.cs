using System.Collections;
using System.Collections.Generic;
using Inventory;
using Player;
using UnityEngine;

public class ViolinCheck : MonoBehaviour
{
    public void Violin()
    {
        PlayerController.Instance.playerData.fixViolin = true;
    }
    public void Start()
    {
        if(PlayerController.Instance.playerData.fixViolin)
            GetComponent<Lock>().UpdateSprite();
    }
}
