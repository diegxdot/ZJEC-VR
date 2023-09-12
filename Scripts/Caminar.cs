using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caminar : MonoBehaviour
{
    public GameObject VisionVR;
    public const int anguloRecto = 90;
    public bool flagCaminando = false;
    public float Velocidad;
    public bool CaminarAlPulsar;
    public bool CaminarAlMirar;
    public double anguloDeUmbral;

    public bool congelarPosicionY;
    public float CompensarY;

    // Update is called once per frame
    void Update()
    {
        if(CaminarAlMirar && CaminarAlPulsar && !flagCaminando && VisionVR.transform.eulerAngles.x >= anguloDeUmbral
             && VisionVR.transform.eulerAngles.x <= anguloRecto)
        {
            flagCaminando = true;
        }
        else if(CaminarAlMirar && !CaminarAlPulsar && flagCaminando && (VisionVR.transform.eulerAngles.x <= anguloDeUmbral 
            || VisionVR.transform.eulerAngles.x >= anguloRecto))
        {
            flagCaminando = false;
        }

        if(flagCaminando)
        {
            //metodo de caminar
            walk();
        }

        if(congelarPosicionY)
        {
            transform.position = new Vector3(transform.position.x, CompensarY, transform.position.z);
        }

    }//cierra update

    public void walk()
    {
        Vector3 Direccion = new Vector3(VisionVR.transform.forward.x, 0, VisionVR.transform.forward.z).normalized * Velocidad * Time.deltaTime;
        Vector3 DireccionY = new Vector3(0, transform.rotation.eulerAngles.y, 0);
        Quaternion Rotacion = Quaternion.Euler(DireccionY);

        transform.Translate(Rotacion * Direccion);

    }
}
