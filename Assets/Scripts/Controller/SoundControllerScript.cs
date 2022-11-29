using UnityEngine;

public class SoundControllerScript : MonoBehaviour
{
    public AudioClip labMusic;
    public AudioClip battleMusic;

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
    
    public void StopMusic(){
        audioSource.Stop();
    }
}
