using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Audio : MonoBehaviour {

    public AudioSource ambientSource;
    public AudioSource effectSource;

    public AudioClip ComputerWriting1;
    public AudioClip ComputerWriting2;
    public AudioClip ComputerWriting3;
    public AudioClip ComputerWriting4;

    public AudioClip WordCorrect1;
    public AudioClip WordCorrect2;
    public AudioClip WordCorrect3;
    public AudioClip WordCorrect4;

    public AudioClip ComputerRightPass;
    public AudioClip ComputerWrongPass;
    public AudioClip WordIncorrect;
    public AudioClip PickingUpLetter;
    public AudioClip SimonCorrect;
    public AudioClip SimonIncorrect;
    public AudioClip SimonFollow;
    public AudioClip SimonPress;

    public AudioClip Ambience2Min;
    public AudioClip ComputerHumming;
    public AudioClip LampBuzzing;
    public AudioClip LightBurning;


    public void RestartAmbient() {
        ambientSource.clip = Ambience2Min;
        ambientSource.Play();
    }

    public void PlayEffect(AudioClip clip) {
        effectSource.clip = clip;
        effectSource.Play();
    }

    void Start() {

    }

    void Update() {

    }
}
