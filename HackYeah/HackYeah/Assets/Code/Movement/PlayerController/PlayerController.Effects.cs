using UnityEngine;

public partial class PlayerController
{
    private float eff_TargetGlow = 0f;
    private float eff_CurrentGlow = 0f;
    private float sd_eff_glow = 0f;
    public Material ToGlow;

    private void UpdateGlowEffect()
    {
        if (ToGlow != null)
        {
            if (capsuleCollider.enabled)
                eff_TargetGlow = rigbody.velocity.magnitude / 14f;
            else
                eff_TargetGlow = 0f;

            if (eff_TargetGlow - 0.25f > 0f)
            {
                eff_CurrentGlow = Mathf.SmoothDamp(eff_CurrentGlow, eff_TargetGlow, ref sd_eff_glow, 0.1f);
            }
            else
            {
                eff_CurrentGlow = Mathf.SmoothDamp(eff_CurrentGlow, 0f, ref sd_eff_glow, 0.25f);
            }

            //Debug.Log("targetGlow = " + eff_TargetGlow);
            float brightness = 0.55f;
            ToGlow.SetColor("_EmissionColor", new Color(brightness, brightness, brightness, 1f) * (1f + eff_CurrentGlow));
        }
    }
}
