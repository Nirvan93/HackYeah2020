using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundTrigger : MonoBehaviour
{
    public EnemyAlienAudio EnemyAudio;
    private bool odegrano = false;        

    private void OnTriggerEnter(Collider other)
    {
        if (odegrano) return;
        if (other.tag != "Player") return;

        //AudioController.GetClip(AudioList);

        if ( EnemyAudio)
        {
            EnemyAudio.PlayAudioVoice(EnemyAudio.Taunts);
            odegrano = true;
        }
        //int selected = Random.Range(0, AudioList.Count);
        //AudioSource.PlayClipAtPoint(AudioList[selected], PlayAudioPosition.transform.position, 1.2f);
    }
}
