using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeySpawn : MonoBehaviour {
    public bool visible;
    Renderer rend;
    void Start() {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        visible = rend.isVisible;
    }
}
