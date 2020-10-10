using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

    public GameManager gm;


    public virtual void Setup() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public virtual void Unload() {

    }

    void Completed() {

    }

    void Failed() {

    }


    void Update() {

    }
}
