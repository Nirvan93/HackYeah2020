using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioSource PlayAudio(AudioClip clip, Vector3 playingPosition)
    {
        GameObject audio = new GameObject();
        AudioSource source = audio.AddComponent<AudioSource>();
        source.clip = clip;
        source.spatialBlend = 1;
        GameObject.Destroy(audio, clip.length);
        return source;
    }

    public static AudioClip GetClip(List<AudioClip> clips)
    {
        return clips[Random.Range(0, clips.Count)];
    }
}
