using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public GameObject PlayAudioPosition;
    public List<AudioClip> AudioList;
            
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        AudioController.GetClip(AudioList);

        int selected = Random.Range(0, AudioList.Count);
        AudioSource.PlayClipAtPoint(AudioList[selected], PlayAudioPosition.transform.position);
    }
}
