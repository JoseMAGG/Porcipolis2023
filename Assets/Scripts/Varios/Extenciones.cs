using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extenciones : MonoBehaviour
{

}

public static class Extenciones_Estatica
{
    public static string AString(this bool a) => (a) ? "1" : "0";
    public static bool ABooleano(this bool a, string b) => a = (b == "1");
    public static bool EntreDos(this int b, int min, int max) => b >= min && b <= max;
}