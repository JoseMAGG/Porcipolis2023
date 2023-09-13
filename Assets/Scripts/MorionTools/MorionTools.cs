using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MorionTools : MonoBehaviour
{
    public static string nombreUsuario = "anonimo";

    private void Start()
    {
        nombreUsuario = PlayerPrefs.GetString("usuario_cargado", "anonimo");
    }

    public static void Guardar(string nombre, string datos)
    {
        PlayerPrefs.SetString(nombreUsuario + "_" + nombre, datos);
        //print("Guardado -> " + nombreUsuario + "_" + nombre + "::" + datos);
    }

    public static string Cargar(string nombre) 
    {
        //print("Cargado => " + nombreUsuario + "_" + nombre + "::" + PlayerPrefs.GetString(nombreUsuario + "_" + nombre));
        string retorno = PlayerPrefs.GetString(nombreUsuario + "_" + nombre);
        return retorno;
    }
        

    public static DateTime GetTiempoActual() =>
        DateTime.Now;


    /// <summary>
    /// Para borrar los player prefbs en el proyecto
    /// </summary>
    [ContextMenu("Borrar Player Prefbs")]
    public void BorrarPlayerPrefbs()
    {
        PlayerPrefs.DeleteAll();
    }

    public static bool MouseEnUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return (results.Count > 0);
    }

}

[System.Serializable]
public class Tiempo
{
    public int años;
    public int meses;
    public int dias;
    public int horas;
    public int minutos;
    public int segundos;



    /// <summary>
    /// Tiempo que inicia con todos los datos en 0
    /// </summary>
    public Tiempo()
    {
        años     = 0;
        meses    = 0;
        dias     = 0;
        horas    = 0;
        minutos  = 0;
        segundos = 0;
    }

    /// <summary>
    /// Especificar cada uno de los datos con los que se va a iniciar el Tiempo
    /// </summary>
    /// <param name="_años"></param>
    /// <param name="_meses"></param>
    /// <param name="_dias"></param>
    /// <param name="_horas"></param>
    /// <param name="_minutos"></param>
    /// <param name="_segundos"></param>
    public Tiempo(int _años, int _meses, int _dias, int _horas, int _minutos, int _segundos)
    {
        años     = _años;
        meses    = _meses;
        dias     = _dias;
        horas    = _horas;
        minutos  = _minutos;
        segundos = _segundos;
    }

    /// <summary>
    /// Utilizar un elemento de System de DateTime tiempo para generar el Tiempo propio
    /// </summary>
    /// <param name="dateTime"></param>
    public Tiempo(DateTime dateTime)
    {
        años     = dateTime.Year;
        meses    = dateTime.Month;
        dias     = dateTime.Day;
        horas    = dateTime.Hour;
        minutos  = dateTime.Minute;
        segundos = dateTime.Second;
    }
    /// <summary>
    /// Actualiza los datos del objeto para que tengan los del tiempo actual
    /// </summary>
    public void AsignarTiempoActual()
    {
        DateTime dateTime = DateTime.Now;
        años     = dateTime.Year;
        meses    = dateTime.Month;
        dias     = dateTime.Day;
        horas    = dateTime.Hour;
        minutos  = dateTime.Minute;
        segundos = dateTime.Second;
    }

    /// <summary>
    /// Utiliza una clase tiempo serializada a JSon para llenar los datos
    /// </summary>
    /// <param name="json"></param>
    public void DesdeJSon(string json)
    {
        Tiempo t = JsonUtility.FromJson<Tiempo>(json);

        años     = t.años;
        meses    = t.meses;
        dias     = t.dias;
        horas    = t.horas;
        minutos  = t.minutos;
        segundos = t.segundos;
    }
    /// <summary>
    /// Convierte la clase tiempo en un JSon y retorna ese string
    /// </summary>
    /// <returns></returns>
    public string AJson()
    {
        Tiempo t = new Tiempo(años, meses, dias, horas, minutos, segundos);
        return JsonUtility.ToJson(t);
    }

    /// <summary>
    /// Calcula el tiempo que hay entre el dato que tiene registrado y la fecha y hora actual
    /// </summary>
    /// <returns></returns>
    public Tiempo CuantoFalta()
    {
        Tiempo tHoy = new Tiempo(DateTime.Now);
        Tiempo tEste = new Tiempo();
        tEste.DesdeJSon(AJson());
        Tiempo diferencia = Diferencia(tHoy, tEste);


        return diferencia;
    }
    /// <summary>
    /// Se especifíca un mes y el método retorna cuántos días tiene ese mes
    /// </summary>
    /// <param name="mes"></param>
    /// <returns></returns>
    public static int DiasDeMes(int mes)
    {
        if (mes == 2)
        {
            return 28;
        }
        else if (mes == 4 || mes == 6 || mes == 9 || mes == 11)
        {
            return 30;
        }
        return 31;
    }

    /// <summary>
    /// Retorna la diferencia entre dos tiempos (Final - Inicial), sí el año es negativo es porque el tiempo inicial es Mayor
    /// </summary>
    /// <param name="tInicial"></param>
    /// <param name="tFinal"></param>
    /// <returns></returns>
    public static Tiempo Diferencia(Tiempo tInicial, Tiempo tFinal)
    {
        Tiempo tHoy = tInicial;
        Tiempo tEste = new Tiempo();
        tEste.DesdeJSon(tFinal.AJson());
        Tiempo diferencia = new Tiempo();

        diferencia.segundos = tEste.segundos - tHoy.segundos;
        if (diferencia.segundos < 0)
        {
            tEste.minutos--;
            diferencia.segundos += 60;
        }
        diferencia.minutos = tEste.minutos - tHoy.minutos;
        if (diferencia.minutos < 0)
        {
            tEste.horas--;
            diferencia.minutos += 60;
        }
        diferencia.horas = tEste.horas - tHoy.horas;
        if (diferencia.horas < 0)
        {
            tEste.dias--;
            diferencia.horas += 24;
        }
        diferencia.dias = tEste.dias - tHoy.dias;
        if (diferencia.dias < 0)
        {
            int diasDelMes = DiasDeMes(tEste.meses);
            tEste.meses--;
            diferencia.dias += diasDelMes;
        }
        diferencia.meses = tEste.meses - tHoy.meses;
        if (diferencia.meses < 0)
        {
            tEste.años--;
            diferencia.meses += 12;
        }

        diferencia.años = tEste.años - tHoy.años;

        return diferencia;
    }

}