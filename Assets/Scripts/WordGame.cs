using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coordinates {
    public int x, y;
    public Coordinates(int x, int y) {
        this.x = x;
        this.y = y;
    }
}

public class WordGame : Puzzle {

    char[] matrix = new char[64];
    GameObject[] buttons = new GameObject[64];

    AssetPack ap;
    GameObject board;

    int progress;

    string word;
    int[] keys = new int[8];

    public override void Setup() {
        base.Setup();

        progress = 0;

        word = gm.words[Random.Range(0, gm.words.Length - 1)];

        ap = gm.gameObject.GetComponent<AssetPack>();
        board = Instantiate(ap.WordGameObject, gm.canvas);

        GameObject buttonTemplate = board.transform.Find("WordGameButton").gameObject;

        for (int i = 0; i < 64; i++) {
            buttons[i] = Instantiate(buttonTemplate, board.transform);
            GameObject button = buttons[i];
            button.SetActive(true);
            Coordinates coords = GetCoordinates(i);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(coords.x * 40 - 141, coords.y * -40 + 140);
        }

        bool horizontalWord = Random.Range(0, 2) == 0;
        int start = Random.Range(0, 7);

        int x = horizontalWord ? 0 : start;
        int y = horizontalWord ? start : 0;

        for (int i = 0; i < 8; i++) {
            int index = GetIndex(x, y);
            keys[i] = index;
            matrix[index] = word[i];
            if (horizontalWord) x++;
            else y++;
        }

        for (int i = 0; i < 64; i++) {
            if (matrix[i] == (char)0) matrix[i] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[Random.Range(0, 25)];
            buttons[i].transform.Find("Text").GetComponent<Text>().text = matrix[i].ToString();
            int index = i;
            buttons[i].GetComponent<Button>().onClick.AddListener(() => {
                Click(index);
            });
        }

    }

    void Click(int index) {
        GameObject button = buttons[index];

        bool correctClick = false;
        for (int i = 0; i < keys.Length; i++) {
            if (keys[i] == index) {
                correctClick = true;
                break;
            }
        }

        if (correctClick) {
            if (button.GetComponent<Image>().color != Color.green) {
                button.GetComponent<Image>().color = Color.green;
                progress++;
                if (progress == 8) {
                    Completed();
                }
            }
        } else {
            button.GetComponent<Image>().color = Color.red;
            gm.GivePenalty(10);
            StartCoroutine(Clear());
        }
    }

    IEnumerator Clear() {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 64; i++) {
            buttons[i].GetComponent<Image>().color = Color.white;
        }
    }

    public int GetIndex(int x, int y) {
        return x + 8 * y;
    }

    public Coordinates GetCoordinates(int index) {
        int x = index % 8;
        int y = (int)Mathf.Floor(index / 8);
        return new Coordinates(x, y);
    }



    public override void Unload() {
        Destroy(board);
    }

    void Update() {

    }
}


