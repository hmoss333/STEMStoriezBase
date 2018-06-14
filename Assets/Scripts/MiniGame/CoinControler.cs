using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControler : MonoBehaviour {

    public enum CoinTypes { Penny, Nickel, Dime, Quarter, Half };
    [Header("Coin Types")]
    public CoinTypes coinTypes;
    public int value; //how much this coin is worth

    [Header("Movement Controls")]
    float defaultSpeed = 0.75f;
    float fastSpeed = 1.25f;
    float speed;
    float leftMax;
    float rightMax;
    bool dirRight;
    float dropCheckDist = 0.0f;
    [System.NonSerialized] public bool interacting = false;
    [System.NonSerialized] public bool matched = false;

    Vector2 initPosition;

    GameManager gm;

    private bool key3Press = false;
    private bool key2Press = false;
    private bool key1Press = false;
    private bool keyspacePress = false;
    private bool keyenterPress = false;
    private Event e;
    private bool objectApp = false;

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

    // Use this for initialization
    void Start () {
#if UNITY_IOS
		iCadeInput.Activate(true);
		
		//Register some callbacks
		iCadeInput.AddICadeEventCallback(iCadeStateCallback);
		iCadeInput.AddICadeButtonUpCallback(iCadeButtonUpCallback);
		iCadeInput.AddICadeButtonDownCallback(iCadeButtonDownCallback);
#endif

        gm = GameObject.FindObjectOfType<GameManager>();

        initPosition = transform.position;
        speed = gm.gameSpeed;
        leftMax = gm.leftMax;
        rightMax = gm.rightMax;
	}
	
	// Update is called once per frame
	void Update () {
        if (speed != gm.gameSpeed)
            speed = gm.gameSpeed;

        if (!GameManager.gameOver && !UIManager.paused)
        {
            if (!interacting)
            {
                if (dirRight)
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                else
                    transform.Translate(-Vector2.right * speed * Time.deltaTime);

                if (transform.position.x >= rightMax)
                    dirRight = false;
                if (transform.position.x <= leftMax)
                    dirRight = true;

                //start zumo controls
                if (Input.GetKeyDown("1") == true)
                {
                    key1Press = true;
                    objectApp = true;
                }
                if (Input.GetKeyDown("2") == true)
                {
                    key2Press = true;
                    objectApp = true;
                }
                if (Input.GetKeyDown("3") == true)
                {
                    key3Press = true;
                    objectApp = true;
                }
                if (Input.GetKeyDown("space") == true)
                {
                    keyspacePress = true;
                    objectApp = true;
                }
                if (e != null)
                {
                    if (e.keyCode.ToString() == "10" && e.type == EventType.keyDown)
                    {
                        //Debug.Log("hello");
                        //		if (Input.GetKeyDown(KeyCode.Return)) {
                        keyenterPress = true;
                        objectApp = true;
                    }
                }

                if ((PlayerPrefs.GetInt("educationOn") == 1) || (PlayerPrefs.GetInt("therapyOn") == 1))
                {
                    if ((PlayerPrefs.GetInt("key1toggle") == 0) && key1Press)
                    {
                        interacting = true;
                    }
                    if ((PlayerPrefs.GetInt("key2toggle") == 0) && key2Press)
                    {
                        interacting = true;
                    }
                    if ((PlayerPrefs.GetInt("key3toggle") == 0) && key3Press)
                    {
                        interacting = true;
                    }
                    if ((PlayerPrefs.GetInt("keySpacetoggle") == 0) && keyspacePress)
                    {
                        interacting = true;
                    }
                    if ((PlayerPrefs.GetInt("keyEntertoggle") == 0) && keyenterPress)
                    {
                        interacting = true;
                    }
                    key1Press = false;
                    key2Press = false;
                    key3Press = false;
                    keyspacePress = false;
                    keyenterPress = false;
                }
                else if (objectApp && (((PlayerPrefs.GetInt("educationOn") == 1) && (PlayerPrefs.GetInt("therapyOn") == 1))
                                  || ((PlayerPrefs.GetInt("educationOn") == 0) && (PlayerPrefs.GetInt("therapyOn") == 0))))
                {
                    interacting = true;
                    objectApp = false;
                }
                else
                {
                    objectApp = false;
                }
                //end zumo controls
            }
            else
            {
                transform.Translate(Vector2.up * -speed * Time.deltaTime);

                if (transform.position.y < dropCheckDist)
                    Interact();
            }
        }
    }

    void Interact ()
    {
        interacting = false;

        if (matched)
        {
            gm.StartCoroutine("UpdateCoins");
            Destroy(gameObject);
        }
        else
        {
            transform.position = initPosition;
            dirRight = true;
            gm.PlayCoinAudio(this);
        }
    }
}
