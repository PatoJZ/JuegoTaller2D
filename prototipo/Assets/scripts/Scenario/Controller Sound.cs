using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSound : MonoBehaviour
{
    public static ControllerSound instance;
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void ExecuteSound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
