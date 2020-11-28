using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelObject : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody!=null)
        {
            PlayerController player = other.attachedRigidbody.GetComponent<PlayerController>();
            if(player!=null)
            {
                LevelController.Instance.FinishLevel();
            }
        }
    }
}
