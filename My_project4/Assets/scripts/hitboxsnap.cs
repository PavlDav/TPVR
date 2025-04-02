using UnityEngine;
using System.Collections;

public class HitboxSnap : MonoBehaviour
{
    public string targetOrganTag; // Tag de l'organe correspondant
    public Transform snapPosition; // Position exacte où l'organe doit se placer
    public GameObject snapParticleEffect; // Particle effect to instantiate on snap
    private Renderer hitboxRenderer;
    private GameObject organInZone; // Stocke l’organe dans la hitbox
    private Coroutine snapCoroutine; // Stocke la coroutine en cours
    private ScoreManager scoreManager; // Référence au ScoreManager

    private void Start()
    {
        hitboxRenderer = GetComponent<Renderer>();
        SetAlpha(hitboxRenderer.material.color.a);

        // Trouver le ScoreManager dans la scène
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetOrganTag))
        {
            hitboxRenderer.material.color = new Color(0f, 1f, 0f, 0.2f); // Vert avec alpha 0.3
            organInZone = other.gameObject;

            // Lancer le timer de 2s pour le snap
            snapCoroutine = StartCoroutine(SnapAfterDelay(2f, organInZone));
        }
        else
        {
            hitboxRenderer.material.color = new Color(1f, 0f, 0f, 0.3f); // Rouge avec alpha 0.3
            organInZone = other.gameObject;

            // Lancer le timer de 2s pour déduire des points
            snapCoroutine = StartCoroutine(DeductPointsAfterDelay(2f, organInZone));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetOrganTag) || organInZone == other.gameObject)
        {
            hitboxRenderer.material.color = new Color(1f, 0f, 0f, 0.3f); // Rouge avec alpha 0.3
            organInZone = null;

            // Annuler le snap ou la déduction de points si l’objet sort avant 2 secondes
            if (snapCoroutine != null)
            {
                StopCoroutine(snapCoroutine);
                snapCoroutine = null;
            }
        }
    }

    private IEnumerator SnapAfterDelay(float delay, GameObject organ)
    {
        yield return new WaitForSeconds(delay);

        // Vérifier si l’objet est toujours dans la hitbox
        if (organInZone == organ)
        {
            SnapOrgan(organ);
            // Ajouter des points une fois l'organe snap
            scoreManager.AddPoints(10); // Ajoute 10 points (modifiable selon ta logique)

            // Instantiate the particle effect
            if (snapParticleEffect != null)
            {
                Instantiate(snapParticleEffect, snapPosition.position, snapPosition.rotation);
            }
        }
    }

    private IEnumerator DeductPointsAfterDelay(float delay, GameObject organ)
    {
        yield return new WaitForSeconds(delay);

        // Vérifier si l’objet est toujours dans la hitbox
        if (organInZone == organ)
        {
            // Deduct points for the wrong organ
            scoreManager.AddPoints(-10); // Deduct 10 points
        }
    }

    private void SnapOrgan(GameObject organ)
    {
        organ.transform.position = snapPosition.position;
        organ.transform.rotation = snapPosition.rotation;

        // Désactiver le mouvement physique
        Rigidbody rb = organ.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Désactiver XRGrabInteractable pour empêcher de le reprendre
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = organ.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }
    }

    private void SetAlpha(float currentAlpha)
    {
        Color currentColor = hitboxRenderer.material.color;
        hitboxRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentAlpha);
    }
}
