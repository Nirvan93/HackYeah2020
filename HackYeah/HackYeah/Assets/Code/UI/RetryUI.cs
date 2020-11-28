using UnityEngine;

public class RetryUI : MonoBehaviour
{
    public void RetryLevel()
    {
        LevelsManager lvlMenager = new LevelsManager();
        lvlMenager.ResetLevel();
    }
}
