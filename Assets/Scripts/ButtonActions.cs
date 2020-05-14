using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");

    }

    public void ExitButton()
    {
        UnityEngine.Application.Quit();
    }
}
