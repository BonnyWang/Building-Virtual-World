using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlay : MonoBehaviour
{
    public AudioClip[] audioClips;

    public void play( float volume)
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[randomIndex];
        Debug.Log(audioSource.clip);
        audioSource.volume = volume;
        audioSource.Play();
    }
}
