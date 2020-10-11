using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : Puzzle {

    public GameObject terminal;
    InputField input;
    float lastPlayed;


    public override void Setup() {
        base.Setup();
        lastPlayed = 0;
        terminal = Instantiate(gm.gameObject.GetComponent<AssetPack>().Terminal, gm.canvas);
        input = terminal.GetComponent<InputField>();
        input.Select();
        input.ActivateInputField();
        input.onValueChanged.AddListener(OnInput);
    }

    public void OnInput(string value) {
        if (gm.gameOver) return;


        if (Time.time - lastPlayed > .05f) {
            lastPlayed = Time.time;

            AudioClip[] clips = { gm.audio.ComputerWriting1, gm.audio.ComputerWriting2, gm.audio.ComputerWriting3, gm.audio.ComputerWriting4 };
            gm.audio.PlayEffect(clips[Random.Range(0, 4)]);
        }



        input.text = value.ToUpper();
        if (input.text.Length == 8) {
            if (input.text == gm.password) {
                gm.audio.PlayEffect(gm.audio.ComputerRightPass);
                gm.EndGame();
            } else {
                gm.audio.PlayEffect(gm.audio.ComputerWrongPass);
                gm.GivePenalty(10);
            }
        }
    }

    public override void Unload() {
        base.Unload();
        Destroy(terminal);
    }


    void Update() {

    }
}
