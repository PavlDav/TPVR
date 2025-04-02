using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public GameObject correctOrgan; // Référence à la Rate (bonne réponse)
    public GameObject wrongOrgan; // Référence au Rein (mauvaise réponse)
    
    public Material correctMaterial; // Matériau vert
    public Material wrongMaterial; // Matériau rouge
    public Material defaultMaterial; // Matériau de base
    
    private Renderer correctRenderer;
    private Renderer wrongRenderer;

    private void Start()
    {
        correctRenderer = correctOrgan.GetComponent<Renderer>();
        wrongRenderer = wrongOrgan.GetComponent<Renderer>();
    }

    public void CheckAnswer(GameObject selectedOrgan)
    {
        if (selectedOrgan == correctOrgan)
        {
            correctRenderer.material = correctMaterial; // Change en vert
        }
        else if (selectedOrgan == wrongOrgan)
        {
            wrongRenderer.material = wrongMaterial; // Change en rouge
        }
    }

    public void ResetMaterials()
    {
        correctRenderer.material = defaultMaterial;
        wrongRenderer.material = defaultMaterial;
    }
}
