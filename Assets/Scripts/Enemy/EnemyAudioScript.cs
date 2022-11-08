using UnityEngine;

public class EnemyAudioScript : MonoBehaviour
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
        audioSource.pitch = getPitch(0.8f, 1.4f);
        audioSource.loop = true;
        
        audioSource.Play();
    }

    public void PlayAttackSound(){
        StopSound();

        audioSource.clip = attackSound;
        audioSource.loop = false;
        audioSource.volume = 0.7f;
        audioSource.pitch = getPitch(0.8f, 1.4f);
        audioSource.Play();
    }

    public void PlayHitSound(){
        StopSound();

        audioSource.clip = hitSound;
        audioSource.loop = false;
        audioSource.volume = 0.1f;
        audioSource.pitch = getPitch(0.8f, 1.4f);
        audioSource.Play();
    }

    public void PlayDieSound(){
        StopSound();

        audioSource.clip = dieSound;
        audioSource.loop = false;
        audioSource.volume = 0.5f;
        audioSource.pitch = getPitch(0.8f, 1.4f);
        audioSource.Play();
    }

    public void StopSound(){
        if(!audioSource.isPlaying){
           return;
        }

        audioSource.Stop();
    }

    private float getPitch(float rangeInit, float rangeEnd ){
       return Random.Range(rangeInit, rangeEnd); 
    }
}

