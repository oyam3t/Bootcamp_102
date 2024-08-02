using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    private string path;
    public GameObject lvl2;
    public GameObject lvl3;
    public GameObject lvl4;
    public GameObject lvl5;
    public GameObject lvl6;
    public SpriteRenderer resim;
    public Sprite map0;
    public Sprite map1;
    public Sprite map2;
    public Sprite map3;
    public Sprite map4;
    public Sprite map5;
    public Sprite map6;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        path = Path.Combine(Application.persistentDataPath, "savedNumber.txt");
        if (File.Exists(path))
        {
            string numberString = File.ReadAllText(path);
            if(numberString == "2")
            {
                lvl2.SetActive(true);
                resim.sprite = map1;
            }
            else if (numberString == "3")
            {
                lvl2.SetActive(true);
                lvl3.SetActive(true);
                resim.sprite = map2;
            }
            else if (numberString == "4")
            {
                lvl2.SetActive(true);
                lvl3.SetActive(true);
                lvl4.SetActive(true);
                resim.sprite = map3;
            }
            else if (numberString == "5")
            {
                lvl2.SetActive(true);
                lvl3.SetActive(true);
                lvl4.SetActive(true);
                lvl5.SetActive(true);
                resim.sprite = map4;
            }
            else if (numberString == "6")
            {
                lvl2.SetActive(true);
                lvl3.SetActive(true);
                lvl4.SetActive(true);
                lvl5.SetActive(true);
                lvl6.SetActive(true);
                resim.sprite = map5;
            }
            else if (numberString == "6")
            {
                lvl2.SetActive(true);
                lvl3.SetActive(true);
                lvl4.SetActive(true);
                lvl5.SetActive(true);
                lvl6.SetActive(true);
                resim.sprite = map6;
            }
            else
            {
                //sadece 1 açık
                resim.sprite = map0;
            }
        }
    }

    public void resetDataOnClick()
    {
        File.WriteAllText(path, "1");
        SceneManager.LoadScene(sceneName: "Levelselect");
    }

    public void backOnClick()
    {
        SceneManager.LoadScene(sceneName: "Start");
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
