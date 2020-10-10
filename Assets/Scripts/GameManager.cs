using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public bool inPuzzle = false;
    public Puzzle activePuzzle;
    public GameObject puzzleRunner;
    public TextAsset rawWords;
    public string[] words;

    public Transform canvas;

    public bool gameOver = true;

    public float time = 120;
    public Text timer;

    public Button backButton;
    public GameObject arrowButtons;

    public string password;
    public char[] passwordHint;
    public Text passwordHintObject;

    public Transform gameOverScreen;

    void Start() {
        gameOver = true;
        words = rawWords.text.Split(' ');
        StartGame();
    }

    public void StartGame() {
        if (!gameOver) return;
        password = words[Random.Range(0, words.Length - 1)];
        time = 120;
        passwordHint = new char[8];
        backButton.gameObject.SetActive(false);
        gameOver = false;
        gameOverScreen.gameObject.SetActive(false);
    }

    public void EndGame() {
        if (gameOver) return;
        CameraMovement cm = Camera.main.GetComponent<CameraMovement>();
        cm.direction = 0;
        cm.desiredDirection = 0;

        Back();
        gameOver = true;
        gameOverScreen.gameObject.SetActive(true);

        Text topText = gameOverScreen.Find("TopText").GetComponent<Text>();
        Text bottomText = gameOverScreen.Find("BottomText").GetComponent<Text>();

        bool won = time > 0;

        Color color = won ? Color.green : Color.red;

        topText.text = won ? "You won!" : "You lost!";
        topText.color = color;
        bottomText.text = won ? "... with " + GetTimeLeft() + " seconds left" : "Good luck next time :(";
        bottomText.text += "\nPress [ENTER] to play again.";
        bottomText.color = color;
    }

    public void Back() {
        if (!inPuzzle) return;
        backButton.gameObject.SetActive(false);
        arrowButtons.SetActive(true);

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


        if (missingLetters.Count > 0) {
            int index = Random.Range(0, missingLetters.Count - 1);
            passwordHint[missingLetters[index]] = password[missingLetters[index]];
        }

        DrawHint();
    }


    public void GivePenalty(float amount) {
        time -= amount;
        SetTimerColor(Color.red);
        StartCoroutine(ResetTimeColor());
    }

    void SetTimerColor(Color color) {
        timer.color = color;
    }

    IEnumerator ResetTimeColor() {
        yield return new WaitForSeconds(.4f);
        SetTimerColor(Color.white);
    }

    string GetTimeLeft() {
        return string.Format("{0,6:##0.00}", time);
    }

    void Update() {

        if (!gameOver) {
            time -= Time.deltaTime;
            timer.text = GetTimeLeft();

            if (time <= 0) {
                EndGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (gameOver) StartGame();
            GiveHintLetter();
        }

        if (Input.GetMouseButton(0) && !gameOver) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000000f, LayerMask.GetMask("Clickable"))) {
                if (!inPuzzle) {
                    inPuzzle = true;
                    backButton.gameObject.SetActive(true);
                    arrowButtons.SetActive(false);
                    puzzleRunner.AddComponent(hit.transform.gameObject.GetComponent<Puzzle>().GetType());
                    activePuzzle = puzzleRunner.GetComponent<Puzzle>();
                    activePuzzle.Setup();
                }
            }
        }
    }


}
