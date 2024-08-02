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
    public AudioSource AudioSource;
    public GameObject creditscanvas;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.Play();
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
    }

    public void creditsOnClick()
    {
        creditscanvas.SetActive(true);
        mainscreen.SetActive(false);
        playButton.SetActive(false);
        logo.SetActive(false);
        creditsbutton.SetActive(false);
    }

    public void backtomainmenu()
    {
        creditscanvas.SetActive(false);
        mainscreen.SetActive(true);
        playButton.SetActive(true);
        logo.SetActive(true);
        creditsbutton.SetActive(true);
    }
}
