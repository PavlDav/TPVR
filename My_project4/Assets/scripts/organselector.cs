using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OrganSelector : MonoBehaviour
{
    private QuizManager quizManager;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Start()
    {
        quizManager = FindObjectOfType<QuizManager>(); // Trouve le script QuizManager
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        quizManager.CheckAnswer(gameObject);
    }
}
