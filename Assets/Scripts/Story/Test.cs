using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Test : MonoBehaviour
{
	[Header("Variables")]
	private bool enabled = true;
    private int count = -1;
	string message;
	private bool completetext = true;
    
	//bool animateBook = false;
	//bool animateChest = false;
    
	private bool didSeeKeyPress = false;
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
    public AudioClip[] part;

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
            //bkgdaudiosource.volume = 0;
            audiosource.volume = 0;
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

            if (objectApp && inTutorialMode)
                setTap();

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

            //if ((PlayerPrefs.GetInt("educationOn") == 0) && (PlayerPrefs.GetInt("therapyOn") == 1))
            //{
            //    if ((PlayerPrefs.GetInt("key1toggle") == 0) && key1Press)
            //    {
            //        setTap();
            //        OnClick();
            //    }
            //    if ((PlayerPrefs.GetInt("key2toggle") == 0) && key2Press)
            //    {
            //        setTap();
            //        OnClick();
            //    }
            //    if ((PlayerPrefs.GetInt("key3toggle") == 0) && key3Press)
            //    {
            //        setTap();
            //        OnClick();
            //    }
            //    if ((PlayerPrefs.GetInt("keySpacetoggle") == 0) && keyspacePress)
            //    {
            //        setTap();
            //        OnClick();
            //    }
            //    if ((PlayerPrefs.GetInt("keyEntertoggle") == 0) && keyenterPress)
            //    {
            //        setTap();
            //        OnClick();
            //    }
            //    key1Press = false;
            //    key2Press = false;
            //    key3Press = false;
            //    keyspacePress = false;
            //    keyenterPress = false;
            //}

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
		if (!inTutorialMode)
		{
			load = false;
			//enabled = false;
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

        switch (count)
        {
            case 7:
                if (!playedGame1)
                    playedGame1 = true;
                break;
            case 17:
                if (!playedGame2)
                    playedGame2 = true;
                break;
            case 27:
                if (!playedGame3)
                    playedGame3 = true;
                break;
        }

        audiosource.Stop();
		mainCamera.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync("LoadingScreen");
        storyView.alpha = 0f;

        StartCoroutine(GoToScene("MiniGameScreen_Story"));
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
        float startA = w.alpha;
        yield return null;

        GameObject prefab = null;

        switch (count)
        {
            case -1:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                if (!back)
                {
                    yield return new WaitForSeconds(0.25F);
                    Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")"));
                }
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                back = false;
                audiosource.clip = part[count + 1];
                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image1") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Shapes are here to show us the way. \n They make us giggle and help us play.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 0:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];
                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image2") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Tap a shape and be sure to shout.\n  Make sure to name them as you point them out!";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 1:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                audiosource.clip = part[count + 1];
                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image3") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Circles are shapes that go round and round.\n Holes can be circles dug in the ground.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 2:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];
                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image4") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Circles for a bright moon that you see.\n Circles for yummy cupcakes baked with glee.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 3:
                inMiniGameMode = true;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image5") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Tap the circles and be sure to shout.\n  Make sure to name them as you point them out!";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 4:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image6") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Squares have 4 sides that are equal, indeed.\n Books can be squares that you can read.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 5:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image7") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Squares for a table on the side of your bed.\n Squares for a mirror to see the top of your head.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 6:
                inMiniGameMode = true;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image8") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Tap the squares and be sure to shout.\n  Make sure to name them as you point them out!";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 7:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image9") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Triangles have 3 points and sides on any day.\n Arrows can be triangles showing the way.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 8:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image10") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Triangles for a cheese wedge for mice.\n Triangles for a tasty pizza slice.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 9:
                inMiniGameMode = true;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image11") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Tap the triangles and be sure to shout.\n  Make sure to name them as you point them out!";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 10:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];
                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image12") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Ovals are a circle squished like a sleeve.\n Grapes can be ovals and are tasty indeed.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 11:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image13") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Ovals for a bathtub where you scrub your feet.\n Ovals for eggs that make tasty grub to eat.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 12:
                inMiniGameMode = true;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image14") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Tap the ovals and be sure to shout.\n  Make sure to name them as you point them out!";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 13:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image15") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Diamonds are two triangles attached head to head.\n Rings can have diamonds that last forever it is said.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 14:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image16") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Diamonds for a baseball field with bases and a home.\n Diamonds for a kite that you fly and let roam.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 15:
                inMiniGameMode = true;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image17") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Tap the diamonds and be sure to shout.\n Make sure to name them as you point them out!";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 16:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image18") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Stars have 5 points that always stick out.\n Badges can be stars on uniforms no doubt.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 17:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image19") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Stars for a sun that gives us heat and light.\n Stars for a starfish that moves around at night.";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 18:
                inMiniGameMode = true;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image20") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Tap the stars and be sure to shout.\n  Make sure to name them as you point them out!";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 19:
                inMiniGameMode = false;
                audiosource.Stop();
                Destroy(GameObject.Find("UI Root/Part (" + (count + 2) + ")(Clone)"));
                prefab = Instantiate(Resources.Load("Prefabs/Part (" + (count + 2) + ")")) as GameObject;
                prefab.transform.SetParent(storyParent.transform);
                prefab.transform.localScale = new Vector3(100, 100, 1);
                Destroy(GameObject.Find("UI Root/Part (" + (count + 1) + ")(Clone)"));
                Destroy(GameObject.Find("UI Root/Part (" + (count + 3) + ")(Clone)"));
                //yield return new WaitForSeconds(0.5F);
                audiosource.clip = part[count + 1];

                audiosource.Play();
                count++;
                sprite.mainTexture = Resources.Load("image21") as Texture;
                StartCoroutine(FadeIn(sprite, 0.5f, startA));
                message = "Shapes, shapes, are found everywhere.\n  Next time you find one, make sure to share!";
                StartCoroutine(TypeText(nextAlpha, coverAlpha));
                break;
            case 20:
                inMiniGameMode = false;
                StartCoroutine(DisplayScene());
                break;
            default:
                break;
        }

        back = false;
        //print("unload the toad");
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

