using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioClip walkSound;
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip dieSound;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWalkSound(){
        if(audioSource.isPlaying){
            return;
        }

        if(audioSource.clip == walkSound){
            return;
        }

        audioSource.clip = walkSound;
        audioSource.volume = 0.3f;
        audioSource.loop = true;
        
        audioSource.Play();
    }

    public void PlayAttackSound(){
        StopSound();

        audioSource.clip = attackSound;
        audioSource.volume = 1f;
        audioSource.loop = false;

        audioSource.Play();
    }

    public void PlayHitSound(){
        StopSound();

        audioSource.clip = hitSound;
        audioSource.volume = 1f;
        audioSource.loop = false;

        audioSource.Play();
    }

    public void PlayDieSound(){
        StopSound();

        audioSource.clip = dieSound;
        audioSource.volume = 1f;
        audioSource.loop = false;

        audioSource.Play();
    }

    public void StopSound(){
        if(!audioSource.isPlaying){
           return;
        }

        audioSource.Stop();
    }
}

