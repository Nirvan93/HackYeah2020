using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource source;
    //public List<AudioClip> Steps;
    public List<AudioClip> BreakBone;
    public List<AudioClip> Pain;
    public List<AudioClip> Jumps;

    public void PlayAudio(List<AudioClip> clips)
    {
        source.PlayOneShot(AudioController.GetClip(clips));
    }
}
