using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntoReticula : MonoBehaviour
{

    // variables para controlar la distancia de la reticula, la camara, la escala, rotacion, entre otros
    public float DistanciaDefault;
    public bool UsoNormal;
    public Image imgPunto;
    public Transform TransformDeReticula;
    public Transform Camara;

    private Vector3 EscalarOriginal;
    private Quaternion RotacionOriginal;

    //get and set del paralelismo entre superficie y retiucla

    public bool UsarNormal
    {
        get { return UsoNormal; }
        set { UsoNormal = value; }
    }

    //get del transform del punto

    public Transform TransformdelPunto
    {
        get { return TransformDeReticula; }
    }

    private void Awake()
    {
        //almacenar la escala y rotacion inicial
        EscalarOriginal = TransformDeReticula.localScale;
        RotacionOriginal = TransformDeReticula.localRotation;
    }

    //metodos para controlar el comportamiento y configuracion de la reticula
    public void ocultarPunto()
    {
        imgPunto.enabled = false;
    }

    public void mostrarPunto()
    {
        imgPunto.enabled = true;
    }

    //metodos para complementar el raycaster y saber si ha tocado algun obj o no

    //Sobrecarga del metodo configurar pra cuando el raycaster no golpea un objeto
    public void Configurar()
    {
        // posicion del punto de la reticula con la distancia default frente a la camara
        TransformDeReticula.position = Camara.position + Camara.forward * DistanciaDefault;

        //escala basada en la original y la distancia de la camara
        TransformDeReticula.localScale = EscalarOriginal * DistanciaDefault;

        // la rotacion no cambia por lo tanto se queda con la original
        TransformDeReticula.localRotation = RotacionOriginal;
    }

    //Sobrecarga del metodo configurar pra cuando el raycaster golpea un objeto
    
    public void Configurar(RaycastHit hit)
    {
        TransformDeReticula.position = hit.point;
        TransformDeReticula.localScale = EscalarOriginal * hit.distance;

        if (UsoNormal)
        {
            TransformDeReticula.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
        }
        else
        {
            TransformDeReticula.localRotation = RotacionOriginal;
        }
    }



}
