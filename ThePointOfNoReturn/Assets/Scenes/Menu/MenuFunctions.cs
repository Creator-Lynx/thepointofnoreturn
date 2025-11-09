using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public void StartGrayBoxScene()
    {
        Destroy(GameObject.FindAnyObjectByType<IntroLoadingSceneScript>().gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("GrayBox");
        
    }
}
