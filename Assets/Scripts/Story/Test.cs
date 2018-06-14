using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Test : MonoBehaviour
{
	[Header("Variables")]
	private bool enabled = true;
	//public bool isOn = false; // Clicking will toggle the button on and off. Starts off
	private int count = -1;
	string message;
	private bool completetext = true;
	//bool animateBook = false;
	//bool animateChest = false;
	//private bool didSeeKeyPress = false;
	private bool objectApp = false;
	private bool key3Press = false;
	private bool key2Press = false;
	private bool key1Press = false;
	private bool keyspacePress = false;
	private bool keyenterPress = false;
	private bool repeat = false;
	private bool back = false;
	private bool backPage = false;
	private bool repeatPage = false;
	private bool nextPage = false;
	private bool load = false;
	float startAlpha;
	int sequence;
	//bool isAnimationEffect = true;
	int randMusic;
	Event e;

	[Header("Menu Objects")]
	public GameObject storyParent;
	public UIPanel storyView;
	public Camera mainCamera;
	public UITexture sprite;
	public UISprite otherBackground;
	public UILabel label;
	public UISprite next;
	public UILabel play;
	public UISprite cover;

	public UISprite exitBack;
	public UISprite playBack2;
	public UILabel exitLabel;
	public UISprite repeatBack;
	public UISprite repeatBack2;
	public UILabel repeatLabel;
	public UISprite backBack;
	public UISprite backBack2;
	public UILabel backLabel;

    [Header("Effects")]
	public UISprite testobj;
	public GameObject specialAnimationEffect1;
	public GameObject specialAnimationEffect2;
	public GameObject specialAnimationEffect3;
	public GameObject specialAnimationEffect4;
	public GameObject specialAnimationEffect5;
	public GameObject specialAnimationEffect6;
	public GameObject specialAnimationEffect7;
	public GameObject specialAnimationEffect8;
	public GameObject specialAnimationEffect9;
	public GameObject specialAnimationEffect10;
	public GameObject specialAnimationEffect11;
	public GameObject specialAnimationEffect12;
	public GameObject specialAnimationEffect13;
	public GameObject specialAnimationEffect14;
	public GameObject specialAnimationEffect15;
	public GameObject specialAnimationEffect16;
	public GameObject specialAnimationEffect17;
	public GameObject specialAnimationEffect18;

    [Header("Story Audio")]
    public AudioClip part1;
    public AudioClip part2;
    public AudioClip part3;
    public AudioClip part4;
    public AudioClip part5;
    public AudioClip part6;
    public AudioClip part7;
    public AudioClip part8;
    public AudioClip part9;
    public AudioClip part10;
    public AudioClip part11;
    public AudioClip part12;
    public AudioClip part13;
    public AudioClip part14;
    public AudioClip part15;
    public AudioClip part16;
    public AudioClip part17;
    public AudioClip part18;
    public AudioClip part19;
    public AudioClip part20;
    public AudioClip part21;
    public AudioClip part22;
    public AudioClip part23;
    public AudioClip part24;
    public AudioClip part25;
    public AudioClip part26;
    public AudioClip part27;
    public AudioClip part28;
    public AudioClip part29;
    public AudioClip part30;
    public AudioClip part31;
    public AudioClip part32;
    public AudioClip part33;
    public AudioClip part34;
    public AudioClip part35;
    public AudioClip part36;

    [Header("Audio")]
	public AudioClip soundSpecial1;
	public AudioClip soundSpecial2;
	public AudioClip soundSpecial3;
	public AudioClip soundSpecial4;
	public AudioClip soundSpecial5;
	public AudioClip soundSpecial6;
	public AudioClip soundSpecial7;
	public AudioClip soundSpecial8;
	public AudioClip soundSpecial9;
	public AudioClip soundSpecial10;
	public AudioClip soundSpecial11;
	public AudioClip soundSpecial12;
	public AudioClip soundSpecial13;
	public AudioClip soundSpecial14;
	public AudioClip soundSpecial15;
	public AudioClip soundSpecial16;
	public AudioClip soundSpecial17;
	public AudioClip soundSpecial18;
	public AudioSource audiosource;
	public AudioSource specialAudiosource;

	[Header("MiniGame Stuff")]
	private bool inMiniGameMode;
	private bool playedGame1;
	private bool playedGame2;
	private bool playedGame3;
	//private bool playedGame4;

	[Header("Tutorial")]
	public GameObject Tutorial_1;
	public GameObject Tutorial_2;
	public GameObject Tutorial_3;
	public bool inTutorialMode;

	[Header("GUI")]
	public UIFont font;
	public UIAtlas atlas;

    //[Header("Loading Screen")]
    //LoadingScreen ls;

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
	//setTap ();
	}

	void iCadeKeyPressedCallback(int i)
	{

	}
