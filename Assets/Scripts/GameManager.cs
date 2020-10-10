using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool inPuzzle = false;
    public Puzzle activePuzzle;
    public GameObject puzzleRunner;
    public TextAsset words;


    void Start() {
        Debug.Log(words.text);
    }

    public void Back() {
        if (!inPuzzle) return;

        activePuzzle.Unload();
        Destroy(activePuzzle);
        activePuzzle = null;
        inPuzzle = false;
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
