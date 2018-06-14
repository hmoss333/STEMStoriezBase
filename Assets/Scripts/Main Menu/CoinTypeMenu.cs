using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTypeMenu : MonoBehaviour {

    public UIToggle pennyCheck;
    public UIToggle nickelCheck;
    public UIToggle dimeCheck;
    public UIToggle quarterCheck;
    public UIToggle halfCheck;

    public UIToggle[] checking;

    public int disabledCoinCount;


    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("pennyOn") == 1)
        {
            pennyCheck.value = true;
        }
        else
        {
            pennyCheck.value = false;
            disabledCoinCount++;
        }

        if (PlayerPrefs.GetInt("nickelOn") == 1)
        {
            nickelCheck.value = true;
        }
        else
        {
            nickelCheck.value = false;
            disabledCoinCount++;
        }

        if (PlayerPrefs.GetInt("dimeOn") == 1)
        {
            dimeCheck.value = true;
        }
        else
        {
            dimeCheck.value = false;
            disabledCoinCount++;
        }

        if (PlayerPrefs.GetInt("quarterOn") == 1)
        {
            quarterCheck.value = true;
        }
        else
        {
            quarterCheck.value = false;
            disabledCoinCount++;
        }

        if (PlayerPrefs.GetInt("halfOn") == 1)
        {
            halfCheck.value = true;
        }
        else
        {
            halfCheck.value = false;
            disabledCoinCount++;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateToggleValues()
    {
        if (PlayerPrefs.GetInt("pennyOn") == 1)
            pennyCheck.value = true;
        else
            pennyCheck.value = false;

        if (PlayerPrefs.GetInt("nickelOn") == 1)
            nickelCheck.value = true;
        else
            nickelCheck.value = false;

        if (PlayerPrefs.GetInt("dimeOn") == 1)
            dimeCheck.value = true;
        else
            dimeCheck.value = false;

        if (PlayerPrefs.GetInt("quarterOn") == 1)
            quarterCheck.value = true;
        else
            quarterCheck.value = false;

        if (PlayerPrefs.GetInt("halfOn") == 1)
            halfCheck.value = true;
        else
            halfCheck.value = false;

        disabledCoinCount = 0;
    }

    public void pennyButton()
    {
        if (disabledCoinCount < 3)
        {
            if (PlayerPrefs.GetInt("pennyOn") == 1)
            {
                PlayerPrefs.SetInt("pennyOn", 0);
                //Debug.Log("Turn off penny");
                disabledCoinCount++;
            }
            else
            {
                PlayerPrefs.SetInt("pennyOn", 1);
                //Debug.Log("Turn on penny");
                disabledCoinCount--;
            }
        }
        else
        {
            if (pennyCheck.value != false)
            {
                pennyCheck.value = true;
                PlayerPrefs.SetInt("pennyOn", 1);
                //Debug.Log("Penny back on");
                disabledCoinCount--;
            }
            else
            {
                pennyCheck.value = true;
                //Debug.Log("Penny still on");
            }
        }
    }

    public void nickelButton()
    {
        if (disabledCoinCount < 3)
        {
            if (PlayerPrefs.GetInt("nickelOn") == 1)
            {
                PlayerPrefs.SetInt("nickelOn", 0);
                //Debug.Log("Turn off nickel");
                disabledCoinCount++;
            }
            else
            {
                PlayerPrefs.SetInt("nickelOn", 1);
                //Debug.Log("Turn on nickel");
                disabledCoinCount--;
            }
        }
        else
        {
            if (nickelCheck.value != false)
            {
                nickelCheck.value = true;
                PlayerPrefs.SetInt("nickelOn", 1);
                //Debug.Log("Nickel back on");
                disabledCoinCount--;
            }
            else
            {
                nickelCheck.value = true;
                //Debug.Log("Nickel still on");
            }
        }
    }

    public void dimeButton()
    {
        if (disabledCoinCount < 3)
        {
            if (PlayerPrefs.GetInt("dimeOn") == 1)
            {
                PlayerPrefs.SetInt("dimeOn", 0);
                //Debug.Log("Turn off dime");
                disabledCoinCount++;
            }
            else
            {
                PlayerPrefs.SetInt("dimeOn", 1);
                //Debug.Log("Turn on dime");
                disabledCoinCount--;
            }
        }
        else
        {
            if (dimeCheck.value != false)
            {
                dimeCheck.value = true;
                PlayerPrefs.SetInt("dimeOn", 1);
                //Debug.Log("Dime back on");
                disabledCoinCount--;
            }
            else
            {
                dimeCheck.value = true;
                //Debug.Log("Dime still on");
            }
        }
    }

    public void quarterButton()
    {
        if (disabledCoinCount < 3)
        {
            if (PlayerPrefs.GetInt("quarterOn") == 1)
            {
                PlayerPrefs.SetInt("quarterOn", 0);
                //Debug.Log("Turn off quarter");
                disabledCoinCount++;
            }
            else
            {
                PlayerPrefs.SetInt("quarterOn", 1);
                //Debug.Log("Turn on quarter");
                disabledCoinCount--;
            }
        }
        else
        {
            if (quarterCheck.value != false)
            {
                quarterCheck.value = true;
                PlayerPrefs.SetInt("quarterOn", 1);
                //Debug.Log("Quarter back on");
                disabledCoinCount--;
            }
            else
            {
                quarterCheck.value = true;
                //Debug.Log("Quarter still on");
            }
        }
    }

    public void halfButton()
    {
        if (disabledCoinCount < 3)
        {
            if (PlayerPrefs.GetInt("halfOn") == 1)
            {
                PlayerPrefs.SetInt("halfOn", 0);
                //Debug.Log("Turn off half");
                disabledCoinCount++;
            }
            else
            {
                PlayerPrefs.SetInt("halfOn", 1);
                //Debug.Log("Turn on half");
                disabledCoinCount--;
            }
        }
        else
        {
            if (halfCheck.value != false)
            {
                halfCheck.value = true;
                PlayerPrefs.SetInt("halfOn", 1);
                //Debug.Log("Half back on");
                disabledCoinCount--;
            }
            else
            {
                halfCheck.value = true;
                //Debug.Log("Half still on");
            }
        }
    }

}
