using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
