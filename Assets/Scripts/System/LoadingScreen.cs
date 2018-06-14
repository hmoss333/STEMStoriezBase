using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {

    public UISprite coinSprite;

    float rotSpeed = 2.5f;
    
    // Use this for initialization
	void Start () {
        SelectRandomCoinSprite();
	}
	
	// Update is called once per frame
	void Update () {
        coinSprite.transform.Rotate(new Vector3(0, 0, -rotSpeed), Space.Self);
	}

    void SelectRandomCoinSprite ()
    {
        int randNum = Random.Range(0, coinSprite.atlas.spriteList.Count);

        coinSprite.spriteName = coinSprite.atlas.spriteList[randNum].name;
        //Debug.Log(coinSprite.spriteName);
    }
}
