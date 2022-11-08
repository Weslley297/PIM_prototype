using UnityEngine;

public class CameraAudioScript : MonoBehaviour
{
   public AudioClip exploreSound;
    public AudioClip battleSound;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    

    public void PlayExploreSound(){
        StopSound();

        audioSource.clip = exploreSound;
        audioSource.loop = true;
        audioSource.volume = 1;
        audioSource.Play();
    }

    public void PlayBattleSound(){
        StopSound();

        audioSource.clip = battleSound;
        audioSource.loop = true;
        audioSource.volume = 1;
        audioSource.Play();
    }

    public void StopSound(){
        if(!audioSource.isPlaying){
           return;
        }

        audioSource.Stop();
    }

}
