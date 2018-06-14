using UnityEngine;
using System.Collections;

public class setPrefs : MonoBehaviour {

    void Awake()
    {
        if (PlayerPrefs.GetInt("firstTime") == 0)
        {
            PlayerPrefs.DeleteAll();

            PlayerPrefs.SetInt("firstTime", 1);

            PlayerPrefs.SetInt("highlight", 0);
            PlayerPrefs.SetInt("typing", 0);
            PlayerPrefs.SetInt("voice", 0);
            PlayerPrefs.SetInt("minigames", 1);

            PlayerPrefs.SetInt("educationOn", 1);
            PlayerPrefs.SetInt("therapyOn", 0);

            PlayerPrefs.SetInt("tutorial", 0);
            PlayerPrefs.SetInt("tutorialMiniGame1", 0);
            PlayerPrefs.SetInt("tutorialMiniGame2", 0);
            PlayerPrefs.SetInt("tutorialMiniGame3", 0);
            PlayerPrefs.SetInt("tutorialMiniGameStory1", 0);
            PlayerPrefs.SetInt("tutorialMiniGameStory2", 0);
            PlayerPrefs.SetInt("tutorialMiniGameStory3", 0);
            PlayerPrefs.SetFloat("speedOfLabel", 1f);
            PlayerPrefs.SetInt("MiniGameTutorial", 0);
            PlayerPrefs.SetInt("difficulty", 0);
            PlayerPrefs.SetInt("miniGameTurns", 3);
            PlayerPrefs.SetFloat("miniGameSpeed", 0.5f);

            PlayerPrefs.SetInt("pennyOn", 1);
            PlayerPrefs.SetInt("nickelOn", 1);
            PlayerPrefs.SetInt("dimeOn", 1);
            PlayerPrefs.SetInt("quarterOn", 1);
            PlayerPrefs.SetInt("halfOn", 1);

            PlayerPrefs.Save();
        }

        PlayerPrefs.SetInt("miniGameTurns", 3);
        PlayerPrefs.SetFloat("miniGameSpeed", 0.5f);

        PlayerPrefs.SetInt("pennyOn", 1);
        PlayerPrefs.SetInt("nickelOn", 1);
        PlayerPrefs.SetInt("dimeOn", 1);
        PlayerPrefs.SetInt("quarterOn", 1);
        PlayerPrefs.SetInt("halfOn", 1);
    }

	// Use this for initialization
	void Start ()
    {        
    }
	
	// Update is called once per frame
	void Update () {	
	}
}
