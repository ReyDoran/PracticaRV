using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    //Lista de aros que crean la pista
    public List<RingScript> ringList;
    //Variable que escucha a los aros
    public bool ringReached;
    //Indice del aro actual
    private int ringIndex;

    private bool raceFinished;

    // Start is called before the first frame update
    void Start()
    {
        raceFinished = false;
        ringIndex = -1;
        ringReached = true;        
    }

    // Update is called once per frame
    void Update()
    {
        //Si se ha pasado por un aro activamos el siguiente y reiniciamos la variable
        if (ringReached == true)
        {
            ringReached = false;
            ringIndex++;
            if (ringList.Count > ringIndex)
                ringList[ringIndex].Activate();
            else raceFinished = true;
        }
        if (raceFinished == true) Debug.Log("GG WP");
    }
}
