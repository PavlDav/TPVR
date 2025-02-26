using UnityEngine;

using TMPro; // Si tu utilises du texte

public class ObjectInfoDisplay : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor; // Référence au XR Ray Interactor du contrôleur
    public GameObject popupUI; // Référence au menu pop-up
    public TextMeshProUGUI popupText; // Texte du pop-up

    private void Update()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                popupUI.SetActive(true);
                popupText.text = hit.collider.gameObject.name; // Affiche le nom de l'objet
                popupUI.transform.position = hit.point + Vector3.up * 0.2f; // Positionne au-dessus de l'objet
            }
        }
        else
        {
            popupUI.SetActive(false); // Cache le pop-up si rien n’est visé
        }
    }
}
