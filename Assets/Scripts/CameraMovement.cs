using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    int direction = 0;

    // Start is called before the first frame update
    void Start() {

    }

    public void ChangeCameraLook(int way) {
        direction += way * 90;
    }



    // Update is called once per frame
    void Update() {
        float mouseX = (Input.mousePosition.x / Screen.width) / 50;
        float mouseY = (Input.mousePosition.y / Screen.height) / 50;
        transform.localRotation = Quaternion.Euler(new Vector4(-1f * (mouseY * 180f), mouseX * 360f * direction, transform.localRotation.z));
    }
}
