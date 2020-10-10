using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float direction = 0;
    public float desiredDirection = 0;
    public float offset = 20;
    float vel;

    // Start is called before the first frame update
    void Start() {

    }

    public void ChangeCameraLook(int way) {
        desiredDirection += way * 90;
    }



    // Update is called once per frame
    void Update() {

        direction = Mathf.SmoothDamp(direction, desiredDirection, ref vel, .1f);
        float mouseX = (Input.mousePosition.x / Screen.width) / 50;
        float mouseY = (Input.mousePosition.y / Screen.height) / 50;
        transform.localRotation = Quaternion.Euler(new Vector4(-1f * (mouseY * 180f) + offset, mouseX * 360f + direction, transform.localRotation.z));
    }
}
