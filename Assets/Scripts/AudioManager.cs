using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> musicList;
    [SerializeField] private List<AudioClip> sfxList;
    [Header("Voice Clips")]
    [SerializeField] private List<AudioClip> bougClips;
    [SerializeField] private List<AudioClip> dwarfClips;
    [SerializeField] private List<AudioClip> clownClips;
    [SerializeField] private List<AudioClip> kingClips;

    private AudioSource audioPlayer;

    public void PlayClip(string listToCall, int i)
    {
        audioPlayer = GetComponent<AudioSource>();
        
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

    }
}
