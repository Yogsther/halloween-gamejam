using System.Collections;
using UnityEngine;

public class Puzzle : MonoBehaviour {


    [HideInInspector] public GameManager gm;
    [HideInInspector] public bool failed;

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
        for (int i = 0; i < 2; i++) {
            gm.GiveHintLetter();
            yield return new WaitForSeconds(.5f);
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
