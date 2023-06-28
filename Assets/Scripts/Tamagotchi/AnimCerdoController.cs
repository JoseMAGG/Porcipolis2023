using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
[Serializable]
public class AnimCerdoController 
{
     public Animator animator;

    private void ActivarDesactivarParametro ( string param, bool band ) => animator.SetBool ( param , band );


    public void Dormido () => ActivarDesactivarParametro ( "despierto" , false );
    public void Despierto () => ActivarDesactivarParametro ( "despierto" , true );

    public void Activo () => ActivarDesactivarParametro ( "activo" , true );
    public void Inactivo () => ActivarDesactivarParametro ( "activo" , false );

    public void Triste () => ActivarDesactivarParametro ( "triste" , true );
    public void Alegre () => ActivarDesactivarParametro ( "triste" , false );

    public void Muerto () => ActivarDesactivarParametro ( "muerto" , true );



    public IEnumerator Accion (string acc)
    {
        ActivarDesactivarParametro (acc, true );
        yield return new WaitForSeconds ( 8f );
        ActivarDesactivarParametro ( acc , false );
    }


    #region no me borres, quizas sea util en un futuro

    /*
    public void Bañar () => ActivarDesactivarParametro ( "bañar" , true );
    public void FinBañar () => ActivarDesactivarParametro ( "bañar" , false );

    public void Comer () => ActivarDesactivarParametro( "comer" , true );
    public void FinComer () => ActivarDesactivarParametro( "comer" , false );

    public void Embarrar () => ActivarDesactivarParametro ( "embarrar" , true );
    public void FinEmbarrar () => ActivarDesactivarParametro ( "embarrar" , false );

    public void Consentir () => ActivarDesactivarParametro ( "consentir" , true );
    public void FinConsentir () => ActivarDesactivarParametro ( "consentir" , false );

    public void Beber () => ActivarDesactivarParametro ( "beber" , true );
    public void FinBeber () => ActivarDesactivarParametro ( "beber" , false );
    */
    #endregion
}
