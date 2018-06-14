﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayLevel : MonoBehaviour {

	public float timer = 0.1f;
	public string levelToLoad = "1stScene";
	private bool on = true;
    //private bool tutorialMode;

    //public bool storyMode;

	private bool key3Press = false;
	private bool key2Press = false;
	private bool key1Press = false;
	private bool keyspacePress = false;
	private bool keyenterPress = false;

    private bool objectApp = false;
    private bool loading = false;

	//public UIPanel loadingScreen;
	public UIPanel frontPanel;

	private Event e;

    //LoadingScreen ls;

	#if UNITY_IOS
	void iCadeStateCallback(int state)
	{
		print("iCade state change. Current state="+state);
	}
	
	/// <summary>
	/// This will be called whenever there's a button up event in iCade. It will get called for buttons and axis, since axis movement also translates into key presses
	/// </summary>
	/// <param name="button"></param>
	void iCadeButtonUpCallback(char button)
	{
		print("Button up event. Button " + button + " up");
	}
	
	/// <summary>
	/// This will be called whenever there's a button down event in iCade. It will get called for buttons and axis, since axis movement also translates into key presses
	/// </summary>
	/// <param name="button"></param>
	void iCadeButtonDownCallback(char button)
	{
		print("Button down event. Button " + button + " down");
		if (button == 'w') {
			key1Press = true;
		} 
		if (button == 'x') {
			key2Press = true;
		}
		if (button == 'd') {
			key3Press = true;
		}
		if (button == 'a') {
			keyspacePress = true;
		}
		if (button == 'y') {
			keyenterPress = true;
		}
		objectApp = true;
	}

	void iCadeKeyPressedCallback(int i)
	{
		
	}
	#endif
	public void switching() {
		on = !on;
	}
	// Use this for initialization
	void Start () {
	#if UNITY_IOS
		iCadeInput.Activate(true);
		
		//Register some callbacks
		iCadeInput.AddICadeEventCallback(iCadeStateCallback);
		iCadeInput.AddICadeButtonUpCallback(iCadeButtonUpCallback);
		iCadeInput.AddICadeButtonDownCallback(iCadeButtonDownCallback);
	#endif

		e = Event.current;

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

            if (!loading)
            {
                SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
                Resources.UnloadUnusedAssets();

                StartCoroutine(GoToScene(levelToLoad));
                loading = true;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

		//test.text = e.keyCode.ToString();

		if (Input.GetKeyDown("1") == true) {
			key1Press = true;
			objectApp = true;
		}
		if (Input.GetKeyDown("2") == true) {
			key2Press = true;
			objectApp = true;
		}
		if (Input.GetKeyDown("3") == true) {
			key3Press = true;
			objectApp = true;
		}
		if (Input.GetKeyDown("space") == true) {
			keyspacePress = true;
			objectApp = true;
		}
		if (e != null) {
		if (e.keyCode.ToString() == "10" && e.type == EventType.keyDown) {
				//Debug.Log("hello");
//		if (Input.GetKeyDown(KeyCode.Return)) {
		    keyenterPress = true;
			objectApp = true;
		}
			}

		if ((PlayerPrefs.GetInt("educationOn") == 1) || (PlayerPrefs.GetInt("therapyOn") == 1)) {
			if ((PlayerPrefs.GetInt("key1toggle") == 0) && key1Press) {
				OnClick();
			}
			if ((PlayerPrefs.GetInt("key2toggle") == 0) && key2Press) {
				OnClick();
			}
			if ((PlayerPrefs.GetInt("key3toggle") == 0) && key3Press) {
				OnClick();
			}
			if ((PlayerPrefs.GetInt("keySpacetoggle") == 0) && keyspacePress) {
				OnClick();
			}
			if ((PlayerPrefs.GetInt("keyEntertoggle") == 0) && keyenterPress) {
				OnClick();
			}
			key1Press = false;
			key2Press = false;
			key3Press = false;
			keyspacePress = false;
			keyenterPress = false;
		}
		if (objectApp && (((PlayerPrefs.GetInt("educationOn") == 1) && (PlayerPrefs.GetInt("therapyOn") == 1)) 
		                  || ((PlayerPrefs.GetInt("educationOn") == 0) && (PlayerPrefs.GetInt("therapyOn") == 0)))) {
			OnClick ();
			objectApp = false;
			
		} else {
			objectApp = false;
		}
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
