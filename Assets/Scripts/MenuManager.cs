using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject endMenu = null;

    public void StartGame()
    {
        SceneManager.LoadScene("Race");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void DisplayEndMenu()
    {
        if (endMenu == null)
        {
            Debug.LogError("EndMenu not assigned");
            return;
        }
        endMenu.SetActive(true);
    }
}
