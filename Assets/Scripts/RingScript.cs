using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    //Variable publica compartida que comprueba cuando se ha pasado por un aro
    public bool ringReached;
    //Variable que almacena si ya se ha pasado por el aro y debe desactivarse
    private bool activated = false;
    public MeshRenderer mesh;
    public Material activatedMaterial;
    public Material deactivatedMaterial;
    public RaceManager raceManager;
    private Transform meshTransf;
    public GameObject pointLight;

    // Start is called before the first frame update
    void Start()
    {
        Deactivate();
        raceManager = (RaceManager) GameObject.FindObjectOfType(typeof(RaceManager));
        meshTransf = mesh.GetComponent<Transform>();
    }

    void Update()
    {
        meshTransf.Rotate(0, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
        {
            Deactivate();
            raceManager.ringReached = true;
        }
    }

    public void Activate()
    {
        activated = true;
        pointLight.SetActive(true);
        //mesh.material = activatedMaterial;
    }

    public void Deactivate()
    {
        activated = false;
        pointLight.SetActive(false);
        //mesh.material = deactivatedMaterial;
    }

}
