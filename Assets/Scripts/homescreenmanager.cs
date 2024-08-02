using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class homescreenmanager : MonoBehaviour
{
    public GameObject loadingscreen;
    public GameObject mainscreen;
    public GameObject playButton;
    public GameObject logo;
    public GameObject creditsbutton;
    

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine(wait());
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        Destroy(loadingscreen);
        mainscreen.SetActive(true);
        playButton.SetActive(true);
        logo.SetActive(true);
        creditsbutton.SetActive(true);
    }

    public void playButtonOnClick()
    {

        Debug.Log("play button");

        SceneManager.LoadScene(sceneName: "Levelselect");
        //DİĞER SAHNEYE GEÇ
    }

    public void creditsOnClick()
    {

    }
}
