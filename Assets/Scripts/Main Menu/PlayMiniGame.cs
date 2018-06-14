using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMiniGame : MonoBehaviour {

    public float timer = 0.1f;
    public string levelToLoad = "1stScene";
    private bool on = true;

    //public UILabel loadingLabel;
    public UIPanel frontPanel;

    //LoadingScreen ls;

    public void switching()
    {
        on = !on;
    }
    // Use this for initialization
    void Start()
    {
        //ls = GameObject.Find("LoadingScreen").GetComponent<LoadingScreen>();

        //if (PlayerPrefs.GetInt("MiniGameTutorial") == 0)
        //    tutorialMode = true;
        //else
        //    tutorialMode = false;
    }

    public void OnClick()
    {
        if (on)
        {
            frontPanel.alpha = 0f;
            SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            Resources.UnloadUnusedAssets();

            StartCoroutine(GoToScene(levelToLoad));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GoToScene(string sceneName)
    {
        PlayerPrefs.SetInt("difficulty", 0);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadSceneAsync(sceneName);

        if (SceneManager.GetActiveScene().name == "LoadingScreen")
            SceneManager.UnloadSceneAsync("LoadingScreen");
    }

}
