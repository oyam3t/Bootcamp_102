using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void level1onclick()
    {
        SceneManager.LoadScene(sceneName: "level1");
    }
    public void level2onclick()
    {
        SceneManager.LoadScene(sceneName: "level2");
    }
    public void level3onclick()
    {
        SceneManager.LoadScene(sceneName: "level3");
    }
    public void level4onclick()
    {
        SceneManager.LoadScene(sceneName: "level4");
    }
    public void level5onclick()
    {
        SceneManager.LoadScene(sceneName: "level5");
    }
    public void level6onclick()
    {
        SceneManager.LoadScene(sceneName: "level6");
    }
}
