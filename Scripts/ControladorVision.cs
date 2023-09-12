using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorVision : MonoBehaviour
{

    public float Velocidad = 50f;
    public float SensibilidadDeArrastre = 2f;

    public bool RotacionDeTeclado = true;
    public bool RotacionDeMouse = true;
    public bool RotacionDeArrastre = true;

    private float x = 0.0f;
    private float y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //uso del teclado
        //validar si se puede utilizar el teclado
        if(RotacionDeTeclado)
        {
            x += Input.GetAxis("Horizontal") * Velocidad * Time.deltaTime;
            y += Input.GetAxis("Vertical") * Velocidad * Time.deltaTime;
        }

        //uso del mouse
        if(RotacionDeMouse && Input.GetMouseButton(0))
        {
            x += Input.GetAxis("Mouse X") * Velocidad * Time.deltaTime * SensibilidadDeArrastre;
            y += Input.GetAxis("Mouse Y") * 1.5f * Velocidad * Time.deltaTime * SensibilidadDeArrastre;
        }

        //Utilizar touch
        if(RotacionDeArrastre && Input.touchCount == 1)
        {
            Touch f0 = Input.GetTouch(0);
            Vector3 f0Delta2 = new Vector3(f0.deltaPosition.x,f0.deltaPosition.y,0);

            x += Mathf.Deg2Rad * f0Delta2.x * SensibilidadDeArrastre * 10;
            y += Mathf.Deg2Rad * f0Delta2.y * SensibilidadDeArrastre * 10;
        }

        //aplicar las nuevas posiciones a las camaras
        y = anguloVista(y, -80, 80);
        Quaternion rotacion = Quaternion.Euler(y,x,0.0f);
        transform.rotation = rotacion;

    }//cierra update

    public static float anguloVista(float angulo, float min, float max)
    {
        //validar angulo
        if(angulo < -360.0f)
            angulo += 360.0f;
        if(angulo > 360.0f)
            angulo -= 360.0f;

        return Mathf.Clamp(angulo,min,max);
    }

}
