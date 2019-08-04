using System.Collections;
using UnityEngine;
using UnityEngine.UI; // include UI namespace so can reference UI elements
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager


public class GameManager : MonoBehaviour
{

    // static reference to game manager so can be called from other scripts directly (not just through gameobject component)
    public static GameManager gm;

    [SerializeField]
    private GameObject player;
    bool isPaused = false;


    // levels to move to on victory and lose
    public string nextLevel;
    public string levelRestart;

    // UI elements to control
    public GameObject UIGamePaused;
    public GameObject VictoryPanel;
    public GameObject TransitionPanel;

    Color originalColour;


    // set things up here
    void Awake()
    {
        originalColour = TransitionPanel.GetComponent<Image>().color;
        StartCoroutine(FadeIn());
        //MakeSingleton();
        // setup reference to game manager
        if (gm == null)
            gm = this.GetComponent<GameManager>();  
    }
    public IEnumerator FadeIn()
    {
        TransitionPanel.GetComponent<Image>().color = Color.black;
        float t = 0f;
        while(t <1)
        {
            TransitionPanel.GetComponent<Image>().color = Color.Lerp(Color.black, originalColour, t);
            t += .5f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        TransitionPanel.SetActive(false);
    }
    // game loop
    void Update()
    {
        // if ESC pressed then pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            try
            {
                CheckPauseState();
            }
            catch
            {
                Debug.Log("NotInTheMainMenu");
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            LevelRestart();
        }
    }

    public void GameOver()
    {
        VictoryPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    // public function for level complete
    public void LoadNextScene()
    {

        // use a coroutine to allow the player to get fanfare before moving to next level
        StartCoroutine(LoadNextLevel());
    }

    // load the nextLevel after delayk
    IEnumerator LoadNextLevel()
    {
        TransitionPanel.SetActive(true);
        float t = 0f;
        while (t < 1)
        {
            TransitionPanel.GetComponent<Image>().color = Color.Lerp(originalColour, Color.black, t);
            t += .5f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }        
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(nextLevel);
    }

    public void LevelRestart()
    {
        Cursor.lockState = CursorLockMode.None;
        //StartCoroutine(LoadCurrentLevel());
        SceneManager.LoadScene(levelRestart);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator LoadCurrentLevel()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(levelRestart);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = true;
    }

    public  void CheckPauseState()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            UnPauseGame();
        }
    }

    void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        UIGamePaused.SetActive(true); // this brings up the pause UI
        Time.timeScale = 0f; // this pauses the game action
        Cursor.visible = true;
    }

    void UnPauseGame()
    {
        Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
        UIGamePaused.SetActive(false); // remove the pause UI
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
