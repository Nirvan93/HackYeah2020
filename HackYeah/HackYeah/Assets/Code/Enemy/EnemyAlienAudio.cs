using System.Collections.Generic;
using UnityEngine;

public class EnemyAlienAudio : MonoBehaviour
{
    public AudioSource Voice;
    public AudioSource SfxSource;

    public List<AudioClip> Steps;
    public List<AudioClip> BreakBone;
    public List<AudioClip> Taunts;
    public List<AudioClip> Pain;

    public void PlayAudioSfx(List<AudioClip> clips)
    {
        SfxSource.PlayOneShot(AudioController.GetClip(clips));
    }

    public void PlayAudioVoice(List<AudioClip> clips)
    {
        SfxSource.PlayOneShot(AudioController.GetClip(clips));
    }
}
