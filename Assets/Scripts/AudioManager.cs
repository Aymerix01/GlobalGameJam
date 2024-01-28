using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Dictionary<string, AudioClip[]> audioClips = new Dictionary<string, AudioClip[] >(40);
    
    public List<string> names;
    public List<List<AudioClip>> clips;
    private AudioSource audioPlayer;
    
    public void PlayClip(AudioClip audioClip)
    {
        
        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = audioClip;
        audioPlayer.Play();
        /*
        if(listToCall == "Music")
        {
            audioPlayer.clip = musicList[i];
            audioPlayer.Play();
        }
        if (listToCall == "SFX")
        {
            audioPlayer.clip = sfxList[i];
            audioPlayer.Play();
        }
        if (listToCall == "Boug")
        {
            audioPlayer.clip = bougClips[i];
            audioPlayer.Play();
        }
        if (listToCall == "Dwarf")
        {
            audioPlayer.clip = dwarfClips[i];
            audioPlayer.Play();
        }
        if (listToCall == "Clown")
        {
            audioPlayer.clip = clownClips[i];
            audioPlayer.Play();
        }
        if (listToCall == "King")
        {
            audioPlayer.clip = kingClips[i];
            audioPlayer.Play();
        }
        */
    }
}
