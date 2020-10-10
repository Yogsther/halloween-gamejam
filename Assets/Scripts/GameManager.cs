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

    public string password;
    public string[] passwordHint = new string[8];
    public Text passwordHintObject;

    void Start() {
        words = rawWords.text.Split(' ');
    }

    public void Back() {
        if (!inPuzzle) return;

        activePuzzle.Unload();
        Destroy(activePuzzle);
        activePuzzle = null;
        inPuzzle = false;
    }

    void DrawHint() {
        string hintText = "";
        for (int i = 0; i < passwordHint.Length; i++) {
            hintText += passwordHint[i] == null ? "_" : passwordHint[i];
            hintText += " ";
        }
        passwordHintObject.text = hintText;
    }

    void Update() {

        if (Input.GetMouseButton(0)) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000000f, LayerMask.GetMask("Clickable"))) {
                if (!inPuzzle) {
                    inPuzzle = true;

                    puzzleRunner.AddComponent(hit.transform.gameObject.GetComponent<Puzzle>().GetType());
                    activePuzzle = puzzleRunner.GetComponent<Puzzle>();
                    activePuzzle.Setup();
                }
            }
        }
    }


}
