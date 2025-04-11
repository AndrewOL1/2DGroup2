using UnityEngine;
using UnityEngine.InputSystem;
using Inventory;

public class MouseClickSound : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = InventoryManager.Instance.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        
        //audioSource.PlayOneShot(clickSound);
    }
    public void playSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
