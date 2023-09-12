using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//agregar los componentes obligatorios
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]

public class ControladorPlayer : MonoBehaviour
{

    //detectar si el objeto esta tocando el terreno para que se pueda mover en la escena

    private CharacterController ControladorDePersonaje;
    private Vector3 MovimientoEnDireccion = Vector3.zero;
    private Vector2 Entrada;
    private CollisionFlags flagsCollision;

    public float FuerzaSobreSuelo;
    public float MultiplicadorGravedad;


    // Start is called before the first frame update
    void Start()
    {
        ControladorDePersonaje = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        //movimiento al frende del plano

        Vector3 Movimiento = transform.forward * Entrada.y + transform.right * Entrada.x;

        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, ControladorDePersonaje.radius,
            Vector3.down, out hitInfo, ControladorDePersonaje.height, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        //validar si el controlador de personaje esta tocando el suelo si lo toca debe avanzar
        if(ControladorDePersonaje.isGrounded)
        {
            MovimientoEnDireccion.y = FuerzaSobreSuelo;
        }
        else
        {
            MovimientoEnDireccion = Physics.gravity * MultiplicadorGravedad * Time.fixedDeltaTime;
        }

        flagsCollision = ControladorDePersonaje.Move(MovimientoEnDireccion * Time.fixedDeltaTime);
    }
}
