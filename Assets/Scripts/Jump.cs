using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Puzzle
{
    GameObject player;
    GameObject floor;
    GameObject obstacle;
    public override void Setup()
    {
        base.Setup();
        Transform canvas = GameObject.Find("2DCanvas").transform;
        player = gm.gameObject.GetComponent<AssetPack>().jumpGamePlayer;
        floor = gm.gameObject.GetComponent<AssetPack>().jumpGameFloor;
        obstacle = gm.gameObject.GetComponent<AssetPack>().jumpGameObstacle;

        player = Instantiate(player, canvas);
        floor = Instantiate(floor, canvas);
        obstacle = Instantiate(obstacle, canvas);

        player.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        floor.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -65);
        obstacle.GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -22.5f);


    }

    public override void Unload()
    {
        Destroy(player);
        Destroy(floor);
        Destroy(obstacle);
    }
    void Update()
    {
        obstacle.GetComponent<RectTransform>().transform.Translate(-1, 0, 0);
        if (obstacle.GetComponent<RectTransform>().anchoredPosition.x <= -350)
        {
            obstacle.GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -22.5f);
        }


        if (Input.GetButtonDown("Jump") && player.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 500);
            //player.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 120);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log("test");
    }
}
