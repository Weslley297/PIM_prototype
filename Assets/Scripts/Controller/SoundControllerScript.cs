using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllerScript : MonoBehaviour
{
    public AudioClip labMusic;
    public AudioClip battleMusic;
    public AudioClip collectItemSound;
    public AudioClip collectLifeItemSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLabMusic(){
        audioSource.clip = labMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayBattleMusic(){
        audioSource.clip = battleMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayCollectItemSound(){
        audioSource.clip = collectItemSound;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayCollectLifeItemSound(){
        audioSource.clip = collectLifeItemSound;
        audioSource.loop = false;
        audioSource.Play();
    }
    
    public void StopMusic(){
        audioSource.Stop();
    }
}
