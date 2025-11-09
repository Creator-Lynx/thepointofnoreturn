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

    bool allowSceneActivation = false;
    public void AnimationEndCallBack()
    {
        allowSceneActivation = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(4f);
        allowSceneActivation = false;
        var sceneLoading = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
        sceneLoading.allowSceneActivation = false;
        while (!allowSceneActivation)
        {
            yield return new WaitForEndOfFrame();
        }
        sceneLoading.allowSceneActivation = true;
        //SceneManager.UnloadSceneAsync(0);
    }
    

}
