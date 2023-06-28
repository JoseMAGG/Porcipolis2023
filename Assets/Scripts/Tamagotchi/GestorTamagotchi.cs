using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GestorTamagotchi : MonoBehaviour
{
    public Animator animatorCerdo;
    public AnimCerdoController animCerdo;
    public Estado estadosActuales;
    #region Objects Instance
    [HideInInspector]
    public  GestorTamagotchi gestorTamagotchi;

    #region Sistemas
    public Alimentacion alimentacion;
    public Salud salud;
    public Animo animo;
    public Edad edad;
    public Energia energia;
    public CicloSueño cicloSueño;
    #endregion


    #endregion

  
    [HideInInspector]
    public string id;
    private bool nuevoCerdito = false;

    public bool resetearTama = false;

    bool deathProcess = false;
    #region Awake,Start,Update
    void Awake ()
    {
     
       if(resetearTama)
        ResetearTamagotchi (); //Para resetear el tamagotchi del cerdo, borrame en caso de ya hacer pruebas practicas.
    }

    private IEnumerator Start ()
    {
        Cargar ();




        if ( nuevoCerdito )
        {
            gestorTamagotchi = this.GetComponent<GestorTamagotchi> ();
            TamagotchiManager.AñadirGestor ( gestorTamagotchi );
            estadosActuales.id = gestorTamagotchi.id;
            NuevoCerdito ();


        }
        else
            TamagotchiManager.AñadirGestor ( gestorTamagotchi );

        yield return new WaitForSeconds ( 0.1f );

        StartCoroutine ( Guardar () );
    }



    void Update ()
    {
        if (animatorCerdo == null)
        {
            animatorCerdo = GetComponentInChildren<Animator>();
        }
        if (animCerdo.animator != animatorCerdo)
        {
            animCerdo.animator = animatorCerdo;
        }
        if ( estadosActuales.VerificarEstado ( estados.vivo.ToString () ) )
        {
            energia.Actualizar ();
            cicloSueño.Actualizar ();
            edad.ActualizarTiempoDeVida ();
            alimentacion.Actualizar ();
            salud.Actualizar ();
            animo.Actualizar ();

        }
        else if(!deathProcess)
        {
            SistemasNivelACero ();
            animCerdo.Muerto();
            TamagotchiEvent.instance.CerdoMuerto();
            this.GetComponent<MovimientoHexagonal>().enabled = false;
            deathProcess = true;
        }

        CheckEstadoCritico();

    }

    public void CheckEstadoCritico()
    {
        bool CerdoMuerto = estadosActuales.VerificarEstado("muerto");
   
        bool CerdoCritico = //estadosActuales.VerificarEstado("hambriento") && //Alimentacion
                            estadosActuales.VerificarEstado("deshidratado") && //Alimentacion
                            estadosActuales.VerificarEstado("triste") && //Animo
                            estadosActuales.VerificarEstado("enfermo");  //Salud
                        
        if (CerdoMuerto)
        {
            TamagotchiEvent.instance.ChangeClip("muerto");
            TamagotchiEvent.instance.CerdoMuerto();
        }
        else if (CerdoCritico)
        {
            TamagotchiEvent.instance.ChangeClip("critico");
        }
        else 
        {
            TamagotchiEvent.instance.ChangeClip("principal");

        }
    }


    #endregion
    public void SistemasNivelACero () {
        salud.nivel = 0;
        animo.nivel = 0;
    }


    void NuevoCerdito ()
    {
        InicializarSistemas ();
        Accion.EjecutarAccion ( Accion.accion.nacer , id );
        nuevoCerdito = false;
    }


    public void InicializarSistemas ()
    {
        alimentacion.Inicializar ( id );
        edad.Inicializar ( id );
        cicloSueño.Inicializar ( id );
        salud.Inicializar ( id );
        animo.Inicializar ( id );
        energia.Inicializar ( id );

    }

    public void EjecutarAccion ( Accion.accion accion )
    {
        if ( Accion.ValidarAccion ( accion , id ) )
        {
            Accion.EjecutarAccion ( accion , id );
            return;
        }
        Debug.Log ( "No puedes ejecutar está accion " + accion );
    }





    public enum estados
    {
        vivo,
        muerto,



    }


    #region Save system 
    public IEnumerator Guardar ()
    {
        yield return new WaitForSeconds ( 15 );       

        MorionTools.Guardar ( "tamagotchi_" + this.gameObject.name , JsonUtility.ToJson ( this ) );
   
        Guardar ();
    }
    #endregion

    #region Load system
    public void Cargar ()
    {


        string tamagotchiString = MorionTools.Cargar ( $"tamagotchi_{this.gameObject.name}");
        if ( tamagotchiString.Equals ( "" ) )
        {
            gestorTamagotchi = this;
            nuevoCerdito = true;
            return;
        }
        JsonUtility.FromJsonOverwrite ( MorionTools.Cargar ( $"tamagotchi_{this.gameObject.name}" ) , this );
        gestorTamagotchi = this;
        // Debug.Log ( "Información tamagotchi cargada" );
    }

    public void AsignarTipoComida ( string v )
    {
        if ( v.Equals ( "1" ) )
        {
            alimentacion.hambre.comidaSeleccionada = Hambre.Comida.comida1;
        }

        else if ( v.Equals ( "2" ) )
        {
            alimentacion.hambre.comidaSeleccionada = Hambre.Comida.comida2;
        }
        else if ( v.Equals ( "3" ) )
        {
            alimentacion.hambre.comidaSeleccionada = Hambre.Comida.comida3;
        }


    }

    public void ResetearTamagotchi ()
    {
        MorionTools.Guardar ( $"tamagotchi_{this.gameObject.name}" , "" );

    }
    #endregion



    private void OnMouseDown ()
    {
        TamagotchiEvent.instance.AsignarCerdo ( this.gameObject );
    }


    /*
     private void OnApplicationQuit ()
     {
         MorionTools.Guardar ( "tamagotchi" , JsonUtility.ToJson ( Instance ) );
         Debug.Log ( " se ha guardado correctamente" );
     }
     */
}