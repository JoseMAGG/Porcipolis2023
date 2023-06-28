using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    public float velAvanzada;
    public float maxVelocidad;
    public float velToRun;
    public float velCambioCarril;
    public float fuerzaSalto;
    public float masGravedad;
    public Transform cerdo;
    public Rigidbody cerdoRb;

    public Vector3 movimiento;
    public float destino;
    public bool onGround;

    public int carril; // 0:izquierda 1:middle 2:right
    public float distancia; // distancia entre dos carriles

    public Animator animator;

    public void Start()
    {
        cerdoRb = this.GetComponent<Rigidbody>();
        carril = 1;
        movimiento = Vector3.right;
        animator.speed = 1;


    }

    public void Update()
    {
        if (!GameManager.singleton.isGameStarted)
        {
            animator.SetBool("isGameStarted", false);
            return;
        }

        if (onGround) animator.SetBool("isGameStarted", true);


        // Incrementar Velocidad
        if (velAvanzada < maxVelocidad)
        {
            velAvanzada += 0.2f * Time.deltaTime;
            animator.speed += 0.01f * Time.deltaTime;
        }

        if (velAvanzada > velToRun) animator.SetBool("faster", true);        


        if (SwipeManager.singleton.swipeDer)
        {
            carril++;
            if (carril == 3) carril = 2;
            movimiento = Vector3.right;
        }

        if (SwipeManager.singleton.swipeIzq)
        {
            carril--;
            if (carril == -1) carril = 0;
            movimiento = Vector3.left;
        }

        if (carril == 0) destino = -distancia;
        else if (carril == 1) destino = 0;
        else if (carril == 2) destino = distancia;

        if (SwipeManager.singleton.swipeArr && onGround)
        {
            Saltar();
        }

    }

    public void FixedUpdate()
    {
        if (!GameManager.singleton.isGameStarted)
        {
            animator.SetBool("isGameStarted", false);
            return;
        }

        cerdoRb.AddForce(Vector3.down * masGravedad);

        //controlCerdo.Move(direccion * Time.deltaTime);
        //cerdo.position += direccion * Time.fixedDeltaTime;

        //cerdoRb.MovePosition(transform.position + transform.forward * velAvanzada * Time.fixedDeltaTime);
        cerdoRb.velocity = new Vector3(cerdoRb.velocity.x, cerdoRb.velocity.y, velAvanzada);

        // cerdoRb.MovePosition(new Vector3(eso, 0, 0)* velCambioCarril * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - destino) >= 0.5)
        {
            cerdoRb.MovePosition(transform.position + movimiento * velCambioCarril * Time.fixedDeltaTime);
        }


    }

    public void Saltar()
    {
        cerdoRb.AddForce(new Vector3(0f, fuerzaSalto * 80, 0f));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstaculo")
        {
            print("Final no va más");
            GameManager.singleton.gameOver = true;
            GameManager.singleton.isGameStarted = false;

        }



    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            onGround = true;
            animator.SetBool("isOnGround", true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            onGround = false;
            animator.SetBool("isOnGround", false);
            animator.SetBool("isGameStarted", false);
        }
    }
}
