﻿using UnityEngine;
using System.Collections;

public class ResetSettingsScript : MonoBehaviour {

	public GameObject education;
	public GameObject therapy;
	public UIToggle highlightCheck;
	public UIToggle typingCheck;
	public UIToggle voiceCheck;
	public UIToggle educationCheck;
	public UIToggle therapyCheck;
    public UISlider miniGameSpeedSlider;
    public UILabel miniGameTurns;
    public CoinTypeMenu ctm;

    public UISprite educationBack;
	public UISprite therapyBack;
	
	public UISprite playBackground; 

	public UISlider printSlider;
	
	private Color orig;
	private Color half;


	// Use this for initialization
	void Start () {
		orig = playBackground.color;
		half = new Color(playBackground.color.r/2, playBackground.color.g/2, playBackground.color.b/2);
	}

	public void OnClick() {
        //PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("firstTime", 1); //needed to prevent game from resetting PlayerPrefs again on restart; see: setPrefs.cs

        highlightCheck.value = true;
		typingCheck.value = false;
		voiceCheck.value = true;
		PlayerPrefs.SetInt("highlight",0);
		PlayerPrefs.SetInt("typing",0);
		PlayerPrefs.SetInt("voice",0);
        PlayerPrefs.SetInt("minigames", 1);
        PlayerPrefs.SetInt("tutorial", 0);
        PlayerPrefs.SetInt("tutorialMiniGame1", 0);
        PlayerPrefs.SetInt("tutorialMiniGame2", 0);
        PlayerPrefs.SetInt("tutorialMiniGame3", 0);
        PlayerPrefs.SetInt("tutorialMiniGameStory1", 0);
        PlayerPrefs.SetInt("tutorialMiniGameStory2", 0);
        PlayerPrefs.SetInt("tutorialMiniGameStory3", 0);
        PlayerPrefs.SetFloat("speedOfLabel", 1f);
        PlayerPrefs.SetInt("MiniGameTutorial", 0);
        PlayerPrefs.SetInt("levelSelect", 0);
        PlayerPrefs.SetFloat("miniGameSpeed", 0.5f);
        miniGameSpeedSlider.value = PlayerPrefs.GetFloat("miniGameSpeed");
        PlayerPrefs.SetInt("miniGameTurns", 3);
        miniGameTurns.text = PlayerPrefs.GetInt("miniGameTurns").ToString();
        PlayerPrefs.SetInt("pennyOn", 1);
        PlayerPrefs.SetInt("nickelOn", 1);
        PlayerPrefs.SetInt("dimeOn", 1);
        PlayerPrefs.SetInt("quarterOn", 1);
        PlayerPrefs.SetInt("halfOn", 1);
        ctm.UpdateToggleValues();

        educationCheck.value = true;
		educationBack.color = orig;
        //education.GetComponent<Collider>().enabled = true;
        education.GetComponentInChildren<Collider>().enabled = true;
		PlayerPrefs.SetInt("educationOn",1);

		therapyCheck.value = false;
		therapyBack.color = half;
		therapy.GetComponent<Collider>().enabled = false;
		PlayerPrefs.SetInt("therapyOn",0);

		printSlider.value = 0.5f;
		PlayerPrefs.SetFloat("printSize",printSlider.value);

        PlayerPrefs.Save();
        //Debug.Log("Prefs Reset");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
