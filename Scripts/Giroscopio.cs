using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giroscopio : MonoBehaviour
{
    public GameObject VRVision;
    public float posicionInicial = 0f;
    public float posicionGiro = 0f;
    public float calibrarPosicion = 0f;
    public bool flagGameStart;





    // Start is called before the first frame update
    void Start()
    {
        // habilitar el giroscopio 
        Input.gyro.enabled = true;
        posicionInicial = VRVision.transform.eulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        GyroscopeRotation();
        ApplyCalibration();

        if (flagGameStart)
        {
            Invoke("CalibratePosition", 3f);
            flagGameStart = false;
        }

    }// cierra el update 

    public void GyroscopeRotation()
    {
        //devolver la orientacion en el espacio del dispositivo
        VRVision.transform.rotation = Input.gyro.attitude;
        // intercambia la propiedad (clase quartenion) del giroscopio
        VRVision.transform.Rotate(0f, 0f, 180f, Space.Self);
        // gira en sentido de la parte posterior del dispositivo
        VRVision.transform.Rotate(90f, 180f, 0f, Space.World);
        // angulo para la calibracion
        posicionGiro = VRVision.transform.eulerAngles.y;

    }

    public void CalibratePosition()
    {
        //desplaza el angulo en caso de no ser 0 al iniciar la app.
        calibrarPosicion = posicionGiro - posicionInicial;
    }


    public void ApplyCalibration()
    {
        VRVision.transform.Rotate(0f, -calibrarPosicion, 0f, Space.World);

    }
   


}//cierra clase 
