using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClickSound : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) // Left mouse button
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
