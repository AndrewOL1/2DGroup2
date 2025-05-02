using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectManager : MonoBehaviour
{
    private Image _image;
    public bool InspectIsActive=false;
    public static InspectManager Instance { get; private set; }

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

    // Update is called once per frame
    public void InspectObjectInInventory(Image image)
    {
        _image = image;
        InspectIsActive = true;
    }
    public void InspectAndStoreInInventory(GameObject obj)
    {
        _image.sprite=obj.GetComponent<SpriteRenderer>().sprite;
        _image.gameObject.SetActive(true);
        InspectIsActive = true;
    }
    public void InspectAndStoreInInventory(GameObject obj,Vector2 rectTransform)
    {
        _image.sprite = obj.GetComponent<SpriteRenderer>().sprite;
        RectTransform temp =_image.gameObject.GetComponent<RectTransform>();
        temp.sizeDelta = rectTransform;
        _image.gameObject.SetActive(true);
        InspectIsActive = true;
        //call to invent to store it

    }

}
