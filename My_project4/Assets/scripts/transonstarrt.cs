using UnityEngine;

public class TransOnStart : MonoBehaviour {
    void Start() {
        Renderer rend = GetComponent<Renderer>(); // Récupère le Renderer du GameObject
        if (rend == null) {
            Debug.LogError("Renderer non trouvé sur " + gameObject.name);
            return;
        }

        Material mat = rend.material;

        // Assure que le matériau est bien en mode Transparent
        mat.SetFloat("_Mode", 3); // Mode Transparent
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000; // Assurer la transparence

        // Changer la couleur avec transparence
        mat.color = new Color(1, 0, 0, 0.3f); // Rouge semi-transparent
    }
}