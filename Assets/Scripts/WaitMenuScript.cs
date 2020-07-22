using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;
using UnityEngine.UI;

public class WaitMenuScript : MonoBehaviour
{

    //timer
    public float tiempo_start;
    public float tiempo_end;

    public VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    // Start is called before the first frame update

    void Update()
    {

        float inicio = 20;
        tiempo_end = 28;
        tiempo_start += Time.deltaTime;

        if (tiempo_start >= inicio)
        {
            videoPlayer.Play();
        }
        if (tiempo_start >= tiempo_end)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
        }


    }

}
