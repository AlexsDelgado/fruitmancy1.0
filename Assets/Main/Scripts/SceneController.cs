using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController: MonoBehaviour
{
    public static SceneController instance;
    
    

    //private int numeroAleatorio;

    private void Start()
    {
        // numeroAleatorio = Random.Range(0,5);
        //playerManager.player_Instance.muerte.AddListener(Defeat);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
   
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void retry()
    {
        SceneManager.LoadScene("GameplayScene ok");
        GameManager.Instance.Continue();
    }
    public void jugar()
    {
        //retry();
        SceneManager.LoadScene("GameplayScene ok");
        Time.timeScale = 1;
    }
    public void Defeat()
    {
        SceneManager.LoadScene("Defeat");
    }
   
    public void Victory()
    {

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
