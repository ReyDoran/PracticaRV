using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private PlayerMovement player;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey("w")) player.Pitch(45);
        if (Input.GetKey("s")) player.Pitch(-45);
        if (Input.GetKey("a")) player.Yaw(-45);
        if (Input.GetKey("d")) player.Yaw(45);
    }
}
