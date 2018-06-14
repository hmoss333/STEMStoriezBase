using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnControler : MonoBehaviour {

    public enum CoinTypes { Penny, Nickel, Dime, Quarter, Half };
    public CoinTypes coinTypes;

    //[System.NonSerialized]
    public float targetValue;
    //[System.NonSerialized]
    public float currentValue;
    //[System.NonSerialized]
    public bool completed = false;

    UILabel scoreLabel;
    GameManager gm;
    UISprite sprite;
    public UISprite coinSprite;
    public UISprite correctSprite;


    private void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        sprite = GetComponent<UISprite>();
        //coinSprite = GetComponentInChildren<UISprite>();
        scoreLabel = GetComponentInChildren<UILabel>();
    }

    // Use this for initialization
    void Start () {
        currentValue = 0;
        correctSprite.alpha = 0f;

        if (gm.difficulty == GameManager.Difficulty.Unselected)
            scoreLabel.text = "";
        else if (gm.difficulty != GameManager.Difficulty.Beginner && !completed)
            coinSprite.alpha = 0f;
    }

    public void CheckState()
    {
        if (gm.isMainMenuGame)
        {
            switch (coinTypes)
            {
                case CoinTypes.Penny:
                    if (PlayerPrefs.GetInt("pennyOn") == 0 && !completed)
                        SetComplete();
                    break;
                case CoinTypes.Nickel:
                    if (PlayerPrefs.GetInt("nickelOn") == 0 && !completed)
                        SetComplete();
                    break;
                case CoinTypes.Dime:
                    if (PlayerPrefs.GetInt("dimeOn") == 0 && !completed)
                        SetComplete();
                    break;
                case CoinTypes.Quarter:
                    if (PlayerPrefs.GetInt("quarterOn") == 0 && !completed)
                        SetComplete();
                    break;
                case CoinTypes.Half:
                    if (PlayerPrefs.GetInt("halfOn") == 0 && !completed)
                        SetComplete();
                    break;
                default:
                    break;
            }
        }

        if (!completed)
            coinSprite.alpha = 1f;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        CoinControler coin = col.gameObject.GetComponent<CoinControler>();

        if (col.tag == "Coin" && !completed)
        {
            if (gm.difficulty == GameManager.Difficulty.Beginner)
            {
                if (coin.coinTypes.ToString() == coinTypes.ToString())
                {
                    sprite.color = Color.green;
                    correctSprite.spriteName = "Correct";
                    correctSprite.alpha = 1f;
                    gm.PlayMatchAudio(true);

                    gm.activeCoins--;
                    gm.correctNum++;
                    SetComplete();

                    coin.matched = true;
                }
                else
                {
                    coinSprite.alpha = 0f;
                    sprite.color = Color.red;
                    correctSprite.spriteName = "Incorrect";
                    correctSprite.alpha = 1f;
                    gm.PlayMatchAudio(false);
                    gm.incorrectNum++;
                }
            }
            else if (gm.difficulty == GameManager.Difficulty.Advanced)
            {
                if (targetValue + coin.value <= 100)
                {
                    sprite.color = Color.green;
                    correctSprite.spriteName = "Correct";
                    correctSprite.alpha = 1f;
                    gm.PlayMatchAudio(true);
                    targetValue += coin.value;

                    if (targetValue == 100)
                    {
                        gm.correctNum++;
                        SetComplete();
                    }

                    coin.matched = true;
                }
                else
                {
                    sprite.color = Color.red;
                    correctSprite.spriteName = "Incorrect";
                    correctSprite.alpha = 1f;
                    gm.PlayMatchAudio(false);
                    gm.incorrectNum++;
                }
            }
            else if (gm.difficulty == GameManager.Difficulty.Expert)
            {
                if (currentValue + coin.value <= targetValue)
                {
                    sprite.color = Color.green;
                    correctSprite.spriteName = "Correct";
                    correctSprite.alpha = 1f;
                    gm.PlayMatchAudio(true);
                    currentValue += coin.value;

                    if (currentValue == targetValue)
                    {
                        gm.correctNum++;
                        SetComplete();
                    }

                    coin.matched = true;
                }
                else
                {
                    sprite.color = Color.red;
                    correctSprite.spriteName = "Incorrect";
                    correctSprite.alpha = 1f;
                    gm.PlayMatchAudio(false);
                    gm.incorrectNum++;
                }
            }

            UpdateLabel();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        CoinControler coin = col.gameObject.GetComponent<CoinControler>();

        if (col.tag == "Coin" && !completed)
        {
            sprite.color = Color.white;
            correctSprite.alpha = 0f;

            if (gm.difficulty == GameManager.Difficulty.Beginner)
                coinSprite.alpha = 1f;
        }
    }

    public void UpdateLabel ()
    {
        decimal target = (decimal)targetValue / 100;
        decimal current = (decimal)currentValue / 100;

        if (gm.difficulty == GameManager.Difficulty.Advanced)
        {
            scoreLabel.text = target.ToString("C2");
        }
        else if (gm.difficulty == GameManager.Difficulty.Expert)
        {
            scoreLabel.fontSize = 23;
            scoreLabel.text = "[" + target.ToString("C2") + "]" + 
                "\n" + 
                "\nCurrent: " + current.ToString("C2") + 
                "\nWhat's Left: " + (target - current).ToString("C2");
        }
        else
        {
            scoreLabel.text = coinTypes.ToString();
        }
    }

    void SetComplete()
    {
        completed = true;
        sprite.color = Color.green;
        coinSprite.alpha = 0f;
        gm.ColumnCheck();
    }
}
