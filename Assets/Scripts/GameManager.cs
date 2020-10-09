using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    void Start() {

    }
    void Update() {
        if (Input.GetMouseButton(0)) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000000f, LayerMask.GetMask("Clickable"))) {

                Debug.Log(hit.transform.gameObject);
            }
        }
    }
}