#endif

    public void setTap()
	{
		completetext = true;
		audiosource.Stop();
	}

	public void exitClick()
	{
        //SceneManager.LoadSceneAsync("LoadingScreen");
        StartCoroutine(DisplayScene());
	}

	public void repeatClick()
	{
		count--;
		OnClick();
	}

	public void backClick()
	{
		back = true;
		if (count == 0)
		{
			back = false;
		}
		else
		{
			count -= 2;
			OnClick();
		}
	}

	public void tapStory()
	{
		if (!inMiniGameMode)
		{
			if (inTutorialMode)
			{
				if (Tutorial_1.activeInHierarchy)
					Turotiral1OnClick();
				else if (Tutorial_2.activeInHierarchy)
					Turotiral2OnClick();
				else if (Tutorial_3.activeInHierarchy)
					Turotiral3OnClick();
			}

			//if (isAnimationEffect)
			else
			{
				playSpecialAnimationEffect();
				playSpecialAudioEffect();
			}
			//isAnimationEffect = !isAnimationEffect;//Toggle effects;
		}
	}

	void Start()
	{
        //ls = GameObject.Find("LoadingScreen").GetComponent<LoadingScreen>();
        inMiniGameMode = false;
		playedGame1 = false;
		playedGame2 = false;
		playedGame3 = false;

        e = Event.current;

        startAlpha = sprite.alpha;
		sequence = 0;


        //This is needed to activate the iCade plugin. Deactivate it by using iCadeInput.Activate(false)
#if UNITY_IOS
		iCadeInput.Activate(true);

		//Register some callbacks
		iCadeInput.AddICadeEventCallback(iCadeStateCallback);
		iCadeInput.AddICadeButtonUpCallback(iCadeButtonUpCallback);
		iCadeInput.AddICadeButtonDownCallback(iCadeButtonDownCallback);
#endif


        //label.fontSize = (int)(34*PlayerPrefs.GetFloat("printSize")) + 34;
        label.fontSize = (int)(27 * PlayerPrefs.GetFloat("printSize")) + 27;

		//if (repeat == false)
		//{
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect1,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect2,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect3,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect4,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect5,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect6,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect7,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect8,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect9,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect10,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect11,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect12,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect13,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect14,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect15,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect16,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect17,1);
		//	CFX_SpawnSystem.PreloadObject (specialAnimationEffect18,1);

		//	repeat = true;
		//}


		if (PlayerPrefs.GetInt("voice") == 1)
		{
            part1 = null;
            part2 = null;
            part3 = null;
            part4 = null;
            part5 = null;
            part6 = null;
            part7 = null;
            part8 = null;
            part9 = null;
            part10 = null;
            part11 = null;
            part12 = null;
            part13 = null;
            part14 = null;
            part15 = null;
            part16 = null;
            part17 = null;
            part18 = null;
            part19 = null;
            part20 = null;
            part21 = null;
            part22 = null;
            part23 = null;
            part24 = null;
            part25 = null;
            part26 = null;
            part27 = null;
            part28 = null;
            part29 = null;
            part30 = null;
            part31 = null;
            part32 = null;
            part33 = null;
            part34 = null;
            part35 = null;
            part36 = null;
        }

		OnClick();

		//If tutorial is finished, don't start; otherwise start normally
		if (PlayerPrefs.GetInt("tutorial") != 0) { inTutorialMode = false; } else { inTutorialMode = true; }

		if (PlayerPrefs.GetInt("tutorial") == 0)
			Tutorial_1.SetActive(true);
		else
			Tutorial_1.SetActive(false);

		Tutorial_2.SetActive(false);
		Tutorial_3.SetActive(false);

	}

	void Update()
	{
		if (!inMiniGameMode) //prevents any input from being read if in minigame mode
		{
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
					//if (Input.GetKeyDown(KeyCode.Return)) {
					keyenterPress = true;
					objectApp = true;
					//setTap ();
				}
			}

            if ((PlayerPrefs.GetInt("educationOn") == 1) && (PlayerPrefs.GetInt("therapyOn") == 0))
            {
                if ((PlayerPrefs.GetInt("key1newobj") == 1) && key1Press)
                {
                    //AnimateAllActiveObject();
                    if (PlayerPrefs.GetFloat("printSize") < 0.6f)
                    {

                    }
                    else
                    {

                    }
                }
                if ((PlayerPrefs.GetInt("key1anim1") == 1) && key1Press)
                {
                    playSpecialAnimationEffect();
                }
                if ((PlayerPrefs.GetInt("key1anim2") == 1) && key1Press)
                {

                }
                if ((PlayerPrefs.GetInt("key1anim3") == 1) && key1Press)
                {

                }
                if ((PlayerPrefs.GetInt("key1music") == 1) && key1Press)
                {
                    playSpecialAudioEffect();
                }
                if ((PlayerPrefs.GetInt("key1back") == 1) && key1Press)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            backPage = true;
                            setTap();
                        }
                        else
                        {
                            backClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("key1next") == 1) && key1Press)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            nextPage = true;
                            setTap();
                        }
                        else
                        {
                            OnClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("key1repeat") == 1) && key1Press)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            repeatPage = true;
                            setTap();
                        }
                        else
                        {
                            repeatClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("key2newobj") == 1) && key2Press)
                {
                    //AnimateAllActiveObject();
                    if (PlayerPrefs.GetFloat("printSize") < 0.6f)
                    {

                    }
                    else
                    {

                    }
                }
                if ((PlayerPrefs.GetInt("key2anim1") == 1) && key2Press)
                {
                    playSpecialAnimationEffect();
                }
                if ((PlayerPrefs.GetInt("key2anim2") == 1) && key2Press)
                {

                }
                if ((PlayerPrefs.GetInt("key2anim3") == 1) && key2Press)
                {

                }
                if ((PlayerPrefs.GetInt("key2music") == 1) && key2Press)
                {
                    playSpecialAudioEffect();
                }
                if ((PlayerPrefs.GetInt("key2back") == 1) && key2Press)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            backPage = true;
                            setTap();
                        }
                        else
                        {
                            backClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("key2next") == 1) && key2Press)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            nextPage = true;
                            setTap();
                        }
                        else
                        {
                            OnClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("key2repeat") == 1) && key2Press)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            repeatPage = true;
                            setTap();
                        }
                        else
                        {
                            repeatClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("key3newobj") == 1) && key3Press)
                {
                    //AnimateAllActiveObject();
                    if (PlayerPrefs.GetFloat("printSize") < 0.6f)
                    {

                    }
                    else
                    {

                    }
                }
                if ((PlayerPrefs.GetInt("key3anim1") == 1) && key3Press)
                {
                    playSpecialAnimationEffect();
                }
                if ((PlayerPrefs.GetInt("key3anim2") == 1) && key3Press)
                {
                }
                if ((PlayerPrefs.GetInt("key3anim3") == 1) && key3Press)
                {
                }
                if ((PlayerPrefs.GetInt("key3music") == 1) && key3Press)
                {
                    playSpecialAudioEffect();
                }
                if ((PlayerPrefs.GetInt("key3back") == 1) && key3Press)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            backPage = true;
                            setTap();
                        }
                        else
                        {
                            backClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("key3next") == 1) && key3Press)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            nextPage = true;
                            setTap();
                        }
                        else
                        {
                            OnClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("key3repeat") == 1) && key3Press)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            repeatPage = true;
                            setTap();
                        }
                        else
                        {
                            repeatClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("keySpacenewobj") == 1) && keyspacePress)
                {
                    //AnimateAllActiveObject();
                    if (PlayerPrefs.GetFloat("printSize") < 0.6f)
                    {

                    }
                    else
                    {

                    }
                }
                if ((PlayerPrefs.GetInt("keySpaceanim1") == 1) && keyspacePress)
                {
                    playSpecialAnimationEffect();
                }
                if ((PlayerPrefs.GetInt("keySpaceanim2") == 1) && keyspacePress)
                {
                }
                if ((PlayerPrefs.GetInt("keySpaceanim3") == 1) && keyspacePress)
                {
                }
                if ((PlayerPrefs.GetInt("keySpacemusic") == 1) && keyspacePress)
                {
                    playSpecialAudioEffect();
                }
                if ((PlayerPrefs.GetInt("keySpaceback") == 1) && keyspacePress)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            backPage = true;
                            setTap();
                        }
                        else
                        {
                            backClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("keySpacenext") == 1) && keyspacePress)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            nextPage = true;
                            setTap();
                        }
                        else
                        {
                            OnClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("keySpacerepeat") == 1) && keyspacePress)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            repeatPage = true;
                            setTap();
                        }
                        else
                        {
                            repeatClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("keyEnternewobj") == 1) && keyenterPress)
                {
                    //AnimateAllActiveObject();
                    if (PlayerPrefs.GetFloat("printSize") < 0.6f)
                    {

                    }
                    else
                    {

                    }
                }
                if ((PlayerPrefs.GetInt("keyEnteranim1") == 1) && keyenterPress)
                {
                    playSpecialAnimationEffect();
                }
                if ((PlayerPrefs.GetInt("keyEnteranim2") == 1) && keyenterPress)
                {
                }
                if ((PlayerPrefs.GetInt("keyEnteranim3") == 1) && keyenterPress)
                {
                }
                if ((PlayerPrefs.GetInt("keyEntermusic") == 1) && keyenterPress)
                {
                    playSpecialAudioEffect();
                }
                if ((PlayerPrefs.GetInt("keyEnterback") == 1) && keyenterPress)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            backPage = true;
                            setTap();
                        }
                        else
                        {
                            backClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("keyEnternext") == 1) && keyenterPress)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            nextPage = true;
                            setTap();
                        }
                        else
                        {
                            OnClick();
                        }
                    }
                }
                if ((PlayerPrefs.GetInt("keyEnterrepeat") == 1) && keyenterPress)
                {
                    if (load)
                    {
                        if (completetext == false)
                        {
                            repeatPage = true;
                            setTap();
                        }
                        else
                        {
                            repeatClick();
                        }
                    }
                }
                key1Press = false;
                key2Press = false;
                key3Press = false;
                keyspacePress = false;
                keyenterPress = false;
            }

            if ((PlayerPrefs.GetInt("educationOn") == 0) && (PlayerPrefs.GetInt("therapyOn") == 1))
            {
                if ((PlayerPrefs.GetInt("key1toggle") == 0) && key1Press)
                {
                    setTap();
                    OnClick();
                }
                if ((PlayerPrefs.GetInt("key2toggle") == 0) && key2Press)
                {
                    setTap();
                    OnClick();
                }
                if ((PlayerPrefs.GetInt("key3toggle") == 0) && key3Press)
                {
                    setTap();
                    OnClick();
                }
                if ((PlayerPrefs.GetInt("keySpacetoggle") == 0) && keyspacePress)
                {
                    setTap();
                    OnClick();
                }
                if ((PlayerPrefs.GetInt("keyEntertoggle") == 0) && keyenterPress)
                {
                    setTap();
                    OnClick();
                }
                key1Press = false;
                key2Press = false;
                key3Press = false;
                keyspacePress = false;
                keyenterPress = false;
            }

            if (objectApp && (((PlayerPrefs.GetInt("educationOn") == 1) && (PlayerPrefs.GetInt("therapyOn") == 1)) 
                || ((PlayerPrefs.GetInt("educationOn") == 0) && (PlayerPrefs.GetInt("therapyOn") == 0))))
            {
                setTap();
                OnClick();
                objectApp = false;
            }
            else
            {
                objectApp = false;
            }
        }
	}

    void playSpecialAudioEffect()
	{
		specialAudiosource.Stop();
		randMusic = Random.Range(1, 25);
		if (randMusic == 1)
		{
			specialAudiosource.clip = soundSpecial1;
		}
		else if (randMusic == 2)
		{
			specialAudiosource.clip = soundSpecial2;
		}
		else if (randMusic == 3)
		{
			specialAudiosource.clip = soundSpecial3;
		}
		else if (randMusic == 4)
		{
			specialAudiosource.clip = soundSpecial4;
		}
		else if (randMusic == 5)
		{
			specialAudiosource.clip = soundSpecial5;
		}
		else if (randMusic == 6)
		{
			specialAudiosource.clip = soundSpecial6;
		}
		else if (randMusic == 7)
		{
			specialAudiosource.clip = soundSpecial7;
		}
		else if (randMusic == 8)
		{
			specialAudiosource.clip = soundSpecial8;
		}
		else if (randMusic == 9)
		{
			specialAudiosource.clip = soundSpecial9;
		}
		else if (randMusic == 10)
		{
			specialAudiosource.clip = soundSpecial10;
		}
		else if (randMusic == 11)
		{
			specialAudiosource.clip = soundSpecial11;
		}
		else if (randMusic == 12)
		{
			specialAudiosource.clip = soundSpecial12;
		}
		else if (randMusic == 13)
		{
			specialAudiosource.clip = soundSpecial13;
		}
		else if (randMusic == 14)
		{
			specialAudiosource.clip = soundSpecial14;
		}
		else if (randMusic == 15)
		{
			specialAudiosource.clip = soundSpecial15;
		}
		else if (randMusic == 16)
		{
			specialAudiosource.clip = soundSpecial16;
		}
		else if (randMusic == 17)
		{
			specialAudiosource.clip = soundSpecial17;
		}
		else if (randMusic == 18)
		{
			specialAudiosource.clip = soundSpecial18;
		}
		if (PlayerPrefs.GetFloat("printSize") < 0.6f)
		{
			switch (count - 1)
			{
			case -1:
				specialAudiosource.clip = soundSpecial1;
				break;
			case 0:
				specialAudiosource.clip = soundSpecial2;
				break;
			case 1:
				specialAudiosource.clip = soundSpecial3;
				break;
			case 2:
				specialAudiosource.clip = soundSpecial4;
				break;
			case 3:
				specialAudiosource.clip = soundSpecial5;
				break;
			case 4:
				specialAudiosource.clip = soundSpecial6;
				break;
			case 5:
				specialAudiosource.clip = soundSpecial7;
				break;
			case 6:
				specialAudiosource.clip = soundSpecial8;
				break;
			case 7:
				specialAudiosource.clip = soundSpecial9;
				break;
			case 8:
				specialAudiosource.clip = soundSpecial10;
				break;
			case 9:
				specialAudiosource.clip = soundSpecial11;
				break;
			case 10:
				specialAudiosource.clip = soundSpecial12;
				break;
			case 11:
				specialAudiosource.clip = soundSpecial13;
				break;
			case 12:
				specialAudiosource.clip = soundSpecial14;
				break;
			case 13:
				specialAudiosource.clip = soundSpecial15;
				break;
			case 14:
				specialAudiosource.clip = soundSpecial16;
				break;
			case 15:
				specialAudiosource.clip = soundSpecial17;
				break;
			case 16:
				specialAudiosource.clip = soundSpecial18;
				break;
			default:
				Debug.Log("This sound is not implemented");
				break;
			}
		}
		else
		{
			switch (count - 1)
			{
			case -1:
			case 0:
				specialAudiosource.clip = soundSpecial1;
				break;
			case 1:
			case 2:
				specialAudiosource.clip = soundSpecial2;
				break;
			case 3:
			case 4:
				specialAudiosource.clip = soundSpecial3;
				break;
			case 5:
			case 6:
				specialAudiosource.clip = soundSpecial4;
				break;
			case 7:
			case 8:
				specialAudiosource.clip = soundSpecial5;
				break;
			case 9:
			case 10:
				specialAudiosource.clip = soundSpecial6;
				break;
			case 11:
			case 12:
				specialAudiosource.clip = soundSpecial7;
				break;
			case 13:
			case 14:
				specialAudiosource.clip = soundSpecial8;
				break;
			case 15:
			case 16:
				specialAudiosource.clip = soundSpecial9;
				break;
			case 17:
			case 18:
				specialAudiosource.clip = soundSpecial10;
				break;
			case 19:
			case 20:
				specialAudiosource.clip = soundSpecial11;
				break;
			case 21:
			case 22:
				specialAudiosource.clip = soundSpecial12;
				break;
			case 23:
			case 24:
				specialAudiosource.clip = soundSpecial13;
				break;
			case 25:
			case 26:
				specialAudiosource.clip = soundSpecial14;
				break;
			case 27:
			case 28:
				specialAudiosource.clip = soundSpecial15;
				break;
			case 29:
			case 30:
				specialAudiosource.clip = soundSpecial16;
				break;
			case 31:
			case 32:
				specialAudiosource.clip = soundSpecial17;
				break;
			case 33:
			case 34:
				specialAudiosource.clip = soundSpecial18;
				break;
			default:
				Debug.Log("This sound is not implemented");
				break;
			}
		}
		specialAudiosource.Play();
	}

	void playSpecialAnimationEffect()
	{
		switch (count - 1)
		{
		case -1:
		case 0:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect1, true);
			specialAnimationEffect1.GetComponent<ParticleSystem>().Play();
			break;
		case 1:
		case 2:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect2, true);
			specialAnimationEffect2.GetComponent<ParticleSystem>().Play();
			break;
		case 3:
		case 4:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect3, true);
			specialAnimationEffect3.GetComponent<ParticleSystem>().Play();
			break;
		case 5:
		case 6:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect4, true);
			specialAnimationEffect4.GetComponent<ParticleSystem>().Play();
			break;
		case 7:
		case 8:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect5, true);
			specialAnimationEffect5.GetComponent<ParticleSystem>().Play();
			break;
		case 9:
		case 10:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect6, true);
			specialAnimationEffect6.GetComponent<ParticleSystem>().Play();
			break;
		case 11:
		case 12:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect7, true);
			specialAnimationEffect7.GetComponent<ParticleSystem>().Play();
			break;
		case 13:
		case 14:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect8, true);
			specialAnimationEffect8.GetComponent<ParticleSystem>().Play();
			break;
		case 15:
		case 16:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect9, true);
			specialAnimationEffect9.GetComponent<ParticleSystem>().Play();
			break;
		case 17:
		case 18:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect10, true);
			specialAnimationEffect10.GetComponent<ParticleSystem>().Play();
			break;
		case 19:
		case 20:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect11, true);
			specialAnimationEffect11.GetComponent<ParticleSystem>().Play();
			break;
		case 21:
		case 22:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect12, true);
			specialAnimationEffect12.GetComponent<ParticleSystem>().Play();
			break;
		case 23:
		case 24:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect13, true);
			specialAnimationEffect13.GetComponent<ParticleSystem>().Play();
			break;
		case 25:
		case 26:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect14, true);
			specialAnimationEffect14.GetComponent<ParticleSystem>().Play();
			break;
		case 27:
		case 28:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect15, true);
			specialAnimationEffect15.GetComponent<ParticleSystem>().Play();
			break;
		case 29:
		case 30:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect16, true);
			specialAnimationEffect16.GetComponent<ParticleSystem>().Play();
			break;
		case 31:
		case 32:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect17, true);
			specialAnimationEffect17.GetComponent<ParticleSystem>().Play();
			break;
		case 33:
		case 34:
			CFX_SpawnSystem.GetNextObject(specialAnimationEffect18, true);
			specialAnimationEffect18.GetComponent<ParticleSystem>().Play();
			break;
		default:
			Debug.Log("This animation effect is not implemented");
			break;
		}
	}

	public void OnClick()
	{
		if (enabled == true && !inTutorialMode)
		{
			load = false;
			enabled = false;
			float nextAlpha = next.alpha;
			float coverAlpha = cover.alpha;
			play.text = "";
			//cover.alpha = 0;
			next.alpha = 0;
			exitBack.GetComponent<Collider>().enabled = false;
			exitBack.alpha = 0;
			playBack2.alpha = 0;
			exitLabel.alpha = 0;
			repeatBack.GetComponent<Collider>().enabled = false;
			repeatBack.alpha = 0;
			repeatBack2.alpha = 0;
			repeatLabel.alpha = 0;
			backBack.GetComponent<Collider>().enabled = false;
			backBack.alpha = 0;
			backBack2.alpha = 0;
			backLabel.alpha = 0;

			specialAnimationEffect1.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect2.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect3.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect4.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect5.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect6.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect7.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect8.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect9.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect10.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect11.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect12.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect13.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect14.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect15.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect16.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect17.GetComponent<ParticleSystem>().Stop();
			specialAnimationEffect18.GetComponent<ParticleSystem>().Stop();


            ////every 5 levels, if minigame option is active, display minigame instead of going to next page
            if (count == 7 || count == 17 || count == 27 /*|| count == 18*/)
            {
                if (PlayerPrefs.GetInt("minigames") == 1)
                {
                    if (!inMiniGameMode && CanPlay())
                    {
                        label.text = "";
                        SetMiniGame();
                    }
                    else
                    {
                        //inMiniGameMode = false;
                        StartCoroutine(FadeOuter(sprite, 0.5f, nextAlpha, coverAlpha));
                    }
                }
                else
                    StartCoroutine(FadeOuter(sprite, 0.5f, nextAlpha, coverAlpha));
            }
            else
            {
                StartCoroutine(FadeOuter(sprite, 0.5f, nextAlpha, coverAlpha));
            }
        }

        else
        {
            if (Tutorial_1.activeInHierarchy)
                Turotiral1OnClick();
            else if (Tutorial_2.activeInHierarchy)
                Turotiral2OnClick();
            else if (Tutorial_3.activeInHierarchy)
                Turotiral3OnClick();
        }
    }

	//check which minigames have already been played
	bool CanPlay ()
	{
		if (count >= 7 && !playedGame1)
			return true;
		else if (count >= 17 && !playedGame2)
			return true;
		else if (count >= 27 && !playedGame3)
			return true;
		//else if (count >= 18 && !playedGame4)
		//    return true;
		else
			return false;
	}

    //minigame logic here
    public void SetMiniGame()
	{
		inMiniGameMode = true;
		if (count >= 7)
			playedGame1 = true;
		if (count >= 17)
			playedGame2 = true;
		if (count >= 27)
			playedGame3 = true;
        //if (count >= 18)
        //    playedGame4 = true;

        audiosource.Stop();
		mainCamera.gameObject.SetActive(false);
		storyView.alpha = 0f;
        //ls.LoadScene("MiniGameScreen_Story");
        //ls.ChangeText("Let's play...");
        SceneManager.LoadSceneAsync("LoadingScreen");
		StartCoroutine(GoToScene("MiniGameScreen_Story"));
		//enabled = false;
	}

	IEnumerator GoToScene(string sceneName)
	{
        yield return new WaitForSeconds(0.25f);
		SceneManager.LoadSceneAsync(sceneName);

        if (SceneManager.GetActiveScene().name == "LoadingScreen")
            SceneManager.UnloadSceneAsync("LoadingScreen");
    }

	public void EndMiniGame()
	{
  //      Scene scene = SceneManager.GetSceneByName("1stScene");
		//if ((scene != null) && (!scene.isLoaded))
		//{
		//	StartCoroutine(GoToScene("Empty"));
		//}
		//SceneManager.UnloadSceneAsync("MiniGameScreen_Story");
		//Resources.UnloadUnusedAssets();

        StartCoroutine(TurnOnCamera());
	}
	IEnumerator TurnOnCamera()
	{
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("Empty");

        inMiniGameMode = false;
        storyView.alpha = 1f;
        mainCamera.gameObject.SetActive(true);
        enabled = true;
        next.alpha = 1f;
        cover.alpha = 1f;
        OnClick();
	}

	//void PlayCharacterAnim (GameObject character)
	//{
	//	character.GetComponentInChildren<ButtonState>().OnButtonPressed();
	//	//character.GetComponentInChildren<ButtonState>().OnButtonRelease();
	//}

	IEnumerator FadeOther(UIWidget w, float durationInSeconds)
	{
		float startA = w.alpha;
		float currentTime = 0f;
		while (currentTime < durationInSeconds)
		{
			w.alpha = Mathf.Lerp(startA, 0f, currentTime / durationInSeconds);
			currentTime += Time.deltaTime;
			yield return null;
		}
		sprite.mainTexture = Resources.Load("image1") as Texture;
		StartCoroutine(FadeIn(sprite, 0.5f, startA));
	}

	IEnumerator FadeOuter(UIWidget w, float durationInSeconds, float nextAlpha, float coverAlpha)
	{
		inMiniGameMode = false; //ensures the scene has changed before OnClick checks if minigame is active; update OnClick so this isn't necissary

        float startA = w.alpha;
        //label.text = "";

        GameObject pagePrefab = null;

        if (count == -1)
		{
            //EnableCharacters(page1);
            if (!back)
            {
                Destroy(GameObject.Find("Page (1)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (1)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part1;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image1") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "There is money, \n money everywhere. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 0)
		{
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (1)(Clone)"));
                Destroy(GameObject.Find("Page (2)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (1)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part2;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image1") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Money for counting. \n Money to share. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 1)
		{
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (1)(Clone)"));
                Destroy(GameObject.Find("Page (2)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (2)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part3;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image2") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "There are lots of \n different coins to count. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 2)
		{
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (2)(Clone)"));
                Destroy(GameObject.Find("Page (3)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (2)")) as GameObject;
                pagePrefab.transform.SetParent(storyView.transform);
                pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.SetAnchor(storyView.transform);
                }
            }

            audiosource.Stop();
			audiosource.clip = part4;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image2") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "And each coin is worth \n a different amount. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
        else if (count == 3)
		{
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (2)(Clone)"));
                Destroy(GameObject.Find("Page (3)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (3)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part5;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image3") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Count the pennies first \n because they are so much fun. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 4)
		{
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (3)(Clone)"));
                Destroy(GameObject.Find("Page (4)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (3)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part6;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image3") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Each penny's worth 1 cent. \n Let's count them one by one. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 5)
		{
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (3)(Clone)"));
                Destroy(GameObject.Find("Page (4)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (4)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part7;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image4") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "That might not be much, \n but it all adds up. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 6)
		{
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (4)(Clone)"));
                Destroy(GameObject.Find("Page (5)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (4)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part8;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image4") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Especially when a hundred pennies \n fill a dollar's giant cup! ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
    /**/else if (count == 7)
		{
            if (PlayerPrefs.GetInt("minigames") != 1)
                yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (4)(Clone)"));
                Destroy(GameObject.Find("Page (5)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (5)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    //chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    //chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part9;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image5") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Let's count nickels next \n because they are worth five. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 8)
		{
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (5)(Clone)"));
                Destroy(GameObject.Find("Page (6)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (5)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    //chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    //chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part10;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image5") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "So five pennies, \n that's worth one nickel. That's jive. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 9)
		{
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (5)(Clone)"));
                Destroy(GameObject.Find("Page (6)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (6)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part11;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image6") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Keep counting by fives. \n Five... ten... fifteen... twenty. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 10)
		{
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (6)(Clone)"));
                Destroy(GameObject.Find("Page (7)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (6)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part12;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image6") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "A pile of twenty nickels equals a dollar. \n Now that's plenty! ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 11)
		{
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (6)(Clone)"));
                Destroy(GameObject.Find("Page (7)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (7)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part13;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image7") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Next we will count \n these silver coins that are so tiny. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 12)
		{
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (7)(Clone)"));
                Destroy(GameObject.Find("Page (8)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (7)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part14;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image7") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "They are dimes \n and they are extra super shiny! ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 13)
		{
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (7)(Clone)"));
                Destroy(GameObject.Find("Page (8)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (8)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part15;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image8") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Two nickels equals one dime \n or ten pennies if you bother. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
        else if (count == 14)
        {
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (8)(Clone)"));
                Destroy(GameObject.Find("Page (9)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (8)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part16;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image8") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "And if you count ten dimes \n you will have one dollar! ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 15)
        {
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (8)(Clone)"));
                Destroy(GameObject.Find("Page (9)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (9)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part17;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image9") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "The next coin is slightly bigger \n and it is called a quarter. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 16)
        {
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (9)(Clone)"));
                Destroy(GameObject.Find("Page (10)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (9)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part18;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image9") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "Now a quarter is worth five nickels \n if you needed to barter. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
    /**/else if (count == 17)
		{
            if (PlayerPrefs.GetInt("minigames") != 1)
                yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (9)(Clone)"));
                Destroy(GameObject.Find("Page (10)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (10)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part19;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image10") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Or you could count twenty-five \n pennies if you can endure. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 18)
		{
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (10)(Clone)"));
                Destroy(GameObject.Find("Page (11)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (10)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part20;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image10") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Because four quarters equals a dollar \n that's for sure! ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
		else if (count == 19)
		{
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (10)(Clone)"));
                Destroy(GameObject.Find("Page (11)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (11)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
			audiosource.clip = part21;
			audiosource.Play();
			count++;
			sprite.mainTexture = Resources.Load("image11") as Texture;
			StartCoroutine(FadeIn(sprite, 0.5f, startA));
			message = "Next is the fifty-cent piece, \n which we call a half-dollar. ";
			StartCoroutine(TypeText(nextAlpha, coverAlpha));
		}
        else if (count == 20)
        {
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (11)(Clone)"));
                Destroy(GameObject.Find("Page (12)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (11)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part22;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image11") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "And you can figure it out because \n all other coins are smaller. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 21)
        {
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (11)(Clone)"));
                Destroy(GameObject.Find("Page (12)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (12)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part23;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image12") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "Fifty pennies, two quarters, ten nickels \n or five dimes makes the same amount  ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 22)
        {
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (12)(Clone)"));
                Destroy(GameObject.Find("Page (13)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (12)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part24;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image12") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "Two half-dollars together gives you a dollar, \n which makes it easy to count. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 23)
        {
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (12)(Clone)"));
                Destroy(GameObject.Find("Page (13)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (13)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part25;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image13") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "Five nickels, a quarter and \n one half-dollar does the same. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 24)
        {
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (13)(Clone)"));
                Destroy(GameObject.Find("Page (14)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (13)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part26;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image13") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "Or five dimes and two quarters \n can make the same claim. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 25)
        {
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (13)(Clone)"));
                Destroy(GameObject.Find("Page (14)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (14)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part27;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image14") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "Let's try ten pennies, four dimes \n with ten nickels to round out.  ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 26)
        {
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (14)(Clone)"));
                Destroy(GameObject.Find("Page (15)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (14)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part28;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image14") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "We still make a dollar \n for the final amount. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
    /**/else if (count == 27)
        {
            if (PlayerPrefs.GetInt("minigames") != 1)
                yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (14)(Clone)"));
                Destroy(GameObject.Find("Page (15)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (15)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part29;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image15") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "Counting dollars in many different ways \n may sometimes seem funny. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 28)
        {
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (15)(Clone)"));
                Destroy(GameObject.Find("Page (16)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (15)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part30;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image15") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "Ten dimes or one-hundred cents \n will always equal a dollar in money. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 29)
        {
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (15)(Clone)"));
                Destroy(GameObject.Find("Page (16)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (16)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part31;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image16") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "That's why when you have money, \n it's so important to count! ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 30)
        {
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (16)(Clone)"));
                Destroy(GameObject.Find("Page (17)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (16)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part32;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image16") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "So we can count all of our coins \n and know the exact amount. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 31)
        {
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (16)(Clone)"));
                Destroy(GameObject.Find("Page (17)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (17)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part33;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image17") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "And it would be a good time \n to save some of it today. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 32)
        {
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (17)(Clone)"));
                Destroy(GameObject.Find("Page (18)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (17)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part34;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image17") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "In a bank account so our coins \n don't somehow fly away. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 33)
        {
            yield return null;

            if (!back)
            {
                Destroy(GameObject.Find("Page (17)(Clone)"));
                Destroy(GameObject.Find("Page (18)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (18)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part35;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image18") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "There is money, \n money piled everywhere. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 34)
        {
            yield return null;

            if (back)
            {
                Destroy(GameObject.Find("Page (17)(Clone)"));
                Destroy(GameObject.Find("Page (18)(Clone)"));

                pagePrefab = Instantiate(Resources.Load("Page (18)")) as GameObject;
                //pagePrefab.transform.SetParent(storyView.transform);
                //pagePrefab.transform.localScale = new Vector3(1, 1, 1);

                UI2DSprite[] characters = pagePrefab.GetComponentsInChildren<UI2DSprite>();
                foreach (UI2DSprite chars in characters)
                {
                    chars.ResetAnchors();
                    chars.SetAnchor(UICamera.list[0].transform);
                    chars.ResetAnchors();
                }
            }

            audiosource.Stop();
            audiosource.clip = part36;
            audiosource.Play();
            count++;
            sprite.mainTexture = Resources.Load("image18") as Texture;
            StartCoroutine(FadeIn(sprite, 0.5f, startA));
            message = "Money for counting. \n Money to share. ";
            StartCoroutine(TypeText(nextAlpha, coverAlpha));
        }
        else if (count == 35)
		{
            //DisableCharacters(page18);
			//ls.GetComponent<UIPanel>().alpha = 1f;
			storyView.alpha = 0f;

            //SceneManager.LoadSceneAsync("LoadingScreen");
			StartCoroutine(DisplayScene());
		}
        //}

        back = false;
        Resources.UnloadUnusedAssets();
    }


	IEnumerator FadeIn(UIWidget w, float durationInSeconds, float a)
	{
		float startA = w.alpha;
		float currentTime = 0f;
		while (currentTime < durationInSeconds)
		{
			w.alpha = Mathf.Lerp(startA, a, currentTime / durationInSeconds);
			currentTime += Time.deltaTime;
			yield return null;
		}
		w.alpha = startAlpha;
	}

	IEnumerator TypeText(float nextAlpha, float coverAlpha)
	{
		load = true;
		completetext = false;
        label.text = "";

        int spaces = 0;
		if ((PlayerPrefs.GetInt("typing") == 1) && (PlayerPrefs.GetInt("highlight") == 0))
		{
			label.text += "[FFFF00]";
			foreach (char letter in message.ToCharArray())
			{
				if (completetext == true)
				{
					label.text = message;
					break;
				}
				label.text += letter;
                if (letter == ' ')
				{
					spaces++;
				}
				yield return 0;
				yield return new WaitForSeconds(0.038f);
			}
		}
		else
		{
			if (PlayerPrefs.GetInt("typing") == 1)
			{
				foreach (char letter in message.ToCharArray())
				{
					if (completetext == true)
					{
						label.text = message;
						break;
					}
					label.text += letter;
                    yield return 0;
					yield return new WaitForSeconds(0.038f);
				}
			}
			else if (PlayerPrefs.GetInt("highlight") == 0)
			{
				label.text = "[FFFF00]" + message;
				string[] parts = new string[2];
				parts[0] = "[FFFF00]";
				parts[1] = "";
				bool twosentence = false;
				foreach (char letter in message.ToCharArray())
				{
					parts[0] += letter;
					parts[1] += letter;
					if (letter == '\n')
					{
						parts[0] += "[-]";
						parts[1] += "[FFFF00]";
						twosentence = true;
					}
				}
				label.text = parts[0];
				if (twosentence)
				{
					yield return 0;
					for (int i = 0; i < 22; i++)
					{
                        yield return new WaitForSeconds((audiosource.clip.length / 2) / 20);

                        if (completetext == true)
                        {
                            break;
                        }
					}
					label.text = parts[1];
				}
				yield return 0;
				if (completetext == false)
				{
					for (int i = 0; i < 22; i++)
					{
                        yield return new WaitForSeconds((audiosource.clip.length / 2) / 20);

                        if (completetext == true)
                        {
                            break;
                        }
                    }
				}
				label.text = message;
			}
			else
			{
				label.text = message;
			}
		}
		
		completetext = true;
		next.alpha = 1f;// nextAlpha;
		//cover.alpha = coverAlpha;
		//play.text = "Next";

		exitBack.GetComponent<Collider>().enabled = true;
		exitBack.alpha = 255;
		playBack2.alpha = 255;
		exitLabel.alpha = 255;
		repeatBack.GetComponent<Collider>().enabled = true;
		repeatBack.alpha = 255;
		repeatBack2.alpha = 255;
		repeatLabel.alpha = 255;
		backBack.GetComponent<Collider>().enabled = true;
		backBack.alpha = 255;
		backBack2.alpha = 255;
		backLabel.alpha = 255;

		if (count == 0)
		{
			//play.text = "Play";
		}
		enabled = true;

		if (backPage)
		{
			backPage = false;
			backClick();
		}
		if (nextPage)
		{
			nextPage = false;
			OnClick();
		}
		if (repeatPage)
		{
			repeatPage = false;
			repeatClick();
		}
	}

	IEnumerator DisplayScene()
	{
        SceneManager.LoadSceneAsync("LoadingScreen");

        yield return new WaitForSeconds(1);
		SceneManager.LoadSceneAsync(1);
		Destroy(storyParent);

        if (SceneManager.GetActiveScene().name == "LoadingScreen")
            SceneManager.UnloadSceneAsync("LoadingScreen");
    }

    IEnumerable RepeatScene()
	{
		yield return new WaitForSeconds(0);
		SceneManager.LoadScene("1stScene");
		Destroy(storyParent);

        if (SceneManager.GetActiveScene().name == "LoadingScreen")
            SceneManager.UnloadSceneAsync("LoadingScreen");
    }

	public void Turotiral1OnClick()
	{
		Tutorial_1.SetActive(false);
		Tutorial_2.SetActive(true);
		PlayerPrefs.SetInt("tutorial", 1);
	}
	public void Turotiral2OnClick()
	{
		Tutorial_2.SetActive(false);
		Tutorial_3.SetActive(true);
		PlayerPrefs.SetInt("tutorial", 2);
    }
	public void Turotiral3OnClick()
	{
		Tutorial_3.SetActive(false);
		inTutorialMode = false;
		//Set finish tutorial
		PlayerPrefs.SetInt("tutorial", 3);
    }
}

