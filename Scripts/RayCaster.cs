using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RayCaster.Utils
{
    public class RayCaster : MonoBehaviour
    {
        //variables

        public event Action<RaycastHit> SeGolpeoConRayCast;

        public Transform Camara;
        public LayerMask CapasDeExlucion;
        public PuntoReticula Reticula;
        public bool MostrarDepuracionRayCast;
        public float LongitudDeDepuracionRayCast = 5f;
        public float DuracionDeDepuracionRayCast = 1f;
        public float LongitudDeRayCast = 500f;

        public InteractivoVR InteractivoActualRC;
        public InteractivoVR InteractivoUltimo;

        //getter de elemento de interaccion actual para ser utilizado desde otras clases
        public InteractivoVR InteractivoActual
        {
            get { return InteractivoActualRC; }
        }

        private void Update()
        {
            EyeRayCast();
        }

        private void EyeRayCast()
        {
            //mostrar el rayo de ser necesario
            if(MostrarDepuracionRayCast)
            {
                Debug.DrawRay(Camara.position, Camara.forward * LongitudDeDepuracionRayCast, Color.green, DuracionDeDepuracionRayCast);
            }

            //Crear el rayo de interaccion que apuente frente a la camara
            Ray RayCast = new Ray(Camara.position, Camara.forward);
            RaycastHit hit;

            //variables para revisar si esta colisionando con un objeto interactivo
            if(Physics.Raycast(RayCast, out hit, LongitudDeRayCast, ~CapasDeExlucion))
            {
                InteractivoVR Interactible = hit.collider.GetComponent<InteractivoVR>();
                InteractivoActualRC = Interactible;

                //validar el elemento interactvio
                if( Interactible && Interactible != InteractivoUltimo)
                {
                    Interactible.Entrando();
                }

                if(Interactible != InteractivoUltimo)
                {
                    DesactivarUltimoInteractivo();
                    InteractivoUltimo = Interactible;
                }

                //validar si algo fue golpeado
                if (Reticula)
                    Reticula.Configurar(hit);

                if (SeGolpeoConRayCast != null)
                    SeGolpeoConRayCast(hit);
            }
            else
            {
                //si nada fue golpeado desactiva las interacciones en el ultimo elemento golpeado
                DesactivarUltimoInteractivo();
                InteractivoActualRC = null;

                //regresar el punto de la reticula o la mira y distancia por default
                if (Reticula)
                    Reticula.Configurar();
            }
        }
        private void DesactivarUltimoInteractivo()
        {
            if (InteractivoUltimo == null)
            {
                return;
            }
            InteractivoUltimo.Saliendo();
            InteractivoUltimo = null;
        }
    }

}
