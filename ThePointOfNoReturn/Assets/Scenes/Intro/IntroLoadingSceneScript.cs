using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLoadingSceneScript : MonoBehaviour
{

    private void Awake()
    {
        StartCoroutine(LoadingScene());
        DontDestroyOnLoad(this);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    bool allowActivation = false;
    public void AnimationEndCallBack()
    {
        allowActivation = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(0.2f);
        allowActivation = false;
        var sceneLoading = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
        sceneLoading.allowSceneActivation = false;
        while (!allowActivation)
        {
            yield return new WaitForEndOfFrame();
        }
        sceneLoading.allowSceneActivation = true;

        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
    

}
