﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : Puzzle {

    public GameObject terminal;
    InputField input;

    public override void Setup() {
        base.Setup();
        terminal = Instantiate(gm.gameObject.GetComponent<AssetPack>().Terminal, gm.canvas);
        input = terminal.GetComponent<InputField>();
        input.Select();
        input.ActivateInputField();
        input.onValueChanged.AddListener(OnInput);
    }

    public void OnInput(string value) {
        if (gm.gameOver) return;
        input.text = value.ToUpper();
        if (input.text.Length == 8) {
            if (input.text == gm.password) {
                gm.EndGame();
            } else {
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
