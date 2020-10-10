using System.Collections;
using UnityEngine;

public class Puzzle : MonoBehaviour {

    public GameManager gm;
    public bool failed;


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
        StartCoroutine(WaitForRestart());
    }

    IEnumerator CompleteDelay() {
        yield return new WaitForSeconds(.5f);
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
