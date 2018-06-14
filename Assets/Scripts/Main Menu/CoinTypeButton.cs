using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTypeButton : MonoBehaviour {

    public UIPanel settingsPanel;
    public UIPanel coinSelectPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        settingsPanel.alpha = 0;
        coinSelectPanel.alpha = 1;
    }
}
