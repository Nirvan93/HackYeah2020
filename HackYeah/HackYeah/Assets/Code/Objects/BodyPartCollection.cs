using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartCollection : Singleton<BodyPartCollection>
{
    [SerializeField]
    private List<BodyPart> _bodyParts = new List<BodyPart>();

   

    public void StartClicking()
    {
        foreach(BodyPart bodyPart in _bodyParts)
        {
            bodyPart.StartTakingClickInput();
        }
    }


    public bool StopClicking()
    {
        bool teleportedCorrectly = true;
        foreach (BodyPart bodyPart in _bodyParts)
        {
            bodyPart.StopClicking();
            if (!bodyPart.WasClicked)
                teleportedCorrectly = false;
        }
        return teleportedCorrectly;
    }

    public void AddRigidbodyToNotClickedBodyParts()
    {
        foreach (BodyPart bodyPart in _bodyParts)
        {
            bodyPart.BreakIfNotClicked();
        }
    }
}
