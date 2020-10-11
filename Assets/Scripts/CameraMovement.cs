using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float direction = 0;
    public float desiredDirection = 0;
    public float offset = 20;
    public float offsetX = 0;
    float vel;

    public Transform monkeySpawns;
    public GameObject monkeyModel;

    // Start is called before the first frame update
    void Start() {

    }

    public void ChangeCameraLook(int way) {
        desiredDirection += way * 90;

        if (Random.Range(0, 3) == 0) {
            Debug.Log("Spawning monkey");
            List<Transform> nonVisibleSpawns = new List<Transform>();
            foreach (Transform spawn in monkeySpawns) {

                if (spawn.childCount == 0) {
                    nonVisibleSpawns.Add(spawn);
                }
            }
            if (nonVisibleSpawns.Count > 0) {
                Transform parent = nonVisibleSpawns[Random.Range(0, nonVisibleSpawns.Count)];
                Instantiate(monkeyModel, parent.position, parent.rotation, parent);
                Debug.Log("Spawned monkey");
            }
        }
    }



    // Update is called once per frame
    void Update() {

        direction = Mathf.SmoothDamp(direction, desiredDirection, ref vel, .1f);
        float mouseX = (Input.mousePosition.x / Screen.width) / 50;
        float mouseY = (Input.mousePosition.y / Screen.height) / 50;
        transform.localRotation = Quaternion.Euler(new Vector4(-1f * (mouseY * 180f) + offset, mouseX * 360f + direction + offsetX, transform.localRotation.z));
    }
}
