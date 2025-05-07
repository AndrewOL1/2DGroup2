using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using Player;
using UnityEngine;

public class ClockCheck : MonoBehaviour
{
    public void Clock()
    {
        PlayerController.Instance.playerData.fixClock = true;
    }

    public void Start()
    {
        if(PlayerController.Instance.playerData.fixClock)
            GetComponent<Lock>().UpdateSprite();
    }
}
