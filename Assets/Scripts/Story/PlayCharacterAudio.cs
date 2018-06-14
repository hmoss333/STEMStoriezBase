using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCharacterAudio : MonoBehaviour {

    public AudioClip characterAudio;
    AudioSource specialAudiosource;


    // Use this for initialization
    void Start()
    {
        specialAudiosource = GameObject.Find("SpecialAudiosource").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        specialAudiosource.Stop();
        specialAudiosource.clip = characterAudio;
        specialAudiosource.Play();
        //Debug.Log("played character audio");
    }
}
