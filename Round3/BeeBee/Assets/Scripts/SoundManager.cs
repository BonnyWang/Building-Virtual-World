using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("BGM")]
    [SerializeField] private List<AudioSource> Bgm = new List<AudioSource>();
    [SerializeField] private List<AudioSource> BgmMelody = new List<AudioSource>();
    [SerializeField] private List<AudioSource> BgmBacking = new List<AudioSource>();
    [SerializeField] private List<AudioSource> BgmBass = new List<AudioSource>();
    [SerializeField] private List<AudioSource> BgmDrums = new List<AudioSource>();
    [SerializeField] private List<AudioSource> BgmFills = new List<AudioSource>();
    [SerializeField] private List<AudioSource> BeeSound = new List<AudioSource>();

    [Header("SFX")]
    [SerializeField] private List<AudioSource> KeeperAttack = new List<AudioSource>();
    [SerializeField] private List<AudioSource> BeeAttack = new List<AudioSource>();
    [SerializeField] private List<AudioSource> DieSound = new List<AudioSource>();
    [SerializeField] private List<AudioSource> WinSound = new List<AudioSource>();

    private static int bgmStage = 0;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        foreach (AudioSource source in Bgm)
        {
            source.Play();
            source.mute = true;
        }

        SetBgmStage();
    }

    public void IncreaseBgmStage()
    {
        bgmStage++;

        if (bgmStage > 4)
        {
            bgmStage = 4;
        }

        SetBgmStage();
    }

    public void DecreaseBgmStage()
    {
        bgmStage--;

        if (bgmStage < 0)
        {
            bgmStage = 0;
        }

        SetBgmStage();
    }

    public void PlayKeeperAttackSound()
    {
        KeeperAttack[Random.Range(0, KeeperAttack.Count)].Play();
    }

    public void PlayBeeAttackSound()
    {
        BeeAttack[Random.Range(0, BeeAttack.Count)].Play();
    }

    public void PlayDieSound()
    {
        DieSound[Random.Range(0, DieSound.Count)].Play();
    }

    public void PlayWinSound()
    {
        bgmStage = 0;
        SetBgmStage();

        WinSound[Random.Range(0, WinSound.Count)].Play();
    }

    private void SetBgmStage()
    {
        switch (bgmStage)
        {
            case 0:
                PlayMelody(0);
                PlayBacking(0);
                PlayBass(1);
                PlayDrums(1);
                PlayBeeSound(0);
                break;
            case 1:
                PlayMelody(0);
                PlayBacking(1);
                PlayBass(1);
                PlayDrums(1);
                PlayBeeSound(1);
                break;
            case 2:
                PlayMelody(1);
                PlayBacking(0);
                PlayBass(2);
                PlayDrums(1);
                break;
            case 3:
                PlayMelody(1);
                PlayBacking(2);
                PlayBass(2);
                PlayDrums(2);
                break;
            case 4:
                PlayMelody(2);
                PlayBacking(2);
                PlayBass(2);
                PlayDrums(2);
                break;
        }

        PlayFills(1);
    }

    private void PlayMelody(int intensity)
    {
        foreach (var source in BgmMelody)
        {
            source.mute = true;
        }

        switch (intensity)
        {
            case 0:
                break;
            case 1:
                BgmMelody[0].mute = false;
                break;
            case 2:
                BgmMelody[1].mute = false;
                break;
        }
    }

    private void PlayBacking(int intensity)
    {
        foreach (var source in BgmBacking)
        {
            source.mute = true;
        }

        switch (intensity)
        {
            case 0:
                break;
            case 1:
                BgmBacking[0].mute = false;
                break;
            case 2:
                BgmBacking[1].mute = false;
                break;
        }
    }

    private void PlayBass(int intensity)
    {
        foreach (var source in BgmBass)
        {
            source.mute = true;
        }

        switch (intensity)
        {
            case 0:
                break;
            case 1:
                BgmBass[0].mute = false;
                break;
            case 2:
                BgmBass[1].mute = false;
                break;
        }
    }

    private void PlayDrums(int intensity)
    {
        foreach (var source in BgmDrums)
        {
            source.mute = true;
        }

        switch (intensity)
        {
            case 0:
                break;
            case 1:
                BgmDrums[0].mute = false;
                break;
            case 2:
                BgmDrums[1].mute = false;
                break;
        }
    }

    private void PlayFills(int intensity)
    {
        BgmFills[0].mute = false;
    }

    private void PlayBeeSound(int intensity)
    {
        foreach (var source in BeeSound)
        {
            source.mute = true;
        }

        switch (intensity)
        {
            case 0:
                break;
            case 1:
                BeeSound[0].mute = false;
                break;
        }
    }
}
