using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Light directionalLight;
    public Light[] additionalLights;
    public float intensity = 1.0f;
    public Color lightColor = Color.white;

    void Start()
    {
        if (directionalLight != null)
        {
            // Configurer la lumière directionnelle pour un éclairage optimal
            directionalLight.intensity = intensity;
            directionalLight.color = lightColor;
            directionalLight.transform.rotation = Quaternion.Euler(50, 30, 0); // Ajuster l'angle
            directionalLight.shadows = LightShadows.Soft;
        }

        // Activer et configurer les lumières supplémentaires
        foreach (Light light in additionalLights)
        {
            if (light != null)
            {
                light.enabled = true;
                light.intensity = intensity / 2; // Moins intense que la directionnelle
                light.color = lightColor;
            }
        }
    }
}
