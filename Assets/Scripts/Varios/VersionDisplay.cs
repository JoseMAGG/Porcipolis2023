using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionDisplay : MonoBehaviour
{
    public Text versionText;
    // Start is called before the first frame update
    void Start()
    {
        string appVersion = Application.version; // Obtiene la versión de la aplicación
        versionText.text = "Versión Actual: 2." + appVersion; // Asigna la versión al componente de texto

    }
}
