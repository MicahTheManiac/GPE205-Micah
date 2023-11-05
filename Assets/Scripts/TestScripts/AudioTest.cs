using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTest : MonoBehaviour
{
    public AudioClip testSound;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource test = this.GetComponent<AudioSource>();
        if (test != null)
        {
            test.PlayOneShot(testSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
