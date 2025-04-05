using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public float delayBeforeLoad = 2f;
    private void Awake()
    {
        instance = this;
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        StartCoroutine(LoadSceneWithDelay(sceneIndex));
    }

    private System.Collections.IEnumerator LoadSceneWithDelay(int sceneIndex)
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(sceneIndex);
    }
}
