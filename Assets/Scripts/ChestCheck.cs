using System.Collections;
using System.Collections.Generic;
using Inventory;
using Player;
using UnityEngine;

public class ChestCheck : MonoBehaviour
{
    public void Chest()
    {
        PlayerController.Instance.playerData.openChest = true;
    }
    public void Start()
    {
        if(PlayerController.Instance.playerData.openChest)
            GetComponent<Lock>().UpdateSprite();
    }
}
