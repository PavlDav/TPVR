using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitboxSnap : MonoBehaviour
{
    public string targetOrganTag;
    public Transform snapPosition;
    public ParticleSystem snapParticleEffect;
    public AudioSource snapSoundEffect;

    public Transform[] basePositions; // Slots de base pour chaque organe

    private Renderer hitboxRenderer;
    private GameObject organInZone;
    private Coroutine snapCoroutine;
    private ScoreManager scoreManager;
    private Collider hitboxCollider;

    private Dictionary<GameObject, Transform> organBasePositions = new Dictionary<GameObject, Transform>();

    private void Start()
    {
        hitboxRenderer = GetComponent<Renderer>();
        hitboxCollider = GetComponent<Collider>();
        SetAlpha(hitboxRenderer.material.color.a);
        scoreManager = FindObjectOfType<ScoreManager>();

        if (snapParticleEffect != null)
        {
            snapParticleEffect.Stop();
        }

        AssignBasePositions(); // Associe les organes à leurs positions de base
    }

    private void AssignBasePositions()
    {
        GameObject[] organs = GameObject.FindGameObjectsWithTag(targetOrganTag);
        
        if (organs.Length != basePositions.Length)
        {
            Debug.LogError("Le nombre de slots de base ne correspond pas au nombre d'organes !");
            return;
        }

        for (int i = 0; i < organs.Length; i++)
        {
            organBasePositions[organs[i]] = basePositions[i];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetOrganTag))
        {
            hitboxRenderer.material.color = new Color(0f, 1f, 0f, 0.2f);
            organInZone = other.gameObject;
            snapCoroutine = StartCoroutine(SnapAfterDelay(2f, organInZone));
        }
        else
        {
            hitboxRenderer.material.color = new Color(1f, 0f, 0f, 0.3f);
            organInZone = other.gameObject;
            snapCoroutine = StartCoroutine(DeductPointsAfterDelay(2f, organInZone));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetOrganTag) || organInZone == other.gameObject)
        {
            hitboxRenderer.material.color = new Color(1f, 0f, 0f, 0.3f);
            organInZone = null;

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

        if (organInZone == organ)
        {
            SnapOrgan(organ);
            scoreManager.AddPoints(10);

            if (snapParticleEffect != null)
            {
                snapParticleEffect.transform.position = snapPosition.position;
                snapParticleEffect.Play();
            }

            if (snapSoundEffect != null)
            {
                snapSoundEffect.Play();
            }

            DisableColliders(organ);
            if (hitboxCollider != null)
            {
                hitboxCollider.enabled = false;
            }
        }
    }

    private IEnumerator DeductPointsAfterDelay(float delay, GameObject organ)
    {
        yield return new WaitForSeconds(delay);

        if (organInZone == organ)
        {
            scoreManager.AddPoints(-10);
            ResetOrganToBase(organ);
        }
    }

    private void SnapOrgan(GameObject organ)
    {
        organ.transform.position = snapPosition.position;
        organ.transform.rotation = snapPosition.rotation;

        Rigidbody rb = organ.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        var grabInteractable = organ.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }
    }

   private void ResetOrganToBase(GameObject organ)
{
    if (organBasePositions.ContainsKey(organ) && organBasePositions[organ] != null)
    {
        Transform baseTransform = organBasePositions[organ];
        organ.transform.position = baseTransform.position;
        organ.transform.rotation = baseTransform.rotation;

        Rigidbody rb = organ.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    else
    {
        Debug.LogWarning($"Aucune position de base trouvée pour {organ.name}. Vérifie les assignations dans l'Inspector.");
    }
}


    private void DisableColliders(GameObject organ)
    {
        Collider organCollider = organ.GetComponent<Collider>();
        if (organCollider != null)
        {
            organCollider.enabled = false;
        }
    }

    private void SetAlpha(float currentAlpha)
    {
        Color currentColor = hitboxRenderer.material.color;
        hitboxRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentAlpha);
    }
}
