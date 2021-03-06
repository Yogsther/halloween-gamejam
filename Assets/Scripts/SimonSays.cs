﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSays : Puzzle {

    List<GameObject> buttons = new List<GameObject>();


    int[] numbers;
    int stage;
    int at;
    bool playingSequence;


    public override void Setup() {
        base.Setup();

        numbers = new int[hard ? 10 : 5];
        stage = 0;
        playingSequence = false;
        at = 0;

        if (hard) rewards = 4;
        else rewards = 2;

        buttons = new List<GameObject>();
        for (int i = 0; i < 9; i++) buttons.Add(null);
        for (int i = 0; i < numbers.Length; i++) {
            numbers[i] = Random.Range(0, 8);
        }

        GameObject button = gm.gameObject.GetComponent<AssetPack>().SimonSaysButton;
        for (int i = 0; i < 9; i++) {
            int index = i;
            buttons[i] = (Instantiate(button, gm.canvas));
            buttons[i].GetComponent<Button>().onClick.AddListener(() => { Click(index); });
            buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-100 + ((i % 3) * 100), -110 + (Mathf.Floor(i / 3) * 100));
        }

        StartCoroutine(Sequence());
    }

    public void Click(int key) {
        if (failed || playingSequence) return;
        DimAll();
        if (numbers[at] == key) {

            gm.audio.PlayEffect(gm.audio.SimonPress);
            LightUp(key, Color.green);

            if (at == numbers.Length - 1) {

                gm.audio.PlayEffect(gm.audio.SimonCorrect);
                LightComplete();
                Completed();

            } else if (at == stage - 1) {
                StartCoroutine(Sequence());
            } else {
                at++;
            }

        } else {
            gm.audio.PlayEffect(gm.audio.SimonIncorrect);
            LightFail();
            Failed();
        }
    }

    public override void Unload() {
        for (int i = 0; i < buttons.Count; i++) {
            Destroy(buttons[i]);
        }
    }

    public void LightUp(int index, Color color) {
        DimAll();
        buttons[index].GetComponent<Image>().color = color;
    }

    public void LightComplete() {
        for (int i = 0; i < 9; i++) {
            buttons[i].GetComponent<Image>().color = Color.green;
        }
    }

    public void LightFail() {
        for (int i = 0; i < 9; i++) {
            buttons[i].GetComponent<Image>().color = Color.red;
        }
    }

    public void DimAll() {
        for (int i = 0; i < 9; i++) {
            buttons[i].GetComponent<Image>().color = Color.white;
        }
    }

    IEnumerator Sequence() {
        playingSequence = true;
        stage++;
        at = 0;
        yield return new WaitForSeconds(.2f);

        for (int i = 0; i < stage; i++) {
            DimAll();
            yield return new WaitForSeconds(hard ? .1f : .25f);
            gm.audio.PlayEffect(gm.audio.SimonFollow, .2f);
            LightUp(numbers[i], Color.blue);
            yield return new WaitForSeconds(hard ? .4f : .7f);
            DimAll();
        }

        DimAll();
        playingSequence = false;
    }


    void Update() {

    }
}
