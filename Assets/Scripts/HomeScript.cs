using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScript : MonoBehaviour
{

    public void MainScene()
    {
        SceneManager.LoadSceneAsync("Main Scene");
    }

    public void Level1()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void Level2()
    {
        SceneManager.LoadSceneAsync("Level 2");
    }
    public void Level3()
    {
        SceneManager.LoadSceneAsync("Level 3");
    }
    public void Level4()
    {
        SceneManager.LoadSceneAsync("Level 4");
    }
    public void Level5()
    {
        SceneManager.LoadSceneAsync("Level 5");
    }

    
}
