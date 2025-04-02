using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowNameOnHover : MonoBehaviour
{
    [Header("Canvas d'affichage du nom")]
    // Assigne ici le Canvas (ou un GameObject contenant le Canvas)
    public GameObject nameCanvas;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Start()
    {
        // S'assurer que le Canvas est désactivé au démarrage
        if (nameCanvas != null)
            nameCanvas.SetActive(false);

        // Récupérer le composant interactable sur ce GameObject
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable != null)
        {
            // Ajouter des listeners aux événements de survol
            interactable.hoverEntered.AddListener(OnHoverEntered);
            interactable.hoverExited.AddListener(OnHoverExited);
        }
    }

    // Lorsque le rayon entre en survol
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (nameCanvas != null)
        {
            nameCanvas.SetActive(true);
            // Positionner le canvas juste au-dessus de l'objet (ajuste l'offset au besoin)
            nameCanvas.transform.position = transform.position + Vector3.up * 0.08f;
        }
    }

    // Lorsque le rayon quitte le survol
    private void OnHoverExited(HoverExitEventArgs args)
    {
        if (nameCanvas != null)
            nameCanvas.SetActive(false);
    }
}