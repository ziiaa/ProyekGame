using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scene_loader : MonoBehaviour
{
    public Image loadingfill;
    public float minimumLoadingTime = 3f; // Minimum time the loading should take

    void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        float elapsedTime = 0f;
        AsyncOperation loading = SceneManager.LoadSceneAsync("Home");

        // Prevent the scene from activating until we allow it
        loading.allowSceneActivation = false;

        while (!loading.isDone)
        {
            // Calculate the progress based on elapsed time and actual loading progress
            float progress = Mathf.Clamp01(elapsedTime / minimumLoadingTime);
            loadingfill.fillAmount = Mathf.Lerp(loadingfill.fillAmount, progress, Time.deltaTime);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // If the operation is almost done and the minimum loading time has passed, allow the scene to activate
            if (loading.progress >= 0.9f && elapsedTime >= minimumLoadingTime)
            {
                loading.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
