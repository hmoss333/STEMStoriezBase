using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Coin Lists")]
    [SerializeField] GameObject[] coins;
    [SerializeField] ColumnControler[] columns;
    [SerializeField] List<GameObject> coinList;
    [System.NonSerialized] public GameObject currentCoin;

    public enum Difficulty { Unselected, Beginner, Advanced, Expert };
    [Header("Difficulty")]
    public Difficulty difficulty;

    [Header("Attributes")]
    public bool isMainMenuGame;
    [System.NonSerialized] public int turnCount;
    //[System.NonSerialized] public int totalTurnCount;
    [System.NonSerialized] public int targetAmount = 0;
    //[System.NonSerialized]
    public bool canSpawn = false;
    public static bool gameOver = false;
    [System.NonSerialized] public int activeCoins = 0;
    [System.NonSerialized] public float gameSpeed;
    public int correctNum;
    public int incorrectNum;
    [System.NonSerialized] public float leftMax;
    [System.NonSerialized] public float rightMax;
    Transform leftLimit;
    Transform rightLimit;

    UIManager uim;
    UIPanel parent;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pennyAudio;
    public AudioClip nickelAudio;
    public AudioClip dimeAudio;
    public AudioClip quarterAudio;
    public AudioClip halfAudio;
    public AudioClip correct;
    public AudioClip incorrect;
    public AudioSource backgroundMusic;
    public AudioSource successSource;
    public AudioClip[] successClips;

    private void Awake()
    {
        turnCount = PlayerPrefs.GetInt("miniGameTurns");
        int difficultyCheck = PlayerPrefs.GetInt("difficulty");

        switch (difficultyCheck)
        {
            case 0:
                difficulty = Difficulty.Unselected;
                break;
            case 1:
                difficulty = Difficulty.Beginner;
                break;
            case 2:
                difficulty = Difficulty.Advanced;
                break;
            case 3:
                difficulty = Difficulty.Expert;
                break;
        }

        if (difficulty != Difficulty.Unselected)
            StartGame(difficulty);
    }

    // Use this for initialization
    void Start () {
        uim = GameObject.FindObjectOfType<UIManager>();
        parent = GetComponent<UIPanel>();
        leftLimit = GameObject.Find("LeftLimit").GetComponent<Transform>();
        rightLimit = GameObject.Find("RightLimit").GetComponent<Transform>();

        //turnCount = PlayerPrefs.GetInt("miniGameTurns");
        gameSpeed = PlayerPrefs.GetFloat("miniGameSpeed") + 0.25f;
        gameOver = false;

        leftMax = leftLimit.position.x;
        rightMax = rightLimit.position.x;

        correctNum = 0;
        incorrectNum = 0;


        //Used for columncontroller if playing with fewer than all coins.
        //If the number of activeCoins is less than the number of turns, 
        //then the game will not remove them from the current objectpool
        if (PlayerPrefs.GetInt("pennyOn") == 1)
            activeCoins++;
        if (PlayerPrefs.GetInt("nickelOn") == 1)
            activeCoins++;
        if (PlayerPrefs.GetInt("dimeOn") == 1)
            activeCoins++;
        if (PlayerPrefs.GetInt("quarterOn") == 1)
            activeCoins++;
        if (PlayerPrefs.GetInt("halfOn") == 1)
            activeCoins++;


        if (PlayerPrefs.GetInt("voice") == 1)
        {
            pennyAudio = null;
            nickelAudio = null;
            dimeAudio = null;
            quarterAudio = null;
            halfAudio = null;
            correct = null;
            incorrect = null;
        }
        else
        {
            backgroundMusic.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameOver && canSpawn && difficulty != Difficulty.Unselected)
        {
            if (coinList.Count > 0)
            {
                SpawnCoin(NextCoin());
            }
            else
                gameOver = true;
        }
    }

    public void StartGame (Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Beginner:
                SetSelectableCoins();
                SetTargetAmount();

                if (isMainMenuGame)
                {
                    //This is for a weird, one-off case 
                    //for when the number of turns is greater 
                    //than the number of coins available
                    if (coinList.Count < 3)
                    {
                        if (PlayerPrefs.GetInt("miniGameTurns") > 1)
                            turnCount = 2;
                        else
                            turnCount = 1;
                    }
                }
                else
                {
                    if (turnCount >= 3)
                        turnCount = 3;
                }
                break;
            case Difficulty.Advanced:
                SetTargetAmount();
                SetSelectableCoins();

                if (!isMainMenuGame)
                    turnCount = 3;
                break;
            case Difficulty.Expert:
                SetTargetAmount();
                SetSelectableCoins();

                if (!isMainMenuGame)
                    turnCount = 3;
                break;
            case Difficulty.Unselected:
                break;
            default:
                break;
        }

        //if (!isMainMenuGame)
        //    turnCount = 3;

        canSpawn = true;
    }

    void SpawnCoin (GameObject coinToSpawn)
    {
        currentCoin = Instantiate(coinToSpawn, new Vector2(coinToSpawn.transform.position.x, leftLimit.localPosition.y), Quaternion.identity);
        currentCoin.transform.SetParent(parent.transform, false);
        PlayCoinAudio(currentCoin.GetComponent<CoinControler>());
        canSpawn = false;

        uim.UpdateCoinValue();
    }

    GameObject NextCoin ()
    {
        GameObject nextCoin;

        if (difficulty == Difficulty.Beginner)
        {
            if (!isMainMenuGame)
            {
                if (targetAmount == 50)
                    nextCoin = coins[4];
                else if (targetAmount == 25)
                    nextCoin = coins[3];
                else if (targetAmount == 10)
                    nextCoin = coins[2];
                else if (targetAmount == 5)
                    nextCoin = coins[1];
                else
                    nextCoin = coins[0];
            }
            else
            {
                //Maybe do this as a switch?
                if (targetAmount == 50 && PlayerPrefs.GetInt("halfOn") == 1)
                    nextCoin = coins[4];
                else if (targetAmount == 25 && PlayerPrefs.GetInt("quarterOn") == 1)
                    nextCoin = coins[3];
                else if (targetAmount == 10 && PlayerPrefs.GetInt("dimeOn") == 1)
                    nextCoin = coins[2];
                else if (targetAmount == 5 && PlayerPrefs.GetInt("nickelOn") == 1)
                    nextCoin = coins[1];
                else
                    nextCoin = coins[0];
            }
        }
        else
        {
            int randomNum = Random.Range(0, coinList.Count);

            nextCoin = coinList[randomNum];
        }
        return nextCoin;
    }

    private void SetSelectableCoins()
    {
        coinList.Clear();
        if (difficulty == Difficulty.Beginner)
        {
            foreach (ColumnControler column in columns)
            {
                column.CheckState();

                if (!column.completed)
                {
                    if (column.coinTypes == ColumnControler.CoinTypes.Half)
                        coinList.Add(coins[4]);
                    if (column.coinTypes == ColumnControler.CoinTypes.Quarter)
                        coinList.Add(coins[3]);
                    if (column.coinTypes == ColumnControler.CoinTypes.Dime)
                        coinList.Add(coins[2]);
                    if (column.coinTypes == ColumnControler.CoinTypes.Nickel)
                        coinList.Add(coins[1]);
                    if (column.coinTypes == ColumnControler.CoinTypes.Penny)
                        coinList.Add(coins[0]);
                }

                column.UpdateLabel();
            }
        }
        else if (difficulty == Difficulty.Advanced)
        {
            if (!isMainMenuGame)
            {
                foreach (ColumnControler column in columns)
                {
                    if (column.targetValue + 50 <= 100 && !coinList.Contains(coins[4]))
                        coinList.Add(coins[4]);
                    if (column.targetValue + 25 <= 100 && !coinList.Contains(coins[3]))
                        coinList.Add(coins[3]);
                    if (column.targetValue + 10 <= 100 && !coinList.Contains(coins[2]))
                        coinList.Add(coins[2]);
                    if (column.targetValue + 5 <= 100 && !coinList.Contains(coins[1]))
                        coinList.Add(coins[1]);
                    if (column.targetValue + 1 <= 100 && !coinList.Contains(coins[0]))
                        coinList.Add(coins[0]);

                    column.UpdateLabel();
                }
            }
            else
            {
                foreach (ColumnControler column in columns)
                {
                    if (column.targetValue + 50 <= 100 && !coinList.Contains(coins[4]) && PlayerPrefs.GetInt("halfOn") == 1)
                        coinList.Add(coins[4]);
                    if (column.targetValue + 25 <= 100 && !coinList.Contains(coins[3]) && PlayerPrefs.GetInt("quarterOn") == 1)
                        coinList.Add(coins[3]);
                    if (column.targetValue + 10 <= 100 && !coinList.Contains(coins[2]) && PlayerPrefs.GetInt("dimeOn") == 1)
                        coinList.Add(coins[2]);
                    if (column.targetValue + 5 <= 100 && !coinList.Contains(coins[1]) && PlayerPrefs.GetInt("nickelOn") == 1)
                        coinList.Add(coins[1]);
                    if (column.targetValue + 1 <= 100 && !coinList.Contains(coins[0]) && PlayerPrefs.GetInt("pennyOn") == 1)
                        coinList.Add(coins[0]);

                    column.UpdateLabel();
                }
            }
        }
        else if (difficulty == Difficulty.Expert)
        {
            if (!isMainMenuGame)
            {
                foreach (ColumnControler column in columns)
                {
                    if (column.targetValue - column.currentValue >= 50 && !coinList.Contains(coins[4]))
                        coinList.Add(coins[4]);
                    if (column.targetValue - column.currentValue >= 25 && !coinList.Contains(coins[3]))
                        coinList.Add(coins[3]);
                    if (column.targetValue - column.currentValue >= 10 && !coinList.Contains(coins[2]))
                        coinList.Add(coins[2]);
                    if (column.targetValue - column.currentValue >= 5 && !coinList.Contains(coins[1]))
                        coinList.Add(coins[1]);
                    if (column.targetValue - column.currentValue >= 1 && !coinList.Contains(coins[0]))
                        coinList.Add(coins[0]);

                    column.UpdateLabel();
                }
            }
            else
            {
                foreach (ColumnControler column in columns)
                {
                    if (column.targetValue - column.currentValue >= 50 && !coinList.Contains(coins[4]) && PlayerPrefs.GetInt("halfOn") == 1)
                        coinList.Add(coins[4]);
                    if (column.targetValue - column.currentValue >= 25 && !coinList.Contains(coins[3]) && PlayerPrefs.GetInt("quarterOn") == 1)
                        coinList.Add(coins[3]);
                    if (column.targetValue - column.currentValue >= 10 && !coinList.Contains(coins[2]) && PlayerPrefs.GetInt("dimeOn") == 1)
                        coinList.Add(coins[2]);
                    if (column.targetValue - column.currentValue >= 5 && !coinList.Contains(coins[1]) && PlayerPrefs.GetInt("nickelOn") == 1)
                        coinList.Add(coins[1]);
                    if (column.targetValue - column.currentValue >= 1 && !coinList.Contains(coins[0]) && PlayerPrefs.GetInt("pennyOn") == 1)
                        coinList.Add(coins[0]);

                    column.UpdateLabel();
                }
            }
        }

        //if (!isMainMenuGame && turnCount > 3)
        //    turnCount = 3;
    }

    private void SetTargetAmount()
    {
        switch (difficulty)
        {
            case Difficulty.Unselected:
                Debug.Log("Error: Difficulty undefined");
                break;
            case Difficulty.Beginner:
                int randomInt = Random.Range(0, coinList.Count);
                targetAmount = coinList[randomInt].GetComponent<CoinControler>().value;
                break;
            case Difficulty.Advanced:
                for (int i = 0; i < columns.Length; i++)
                {
                    columns[i].targetValue = 0;
                }
                break;
            case Difficulty.Expert:
                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                        columns[i].targetValue = 10;
                    else if (i == 1)
                        columns[i].targetValue = 25;
                    else if (i == 2)
                        columns[i].targetValue = 50;
                    else if (i == 3)
                        columns[i].targetValue = 100;
                    else if (i == 4)
                        columns[i].targetValue = 200;
                    else
                        columns[i].targetValue = 0;
                }
                break;
            default:
                Debug.Log("Error: Difficulty undefined");
                break;
        }
    }

    IEnumerator UpdateCoins()
    {
        yield return new WaitForSeconds(0.45f);
        if (turnCount > 0)
        {
            SetSelectableCoins();
            if (difficulty == Difficulty.Beginner)
                SetTargetAmount();
            canSpawn = true;
        }
        else
            gameOver = true;
    }

    public void ColumnCheck()
    {
        turnCount--;
    }


    //==Audio==//
    public void PlayCoinAudio (CoinControler coin)
    {
        audioSource.Stop();

        switch (coin.coinTypes)
        {
            case CoinControler.CoinTypes.Penny:
                audioSource.clip = pennyAudio;
                break;
            case CoinControler.CoinTypes.Nickel:
                audioSource.clip = nickelAudio;
                break;
            case CoinControler.CoinTypes.Dime:
                audioSource.clip = dimeAudio;
                break;
            case CoinControler.CoinTypes.Quarter:
                audioSource.clip = quarterAudio;
                break;
            case CoinControler.CoinTypes.Half:
                audioSource.clip = halfAudio;
                break;
            default:
                audioSource.clip = pennyAudio;
                break;
        }

        audioSource.Play();
    }

    public void PlayMatchAudio(bool isCorrect)
    {
        audioSource.Stop();
        audioSource.clip = (isCorrect) ? correct : incorrect;
        audioSource.Play();

        if (isCorrect)
            PlaySuccessAudio();
    }

    void PlaySuccessAudio ()
    {
        int randClip = Random.Range(0, successClips.Length);

        successSource.Stop();
        successSource.clip = successClips[randClip];
        successSource.Play();
    }


    //==Touch Interaction==//
    public void ButtonTest()
    {
        if (currentCoin != null && !currentCoin.GetComponent<CoinControler>().interacting && !UIManager.paused)
            currentCoin.GetComponent<CoinControler>().interacting = true;
    }
}
