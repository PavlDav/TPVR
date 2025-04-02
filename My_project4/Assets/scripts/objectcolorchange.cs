using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectColorChange : MonoBehaviour
{
    public Material defaultMaterial;
    public Material grabbedMaterial;
    public Material correctMaterial;  // Pour l'état "correct"
    public Material incorrectMaterial;  // Pour l'état "incorrect"

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Vérifier si le composant existe avant d'ajouter les événements
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log($"Objet saisi par : {args.interactorObject.transform.gameObject.name}");

        // Parcourir tous les rendus d'enfants et appliquer le matériau de saisie
        SetChildMaterials(grabbedMaterial);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("Objet relâché, retour au matériau d'origine.");

        // Remettre le matériau par défaut sur les enfants
        SetChildMaterials(defaultMaterial);
    }

    private void SetChildMaterials(Material material)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer childRenderer in renderers)
        {
            if (childRenderer != null)
            {
                childRenderer.material = material;
            }
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    // Méthode pour changer la couleur en fonction de la bonne ou mauvaise saisie
    public void SetCorrect(bool isCorrect)
    {
        Material materialToApply = isCorrect ? correctMaterial : incorrectMaterial;
        SetChildMaterials(materialToApply);
    }
}
