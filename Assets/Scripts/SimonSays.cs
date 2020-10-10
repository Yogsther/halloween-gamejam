using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSays : Puzzle {

    List<GameObject> buttons = new List<GameObject>();
    int[] numbers = new int[5];
    int stage;

    public override void Setup() {
        base.Setup();
        stage = 0;
        buttons = new List<GameObject>();
        for (int i = 0; i < 9; i++) buttons.Add(null);
        for (int i = 0; i < numbers.Length; i++) {
            numbers[i] = Random.Range(0, 8);
        }

        Transform canvas = GameObject.Find("2DCanvas").transform;
        GameObject button = gm.gameObject.GetComponent<AssetPack>().SimonSaysButton;
        for (int i = 0; i < 9; i++) {
            int index = i;
            buttons[i] = (Instantiate(button, canvas));
            buttons[i].GetComponent<Button>().onClick.AddListener(() => { Click(index); });
            buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-100 + ((i % 3) * 100), -110 + (Mathf.Floor(i / 3) * 100));
        }
    }

    public void Click(int key) {
        Debug.Log("Click: " + key);
    }

    public override void Unload() {
        for (int i = 0; i < buttons.Count; i++) {
            Destroy(buttons[i]);
        }
    }




    void Update() {

    }
}
