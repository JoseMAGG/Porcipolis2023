using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonTrack : MonoBehaviour
{

    public GameObject mainButton;
    public GameObject createTrack;
    public GameObject playMenuTrack;
    public PistasController pistasController;
    public InputField trackInput;
    private bool trackExist;
    public string trackName;
    public Text botonTextoOAlgoAsi;

    public bool TrackExist { get => trackExist; set => trackExist = value; }

    private void Awake()
    {
        trackExist = false;
    }

    private void Start()
    {
        PistasController.Instance.botonesTracks.Add(this);
        pistasController = PistasController.Instance;
        ResetStatus();
        
    }

    public void OnClickMainButton() {
        pistasController.ResetStatusButton();
        mainButton.SetActive(false);

        if (trackExist)
        {
            playMenuTrack.SetActive(true);
        }
        else {
            createTrack.SetActive(true);
        }


        

    }

    public void Play() {
        if (!trackInput.IsActive()) {
            Compositor.trackName = trackName;
            SceneManager.LoadScene("Compositor");
        }
            
        else {
            if (trackInput.text.Length > 0) {

                Compositor.trackName = trackInput.text;
            SceneManager.LoadScene("Compositor");
            }
            
        }
    }
    public void Delete() {
        //MorionTools.Destroy(MorionTools.Cargar(trackName));
        //Debug.Log("Track has been deleted");
        this.gameObject.SetActive(false);
    }

    public void ResetStatus() {
        createTrack.SetActive(false);
        playMenuTrack.SetActive(false);
        mainButton.SetActive(true);


    }



}
