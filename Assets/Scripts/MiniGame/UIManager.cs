using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    Test storyScript;
    GameManager gm;
    //Camera mainCam;
    public static bool paused = false;
    bool tutorialSet = false;

    [Header("UI Buttons")]
    public UISlider speedSlider;
    public UISprite pauseButton;
    public UISprite playButton;

    [Header("UI Panels")]
    public UIPanel pauseMenu;
    public UISprite tutorialMenu;
    public UILabel tutorialText;
    string level1Text = "Step 1. Match the coin to the correct amount. \nStep 2. Watch for the coin. \nStep 3. Tap to drop the coin.";
    string level2Text = "Step 1. Add up to a dollar with your coins. \nStep 2. Watch for the coin. \nStep 3. Tap to drop the coin.";
    string level3Text = "Step 1. Add up to the correct amount with your coins. \nStep 2. Watch for the coin. \nStep 3. Tap to drop the coin.";
    public UISprite levelSelectMenu;
    public UISprite endMenu;

    [Header("UI Elements")]
    //public UISprite correct;
    //public UISprite incorrect;
    public UILabel turns;
    public UILabel currentCoinValue;
    public UILabel targetCoinValue;
    //public UILabel correctNum;
    //public UILabel incorrectNum;

    [Header("Scoring")]
    public UILabel correctLabel;
    public UILabel incorrectLabel;
    public UISprite star1;
    public UISprite star2;
    public UISprite star3;

    //[Header("Loading Screen")]
    //public UITexture loadingPanel;
    //LoadingScreen ls;

    bool key1Press = false;
    bool key2Press = false;
    bool key3Press = false;
    bool keyspacePress = false;
    bool keyenterPress = false;
    bool objectApp = false;
    Event e;

    /// <summary>
    /// This will be called whenever the iCade state changes i.e. it will get called for ALL events
    /// </summary>
    /// <param name="state"></param>
    /// 
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
		//print ("Any old key pressed from Scan Mode");
//		if (!trying && !comparing)
//			trying = true;
	}
