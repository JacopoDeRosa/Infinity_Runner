using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Functions : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void CallReload()
    {
        ReloadableComponent[] reloadables = FindObjectsOfType<ReloadableComponent>();

        foreach (var reloadable in reloadables)
        {
            reloadable.Reload();
        }

    }
}
