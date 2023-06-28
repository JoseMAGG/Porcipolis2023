using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamagotchiTiempoExtraTools 
{
    public static bool VerificarTiempoLimite ( Tiempo restante )
    {
        if ( ( restante.años <= 0 &&
            restante.meses <= 0 &&
            restante.dias <= 0 &&
            restante.horas <= 0 &&
            restante.minutos <= 0 &&
            restante.segundos <= 0 ) ||
            restante.años < 0 ||
            restante.meses < 0 ||
            restante.dias < 0 ||
            restante.horas < 0 ||
            restante.minutos < 0 ||
            restante.segundos < 0 )
        {
            return true;

        }
        return false;
    }

    public static int TiempoASegundos ( Tiempo tiempo )
    {
        int total = 0;
        total += AñosASegundos ( tiempo.años );
        total += MesesASegundos ( tiempo.meses );
        total += DiasASegundos ( tiempo.dias );
        total += HorasASegundos ( tiempo.horas );
        total += MinutosASegundos ( tiempo.minutos );
        total += tiempo.segundos;
        return total;

    }

    public static int AñosADias ( int año ) => MesesADias ( 12 * año );
    public static int MesesADias ( int meses ) => ( Tiempo.DiasDeMes ( meses ) * meses );


    public static int AñosASegundos ( int año ) => MesesASegundos ( 12 * año );

    public static int MesesASegundos ( int mes ) => DiasASegundos ( Tiempo.DiasDeMes ( mes ) * mes );// No lo hace del todo bien

    public static int DiasASegundos ( int dia ) => HorasASegundos ( dia * 24 );

    public static int HorasASegundos ( int hora ) => MinutosASegundos ( 60 * hora );

    public static int MinutosASegundos ( int minuto ) => 60 * minuto;

    public static void ConfigurarFecha ( Tiempo tiempo )
    {
        tiempo.años = tiempo.dias / 365;
        tiempo.meses = ( tiempo.dias % 365 ) / 30;
        tiempo.dias %= 30;
    }
}
