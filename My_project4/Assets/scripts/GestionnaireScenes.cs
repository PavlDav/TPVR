using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionnaireScenes : MonoBehaviour
{
    public void ChargerSceneParNom(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }
}