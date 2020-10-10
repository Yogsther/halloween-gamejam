using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Puzzle
{
    GameObject player;
    GameObject floor;
    GameObject obstacle;
    bool offScreen;
    public bool dead;
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
        obstacle.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, -22.5f);

        StartCoroutine(ObstacleLoop());
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
        if (obstacle.GetComponent<RectTransform>().anchoredPosition.x <= -200)
        {
            offScreen = true;
            obstacle.SetActive(false);
            //obstacle.GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -22.5f);
        }
        else
        {
            offScreen = false;
            obstacle.SetActive(true);
        }

        //Debug.Log(offScreen);
        if (Input.GetButtonDown("Jump") && player.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 700);
            //player.GetComponent<Rigidbody2D>().AddForce(transform.up * 50000, ForceMode2D.Force);
            //player.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 120);
        }
        Debug.Log(dead);
    }
    public void Test()
    {
        dead = true;
        //StartCoroutine(Fail());
    }
    private IEnumerator ObstacleLoop()
    {
        yield return StartCoroutine(ObstacleOffscreen());
        yield return StartCoroutine(ObstacleReposition());
        
        if (dead)
        {
            StartCoroutine(Fail());
        }
        else
        {
            StartCoroutine(ObstacleLoop());
        }
        //obstacle.GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -22.5f);
    }

    IEnumerator ObstacleOffscreen()
    {
        int waitTime = Random.Range(0, 3);
        //Debug.Log(waitTime);
        while (!offScreen)
        {
            yield return new WaitForSeconds(waitTime);
        }
        
    }

    IEnumerator ObstacleReposition()
    {
        obstacle.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, -22.5f);
        //Failed();
        yield return null;
    }
    IEnumerator Fail()
    {
        Debug.Log("gfdsg");
        Failed();
        yield return null;
    }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Obstacle")
            {
            Test();
            //StartCoroutine(Fail());
            //Test();
        }

        }
    
}
