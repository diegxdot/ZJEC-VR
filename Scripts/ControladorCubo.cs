using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace RayCaster.Utils
{
    public class ControladorCubo : MonoBehaviour
    {

        //variables de control
        public static int SceneNumber;


        //variables de control

        //neesitamos una variable del tipo InteractivoVR
        //variables de interaccion
        public InteractivoVR RayCast;
        public PuntoReticula Reticula;

        //Variables de cambio de color en el cubo
        public Material ColorInicial;
        public Material ColorCambio;

        public AudioSource AudioControl;
        public AudioClip EntradaAudio;

        //flag de colision
        public bool EsteEsElObj;

        IEnumerator ToLevelOneScene()
        {
            yield return new WaitForSeconds(3);
            SceneNumber = 3;
            SceneManager.LoadScene(3);
        }



        // Start is called before the first frame update
        void Start()
        {
            //flag que se activa al colisionar con el cubo
            EsteEsElObj = true;
            gameObject.GetComponent<Renderer>().material = ColorInicial;
        }

        // Update is called once per frame
        void Update()
        {
            //validamos si el raycast esta chocando con el cubo cada vez que se mueve la camara
            if (RayCast.EstaMirando == true && EsteEsElObj == true)
            {
                gameObject.GetComponent<Renderer>().material = ColorCambio;
                AudioControl.clip = EntradaAudio;
                AudioControl.Play();
                EsteEsElObj = false;
                StartCoroutine(ToLevelOneScene());
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = ColorInicial;
                EsteEsElObj = true;
            }
        }
    }
}