#endif

    private void Awake()
    {
        star1.alpha = 0f;
        star2.alpha = 0f;
        star3.alpha = 0f;
    }

    // Use this for initialization
    void Start()
    {
#if UNITY_IOS
		iCadeInput.Activate(true);

		//Register some callbacks
		iCadeInput.AddICadeEventCallback(iCadeStateCallback);
		iCadeInput.AddICadeButtonUpCallback(iCadeButtonUpCallback);
		iCadeInput.AddICadeButtonDownCallback(iCadeButtonDownCallback);
#endif

        storyScript = GameObject.FindObjectOfType<Test>();
        gm = GameObject.FindObjectOfType<GameManager>();

        turns.text = "";
        e = Event.current;

        pauseMenu.alpha = 0f;
        tutorialMenu.alpha = 0f;
        endMenu.alpha = 0f;

        paused = false;

        speedSlider.value = PlayerPrefs.GetFloat("miniGameSpeed");

        if (gm.difficulty == GameManager.Difficulty.Unselected)
        {
            turns.alpha = 0;
            levelSelectMenu.alpha = 1f;
            pauseButton.alpha = 0f;
            speedSlider.alpha = 0f;
        }
        else
        {
            tutorialSet = true;
            turns.alpha = 1f;
            levelSelectMenu.alpha = 0f;
            pauseButton.alpha = 1f;
            speedSlider.alpha = 1f;
            //gm.StartGame(gm.difficulty);
        }

        UpdateTurns(gm.turnCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1") == true || key1Press)
        {
            objectApp = true;
        }
        if (Input.GetKeyDown("2") == true || key2Press)
        {
            objectApp = true;
        }
        if (Input.GetKeyDown("3") == true || key3Press)
        {
            objectApp = true;
        }
        if (Input.GetKeyDown("space") == true || keyspacePress)
        {
            objectApp = true;
        }
        if (e != null)
        {
            if (e.keyCode.ToString() == "10" && e.type == EventType.keyDown || keyenterPress)
            {
                objectApp = true;
            }
        }

        if (GameManager.gameOver && !paused)
        {
            currentCoinValue.text = "Winner!";
            targetCoinValue.text = "Complete!";
            turns.text = gm.turnCount.ToString();
            correctLabel.text = "Correct: " + gm.correctNum.ToString();
            incorrectLabel.text = "Incorrect: " + gm.incorrectNum.ToString();
            endMenu.alpha = 1f;
            pauseButton.alpha = 0f;
            speedSlider.alpha = 0f;
            StartCoroutine(starGrow());

            if (objectApp)
            {
                if (gm.isMainMenuGame)
                {
                    ReplayButton();
                }
                else
                {
                    ContinueStoryButton();
                }
            }
        }
        else if (gm.difficulty == GameManager.Difficulty.Unselected && !GameManager.gameOver)
        {
            if (objectApp)
            {
                LevelSelectBeginner_Button();
            }
        }
        else if (!tutorialSet)//tutorialMenu.alpha == 1f)
        {
            if (objectApp)
            {
                ContinueTutorialButton();
            }
        }


        key1Press = false;
        key2Press = false;
        key3Press = false;
        keyspacePress = false;
        keyenterPress = false;
        objectApp = false;
    }


    //===UI Updates===//
    public void UpdateCoinValue()
    {
        currentCoinValue.text = "Current Coin: " + gm.currentCoin.GetComponent<CoinControler>().coinTypes.ToString();
        if (gm.difficulty == GameManager.Difficulty.Beginner)
        {
            if (gm.targetAmount >= 100)
                targetCoinValue.text = "Target Amount: $" + gm.targetAmount / 100 + ".00";// + gm.targetAmount.ToString();
            else if (gm.targetAmount >= 10)
                targetCoinValue.text = "Target Amount: $0." + gm.targetAmount.ToString();
            else
                targetCoinValue.text = "Target Amount: $0.0" + gm.targetAmount.ToString();
        }
        else if (gm.difficulty == GameManager.Difficulty.Advanced)
        {
            targetCoinValue.text = "Add coins to equal $1.00!";
        }
        else
            targetCoinValue.text = "Add coins to reach each amount!";
        UpdateTurns(gm.turnCount);
    }

    public void UpdateTurns(int turnNum)
    {
        turns.text = turnNum.ToString();
    }

    IEnumerator starGrow()
    {
        if (gm.incorrectNum < gm.correctNum)
            star1.alpha = 1f;
        yield return new WaitForSeconds(0.5f);
        if (gm.incorrectNum <= gm.correctNum / 2)
            star2.alpha = 1f;
        yield return new WaitForSeconds(0.5f);
        if (gm.incorrectNum == 0)
            star3.alpha = 1f;
    }

    private void SetTutorialText()
    {
        switch (gm.difficulty)
        {
            case GameManager.Difficulty.Beginner:
                tutorialText.text = level1Text;
                break;
            case GameManager.Difficulty.Advanced:
                tutorialText.text = level2Text;
                break;
            case GameManager.Difficulty.Expert:
                tutorialText.text = level3Text;
                break;
            default:
                break;
        }
    }


    //===Button Controls===//
    public void PauseButton()
    {
        paused = true;
        Time.timeScale = 0;
        pauseButton.alpha = 0f;
        speedSlider.alpha = 0f;
        pauseMenu.alpha = 1f;
        //Debug.Log("paused");
    }

    public void ResumeButton()
    {
        paused = false;
        Time.timeScale = 1f;
        pauseButton.alpha = 1f;
        speedSlider.alpha = 1f;
        pauseMenu.alpha = 0f;
        //Debug.Log("resumed");
    }

    public void HomeButton()
    {
        GameManager.gameOver = true;
        Time.timeScale = 1f;
        pauseMenu.alpha = 0f;
        endMenu.alpha = 0f;

        SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
        StartCoroutine(GoToScene("MenuScreen"));
    }

    public void ReplayButton()
    {
        Time.timeScale = 1f;
        endMenu.alpha = 0f;
        if (!gm.isMainMenuGame)
        {
            SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            StartCoroutine(GoToScene("MiniGameScreen_Story"));
        }
        else
        {
            SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            StartCoroutine(GoToScene("MiniGameScreen"));
        }
    }

    public void ContinueStoryButton()
    {
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);

        GameManager.gameOver = true;
        Time.timeScale = 1.0f;
        pauseMenu.alpha = 0f;
        endMenu.alpha = 0f;
        storyScript.EndMiniGame();
    }

    public void ContinueTutorialButton()
    {
        //set tutorial to complete
        if (gm.isMainMenuGame)
        {
            switch (gm.difficulty)
            {
                case GameManager.Difficulty.Beginner:
                    PlayerPrefs.SetInt("tutorialMiniGame1", 1);
                    break;
                case GameManager.Difficulty.Advanced:
                    PlayerPrefs.SetInt("tutorialMiniGame2", 1);
                    break;
                case GameManager.Difficulty.Expert:
                    PlayerPrefs.SetInt("tutorialMiniGame3", 1);
                    break;
                default:
                    Debug.Log("Did not find a difficulty level");
                    break;
            }
        }
        else
        {
            switch (gm.difficulty)
            {
                case GameManager.Difficulty.Beginner:
                    PlayerPrefs.SetInt("tutorialMiniGameStory1", 1);
                    break;
                case GameManager.Difficulty.Advanced:
                    PlayerPrefs.SetInt("tutorialMiniGameStory2", 1);
                    break;
                case GameManager.Difficulty.Expert:
                    PlayerPrefs.SetInt("tutorialMiniGameStory3", 1);
                    break;
                default:
                    Debug.Log("Did not find a difficulty level");
                    break;
            }
        }
        tutorialMenu.alpha = 0f;
        tutorialSet = true;
        //levelSelectMenu.alpha = 1f;
        turns.alpha = 1f;
        pauseButton.alpha = 1f;
        speedSlider.alpha = 1f;
        gm.StartGame(gm.difficulty);
    }

    public void SpeedSlider()
    {
        //PlayerPrefs.SetFloat("miniGameSpeed", speedSlider.value);
        speedSlider.alpha = 1f;
        gm.gameSpeed = speedSlider.value + 0.25f; //PlayerPrefs.GetFloat("miniGameSpeed") + 0.25f;

        //bool fastMode = gm.fastMode;

        //if (fastMode)
        //{
        //    speedSlider.GetComponent<UISprite>().spriteName = "Walk";
        //    Debug.Log("walk");
        //}
        //else
        //{
        //    speedSlider.GetComponent<UISprite>().spriteName = "Run";
        //    Debug.Log("run");
        //}

        //fastMode = !fastMode;
        //gm.fastMode = fastMode;
    }

    public void PlayButton()
    {
        if (gm.difficulty != GameManager.Difficulty.Unselected && Time.timeScale > 0)
            Debug.Log("Play game");
    }

    public void LevelSelectBeginner_Button()
    {
        Debug.Log("Beginner");
        gm.difficulty = GameManager.Difficulty.Beginner;
        PlayerPrefs.SetInt("difficulty", 1);
        levelSelectMenu.alpha = 0f;
        if ((gm.isMainMenuGame && PlayerPrefs.GetInt("tutorialMiniGame1") == 0) || (!gm.isMainMenuGame && PlayerPrefs.GetInt("tutorialMiniGameStory1") == 0))
        {
            tutorialMenu.alpha = 1f;
            SetTutorialText();
        }
        else
        {
            tutorialSet = true;
            turns.alpha = 1f;
            pauseButton.alpha = 1f;
            speedSlider.alpha = 1f;
            gm.StartGame(gm.difficulty);
        }
    }
    public void LevelSelectAdvanced_Button()
    {
        Debug.Log("Advanced");
        gm.difficulty = GameManager.Difficulty.Advanced;
        PlayerPrefs.SetInt("difficulty", 2);
        levelSelectMenu.alpha = 0f;
        if ((gm.isMainMenuGame && PlayerPrefs.GetInt("tutorialMiniGame2") == 0) || (!gm.isMainMenuGame && PlayerPrefs.GetInt("tutorialMiniGameStory2") == 0))
        {
            tutorialMenu.alpha = 1f;
            SetTutorialText();
        }
        else
        {
            tutorialSet = true;
            turns.alpha = 1f;
            pauseButton.alpha = 1f;
            speedSlider.alpha = 1f;
            gm.StartGame(gm.difficulty);
        }
    }
    public void LevelSelectExpert_Button()
    {
        Debug.Log("Expert");
        gm.difficulty = GameManager.Difficulty.Expert;
        PlayerPrefs.SetInt("difficulty", 3);
        levelSelectMenu.alpha = 0f;
        if ((gm.isMainMenuGame && PlayerPrefs.GetInt("tutorialMiniGame3") == 0) || (!gm.isMainMenuGame && PlayerPrefs.GetInt("tutorialMiniGameStory3") == 0))
        {
            tutorialMenu.alpha = 1f;
            SetTutorialText();
        }
        else
        {
            tutorialSet = true;
            turns.alpha = 1f;
            pauseButton.alpha = 1f;
            speedSlider.alpha = 1f;
            gm.StartGame(gm.difficulty);
        }
    }


    //===Scene Change===//
    IEnumerator GoToScene(string sceneName)
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadSceneAsync(sceneName);

        if (SceneManager.GetActiveScene().name == "LoadingScreen")
            SceneManager.UnloadSceneAsync("LoadingScreen");
    }
}
