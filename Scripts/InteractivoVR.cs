using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RayCaster.Utils
{
    public class InteractivoVR : MonoBehaviour
    {
        //Variables de control de la interaccion
        //Se agrega el contexto de System para que agrege el tipo action al namespace
        //Se agrega el namespace de Raycaster.Utils ya que es el elemento de la clase physics que nos proporciona soporte de colision

        public event Action Mirando;
        public event Action NoEstaMirando;

        //flag de terminar de mirar un objeto
        protected bool terminar = false;

        //flag para tener la vista de otros objetos o continuar con el proceso
        public bool EstaMirando = false;

        //Variables para la reticula y su comportamiento
        public Image imgPunto;
        public Image imgCiruclo;
        public Animator AnimacionCirculo;

        //agregar un getter de la bandera, para consultarlo desde otro script en caso de ser necesario
        public bool SeTermino
        {
            get { return terminar; }
        }

        //funciones para detectar las entradas en el RayCaster al colocar la vista sobre un objeto
        public void Entrando()
        {
            terminar = true;//cambiar bandera, ya que esta sobre objeto

            //validamos el objeto que se esta mirando en la reticula
            if(Mirando != null)
            {
                Mirando();
            }

            EstaMirando = true;

            //desactivar el punto de la reticula para que no continue con las iteracciones
            // activar el circulo de la reticula para indicar que esta sobre el objeto con alguna animacion y/o sonido
            imgPunto.enabled = false;
            imgCiruclo.enabled = true;
            AnimacionCirculo.enabled = true;

        }

        public void Saliendo()
        {
            terminar = false;

            if (NoEstaMirando != null)
                NoEstaMirando();

            EstaMirando = false;

            imgPunto.enabled = true;
            //imgCiruclo.enabled = false;
            AnimacionCirculo.enabled = false;
        }
    }

}