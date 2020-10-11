using System.Collections;
using UnityEngine;

public class Puzzle : MonoBehaviour {


    [HideInInspector] public GameManager gm;
    [HideInInspector] public bool failed;

    public bool hard = false;
    public int rewards = 2;

    public virtual void Setup() {
        failed = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public virtual void Unload() {

    }

    public void Completed() {
        StartCoroutine(CompleteDelay());

    }

    public void Failed() {
        failed = true;
        gm.GivePenalty(10);
        StartCoroutine(WaitForRestart());
    }

    IEnumerator CompleteDelay() {
        for (int i = 0; i < rewards; i++) {
            gm.GiveHintLetter();
            yield return new WaitForSeconds(.25f);
        }
        gm.Back();
    }

    IEnumerator WaitForRestart() {
        yield return new WaitForSeconds(2);
        Unload();
        Setup();
    }


    void Update() {

    }
}
