using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private const float FADE_TIME_SECONDS = 3.5f;

    #region Background

    [Header("In Game")]

    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource amb;
    [SerializeField] private AudioSource baseBgm;

    #endregion

    #region Interaction

    [Header("Grab Interaction")]

    [SerializeField] private AudioSource pickupLetter;
    [SerializeField] private AudioSource pickupKey;
    [SerializeField] private AudioSource keySocketed;
    [SerializeField] private AudioSource diskMusic;
    [SerializeField] private AudioSource jumpScareDrop;

    #endregion

    #region GhostChase
    [Header("Event")]
    [SerializeField] private AudioSource ghostChase;
    [SerializeField] private AudioSource ghostDestroyWall;

    #endregion

    #region Elevator
    [Header("Elevator")]
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource doorClose;
    [SerializeField] private AudioSource elevatorMove;
    [SerializeField] private AudioSource elevatorStop;
    [SerializeField] private AudioSource elevatorBgm;

    #endregion

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBgm()
    {
        bgm.Play();
    }

    public void PlayAmb()
    {
        amb.Play();
    }

    public void PlayLetterPickedUpSound()
    {
        //Voiceover
        pickupLetter.Play();
    }

    public void PlayKeyPickedUpSound()
    {
        pickupKey.Play();
    }

    public void PlayKeySocketedSound()
    {
        keySocketed.Play();
    }

    public void PlayElevatorDoorOpenSound()
    {
        doorOpen.Play();
    }
    public void PlayElevatorDoorCloseSound()
    {
        doorClose.Play();
    }
    public void PlayElevatorOperatingSound()
    {
        elevatorMove.Play();
    }

    public void StopElevatorOperatingSound()
    {
        elevatorMove.Stop();
    }
    public void PlayElevatorStopSound()
    {
        elevatorStop.Play();
    }
    public void PlayDiskSocketedSound()
    {
        bgm.Pause();
        StartCoroutine(SoundFadeIn(diskMusic, 1f));
        diskMusic.Play();
        Debug.Log("Vinyl");
    }

    public void StopDiskSocketedSound()
    {
        // diskMusic.Pause();
        // diskMusic.volume = 0f;
        bgm.Play();
        StartCoroutine(SoundFadeIn(bgm, 0.6f));
        Debug.Log("Stop");
    }

    public void playBaseBgm()
    {
        bgm.enabled = false;
        baseBgm.Play();
    }


    public void PlayJumpScareDropSound()
    {
        //Voice
        jumpScareDrop.Play();
    }


    IEnumerator SoundFadeIn(AudioSource audioSource, float maxVolume)
    {
        float timeElapsed = 0;

        while (audioSource.volume < 1)
        {
            audioSource.volume = Mathf.Lerp(0, maxVolume, timeElapsed / FADE_TIME_SECONDS);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }


}
