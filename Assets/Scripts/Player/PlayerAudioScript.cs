
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    public AudioClip walkSound;
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip dieSound;
    public GameObject hitParticle;
    

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWalkSound(){
        if(audioSource.isPlaying && audioSource.clip == walkSound){
            return;
        }

        audioSource.clip = walkSound;
        audioSource.loop = true;
        audioSource.priority = 50;
        audioSource.volume = 0.9f;
        
        audioSource.pitch = getPitch(1.4f, 1.6f);
        audioSource.Play();
    }

    public void PlayAttackSound(){
        StopSound();

        audioSource.clip = attackSound;
        audioSource.loop = false;
        audioSource.volume = 0.7f;
        audioSource.pitch = getPitch(1f, 1.4f);
        audioSource.Play();
    }

    public void PlayHitSound(){
        StopSound();

        audioSource.clip = hitSound;
        audioSource.loop = false;
        audioSource.volume = 0.6f;
        audioSource.pitch = getPitch(0.8f, 1.4f);
        audioSource.Play();
        if(hitParticle != null)
        {
            Instantiate(hitParticle, this.transform);
        }
    }

    public void PlayDieSound(){
        StopSound();

        audioSource.clip = dieSound;
        audioSource.loop = false;
        audioSource.volume = 0.6f;
        audioSource.pitch = getPitch(0.8f, 1.4f);
        audioSource.Play();
    }

    public void StopSound(){
        if(!audioSource.isPlaying){
           return;
        }

        audioSource.Stop();
    }

    public void StopWalkSound(){
        if(audioSource.clip != walkSound){
            return;
        }

        if(!audioSource.isPlaying){
           return;
        }

        audioSource.Stop();
    }

    private float getPitch(float rangeInit, float rangeEnd ){
       return Random.Range(rangeInit, rangeEnd); 
    }
}
