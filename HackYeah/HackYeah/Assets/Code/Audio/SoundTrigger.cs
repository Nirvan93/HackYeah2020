using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public GameObject PlayAudioPosition;
    EnemyAlienAudio audio;
    public List<AudioClip> AudioList;
            
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        AudioController.GetClip(AudioList);

        int selected = Random.Range(0, AudioList.Count);

        AudioController.PlayAudio(AudioList[selected], PlayAudioPosition.transform.position);
    }
}
