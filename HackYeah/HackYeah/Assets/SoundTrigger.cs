using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    EnemyAlienAudio audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            audio.IsPlayerOnTrigger = true;
    }
}
