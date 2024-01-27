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

    public void playClip()
    {

    }
}
