using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private Escenas escenas;
    public GameObject saveWarningGo;
    public GameObject savedDialogGo;
    private bool showingSaved = false;
    void Start()
    {
        escenas = GetComponent<Escenas>();
    }

    public void RequestExit(string sceneName)
    {
        if (CompositorUI.instance.newChanges)
        {
            saveWarningGo.SetActive(true);
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

    public void ShowSavedDialog()
    {
        if (!showingSaved) StartCoroutine(SavedDialog());
    }

    private IEnumerator SavedDialog()
    {
        showingSaved = true;
        savedDialogGo.SetActive(true);
        yield return new WaitForSeconds(2);
        savedDialogGo.SetActive(false);
        showingSaved = false;
    }
}
