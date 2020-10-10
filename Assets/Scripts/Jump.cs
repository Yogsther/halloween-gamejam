using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Puzzle
{
    GameObject player;
    public override void Setup()
    {
        base.Setup();
        Transform canvas = GameObject.Find("2DCanvas").transform;
        GameObject player = gm.gameObject.GetComponent<AssetPack>().jumpGamePlayer;
        player = Instantiate(player, canvas);
        player.GetComponent<RectTransform>().anchoredPosition = new Vector2(3, 5);
    }

    public override void Unload()
    {
        Destroy(player);
    }
    void Update()
    {
        
    }
}
