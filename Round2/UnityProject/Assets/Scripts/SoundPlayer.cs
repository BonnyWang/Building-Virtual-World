using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public bool sequential;
    public bool random;
    public bool playOnAwake;
    public AudioClip[] audioClips;
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(playOnAwake){
            if(sequential){
                StartCoroutine(play_sequential());
            }
            else if(random){
                play_random();
            }
        }
        
    }

    public IEnumerator play_sequential(){
        yield return null;

        // Sequentially play all audio clips
        foreach(AudioClip clip in audioClips){
            audioSource.PlayOneShot(clip);
            // Wait for the clip to finish playing
            while(audioSource.isPlaying){
                yield return null;
            }
        }
    }

    public void play_random(){
        int randomIndex = Random.Range(0, audioClips.Length);
        if(audioSource != null){
            audioSource.clip = audioClips[randomIndex];
            audioSource.Play();
        }
    }
}
