using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public bool inPuzzle = false;
    public Puzzle activePuzzle;
    public GameObject puzzleRunner;
    public TextAsset rawWords;
    public string[] words;

    public Button backButton;

    public string password;
    public char[] passwordHint = new char[8];
    public Text passwordHintObject;

    void Start() {
        backButton.gameObject.SetActive(false);
        words = rawWords.text.Split(' ');
        password = words[Random.Range(0, words.Length - 1)];
    }

    public void Back() {
        if (!inPuzzle) return;
        backButton.gameObject.SetActive(false);

        activePuzzle.Unload();
        Destroy(activePuzzle);
        activePuzzle = null;
        inPuzzle = false;
    }

    void DrawHint() {
        string hintText = "";
        for (int i = 0; i < passwordHint.Length; i++) {
            hintText += passwordHint[i] == (char)0 ? "_" : passwordHint[i].ToString();
            hintText += " ";
        }
        passwordHintObject.text = hintText;
    }

    public void GiveHintLetter() {
        List<int> missingLetters = new List<int>();
        for (int i = 0; i < passwordHint.Length; i++) {
            if (passwordHint[i] == (char)0) missingLetters.Add(i);
        }

        Debug.Log(string.Join(", ", missingLetters));

        if (missingLetters.Count > 0) {
            int index = Random.Range(0, missingLetters.Count - 1);
            Debug.Log("Random: " + index);
            passwordHint[missingLetters[index]] = password[missingLetters[index]];
        }



        DrawHint();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Return)) {
            GiveHintLetter();
        }

        if (Input.GetMouseButton(0)) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000000f, LayerMask.GetMask("Clickable"))) {
                if (!inPuzzle) {
                    inPuzzle = true;
                    backButton.gameObject.SetActive(true);
                    puzzleRunner.AddComponent(hit.transform.gameObject.GetComponent<Puzzle>().GetType());
                    activePuzzle = puzzleRunner.GetComponent<Puzzle>();
                    activePuzzle.Setup();
                }
            }
        }
    }


}
