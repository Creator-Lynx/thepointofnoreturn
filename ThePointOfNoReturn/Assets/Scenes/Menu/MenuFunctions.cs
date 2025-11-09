using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public void StartGrayBoxScene()
    {
        SceneManager.LoadScene("GrayBox");
    }
}
