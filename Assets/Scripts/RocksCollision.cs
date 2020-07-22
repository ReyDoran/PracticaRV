using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider collisionRock)
    {
        if (collisionRock.GetComponent<Collider>().tag == "CollisionRocks")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
