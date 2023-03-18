using UnityEngine;

public class RandomSound : MonoBehaviour{
    private AudioSource source;

    public AudioClip[] clips;

    private void Start(){
        source = GetComponent<AudioSource>();
        var randomNumber = Random.Range(0, clips.Length);
        source.clip = clips[randomNumber];
        source.Play();
    }
}