using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Puzzle
{
    GameObject player;
    GameObject floor;
    public override void Setup()
    {
        base.Setup();
        Transform canvas = GameObject.Find("2DCanvas").transform;
        player = gm.gameObject.GetComponent<AssetPack>().jumpGamePlayer;
        floor = gm.gameObject.GetComponent<AssetPack>().jumpGameFloor;
        floor = Instantiate(floor, canvas);
        player = Instantiate(player, canvas);
        floor.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -65);
        player.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 120);
        
    }

    public override void Unload()
    {
        Destroy(player);
        Destroy(floor);
    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 120);
            Debug.Log("up");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120);
            Debug.Log("up");
        }*/
        if (Input.GetButtonDown("Jump"))
        {
            player.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 120);
        }
    }
}
