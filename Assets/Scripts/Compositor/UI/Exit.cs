using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private Escenas escenas;
    public GameObject dialogGo;
    void Start()
    {
        escenas = GetComponent<Escenas>();
    }

    public void RequestExit(string sceneName)
    {
        if (CompositorUI.instance.newChanges)
        {
            dialogGo.SetActive(true);
        }
        else
        {
            JustExit(sceneName);
        }
    }

    public void SaveAndExit(string sceneName)
    {
        GameController.instance.Save();
        JustExit(sceneName);
    }

    public void JustExit(string sceneName)
    {
        escenas.CargarEscena(sceneName);
    }
}
