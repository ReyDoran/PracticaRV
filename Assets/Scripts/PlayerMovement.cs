using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Posición de la nave
    private Transform transf;
    //Cuerpo de la nave (para trabajar con físicas de Unity)
    private Rigidbody body;
    //Indica el factor de escalado de los ángulos del volante al avión
    public float rollFactor;
    //Indica el factor de escalado de los ángulos del volante al avión
    public float pitchFactor;
    //Indica el factor de escalado de los ángulos del volante al avión
    public float yawFactor;
    //Indica la velocidad máxima de la nave
    public float maxVelocity;
    //Fuerza de aceleración de la nave
    public float acceleration;
    //Modelo de la cabina (para simular alabeo)
    public Transform cabin;
    //Factor de rotación falsa de alabeo (multiplica al ángulo del volante)
    public float fakeRollFactor;

    void Start()
    {
        //Obtenemos ambos componentes de la instancia
        transf = GetComponent<Transform>();
        body = GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    //Update para trabajar con físicas (sin diferencia entre procesadores)
    private void FixedUpdate()
    {
        //Si no se pasa de la velocidad máxima se sigue acelerando
        if (body.velocity.z < maxVelocity)
        {
            //Aplicamos fuerza para moverse hacia adelante
            body.AddRelativeForce(0f, 0f, acceleration);
        }
        Vector3 XYRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(XYRotation.x, XYRotation.y, 0f);
    }

    //Función para realizar alabeo, en función de parámetro de entrada y rollFactor
    //El parámetro será la rotación del código QR
    //DEBE LLAMARSE EN UN FixedUpdate()!
    public void Roll(float angle)
    {
        body.AddRelativeTorque(Vector3.forward * rollFactor * angle);
    }

    //Función para realizar cabeceo, en función de parámetro de entrada y pitchFactor
    //El parámetro será la rotación del código QR
    //DEBE LLAMARSE EN UN FixedUpdate()!
    public void Pitch(float angle)
    {
        if (transform.rotation.eulerAngles.x < 80 || transform.rotation.eulerAngles.x > 290)
        {
            body.AddRelativeTorque(Vector3.left * pitchFactor * angle);
        }
        else if (transform.rotation.eulerAngles.x > 80 && transform.rotation.eulerAngles.x < 90 && angle > 0)
        {
            body.AddRelativeTorque(Vector3.left * pitchFactor * angle);
        }
        else if (transform.rotation.eulerAngles.x < 290 && transform.rotation.eulerAngles.x > 280 && angle < 0)
        {
            body.AddRelativeTorque(Vector3.left * pitchFactor * angle);
        }
        //Debug.Log(transform.rotation.eulerAngles.x);
    }

    //Función para realizar guiñada, en función de parámetro de entrada y yawFactor
    //El parámetro será la rotación del código QR
    //DEBE LLAMARSE EN UN FixedUpdate()!
    public void Yaw(float angle)
    {
        //position.Rotate(Vector3.up * yaw * angle, Space.World);
        body.AddTorque(Vector3.up * yawFactor * -angle);
        Vector3 cabinRotation = cabin.rotation.eulerAngles;
        cabin.rotation = Quaternion.Euler(cabinRotation.x, cabinRotation.y, - angle * fakeRollFactor);
    }

    public void RelativePitch()
    {
        transf.Rotate(new Vector3(5f, 0f, 0f));
    }

    public void AbsolutePitch(float degrees)
    {

    }

    public void RelativeRoll()
    {
        transf.Rotate(new Vector3(0f, 0f, 5f));
    }

    public void AbsoluteRoll(float degrees)
    {

    }
}
