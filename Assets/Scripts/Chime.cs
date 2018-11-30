using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chime : MonoBehaviour {

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayChilme(){ audioSource.PlayOneShot(AudioClips.Instance.Chime);}
}
