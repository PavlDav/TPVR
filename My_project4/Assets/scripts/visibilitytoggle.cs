using UnityEngine;
using UnityEngine.InputSystem;


public class visibilitytoggle    : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor;  // L'interacteur pour pointer les objets
    public InputActionProperty selectAction; // L'input de la gâchette

    private void Update()
    {
        if (selectAction.action.WasPressedThisFrame()) 
        {
            ToggleObjectVisibility();
        }
    }

    private void ToggleObjectVisibility()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            GameObject target = hit.transform.gameObject;
            target.SetActive(!target.activeSelf); // Change la visibilité
            
        }
    }
}
