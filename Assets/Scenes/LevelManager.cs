using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const string WINNING_SCENE_NAME = "Winning Scene";
    private const string MAIN_MENU_SCENE_NAME = "Main Menu Scene";

    public static LevelManager Instance { get; private set; }
    public Player Winner { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayLevel() => SceneManager.LoadScene(FindObjectOfType<LevelSelection>(true).GetSelectedLevelName());

    public void LoadWinningScene()
    {
        AudioManager.Instance.DestroyAudioSources();
        SceneManager.LoadScene(WINNING_SCENE_NAME);
    }

    public void LoadMainMenuScene()
    {
        AudioManager.Instance.DestroyAudioSources();
        SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
