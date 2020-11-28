using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Klasa obsługująca wyłaniające się napisy/buttony
/// </summary>
public class FadingUI : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Button RestartButton;
    public Image RestartButtonSprite;
    public TextMeshProUGUI RestartButtonText;
    public float FadingSpeed = 5f;
    
    void Start()
    {
        Text.alpha = 0f;
        RestartButton.enabled = false;
        RestartButtonText.enabled = false;
        RestartButtonSprite.enabled = false;
        RestartButtonSprite.color = ChangeAlpha(0, RestartButtonSprite.color);
    }

    /// <summary>
    /// Teksty po umarnięciu, wyświetlanie buttonu restart
    /// </summary>
    /// <returns></returns>
    public IEnumerator DeathUI()
    {
        yield return new WaitForEndOfFrame();

        float elapsed = 0f;
        while(Text.alpha < .99f)
        {
            elapsed += Time.deltaTime;
            Text.alpha = Mathf.Lerp(Text.alpha, 1f, Time.deltaTime * FadingSpeed);
            yield return new WaitForEndOfFrame();
        }

        RestartButton.enabled = true;
        RestartButtonText.enabled = true;
        RestartButtonSprite.enabled = true;
        RestartButtonSprite.color = ChangeAlpha(1f, RestartButtonSprite.color);
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Metoda zwraca kolor obiektu, uwzględniając alpha
    /// </summary>
    public Color ChangeAlpha(float alpha, Color col)
    {
        return new Color(col.r, col.b, col.b, alpha);
    }
}
