using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    //Variable publica compartida que comprueba cuando se ha pasado por un aro
    public bool ringReached;
    //Variable que almacena si ya se ha pasado por el aro y debe desactivarse
    private bool activated;
    public MeshRenderer mesh;
    public Material activatedMaterial;
    public Material deactivatedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        Activate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
        {
            Deactivate();
            ringReached = true;
        }
    }

    public void Activate()
    {
        activated = true;
        mesh.material = activatedMaterial;
    }

    public void Deactivate()
    {
        activated = false;
        mesh.material = deactivatedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
