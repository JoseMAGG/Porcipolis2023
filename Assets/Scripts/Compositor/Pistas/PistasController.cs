using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PistasController : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] tracksNames =new string[] { };
    public List<ButtonTrack> botonesTracks = new List<ButtonTrack>();
    public GameObject muestra;
    public int cantidad;

    #region Singlenton
    public static PistasController Instance;


    #endregion
    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < cantidad-1; i++)
        {
            Instantiate(muestra, muestra.transform.parent);
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
      
        LoadTracks();
        EnableButtonsAndSetText();
    }

   
    void LoadTracks() {

        string tracks = MorionTools.Cargar("nombrePistas");
        if (tracks == "") {
            return;
        }
        tracks = tracks.Substring(0, tracks.Length - 1);
        tracksNames = tracks.Split(',');
    }

    void EnableButtonsAndSetText() {

        for (int i = 0; i < botonesTracks.Count; i++)
        {
            if (i < tracksNames.Length)
            {
                //Set Text
                botonesTracks[i].botonTextoOAlgoAsi.text = tracksNames[i];
                //active Button
                botonesTracks[i].gameObject.SetActive(true);
                //Track exist
                botonesTracks[i].TrackExist = true;
                //set track name
                botonesTracks[i].trackName = tracksNames[i];
            }
            else
            {
                
                if (i == tracksNames.Length)
                {
                    botonesTracks[i].gameObject.SetActive(true);
                }
                else
                {
                
                    botonesTracks[i].gameObject.SetActive(false);
                }
            }                          
        }
    }

    public void ResetStatusButton() {

        for (int i = 0; i < botonesTracks.Count; i++)
        {
            if (botonesTracks[i].gameObject.activeInHierarchy) {
                botonesTracks[i].ResetStatus();
            }
        }
    }
}
