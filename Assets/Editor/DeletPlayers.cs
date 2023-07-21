using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeletPlayers : Editor
{
    [MenuItem ("Tools/Borrar Player Prefs")]
    static void Borrar(){
        PlayerPrefs.DeleteAll();
    }
}
