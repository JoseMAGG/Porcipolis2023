 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource musica;

    public bool gameOver;
    public GameObject gameOverPanel;
    public Text score;
    public Text results;
    public bool isGameStarted;
    public GameObject startingText;

    public float primerMomento;
    public float ultimoMomento;
    public float momentoActual;

    public static GameManager singleton;

    void Start()
    {
        musica.Stop();
        primerMomento = Time.time;
        gameOver = false;
        Time.timeScale = 1;
        score.text = "Puntos: ";

        isGameStarted = false;

    }

    public void Awake()
    {
        singleton = this;
    }

    void Update()
    {
        if(isGameStarted) momentoActual = Time.time - primerMomento;

        score.text = "Puntos: " + (int)momentoActual;

        if (SwipeManager.singleton.tap && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(startingText);
            primerMomento = Time.time;
            musica.Play();
        }

        if (gameOver)
        {
            gameOverPanel.SetActive(true);

            results.text = score.text;
            score.text = "";
            musica.Stop();
        }
    }

    public void Replay()
    {
        Escenas.singleton.CargarEscena("MiniJuego");
    }

    public void Exit()
    {
        Escenas.singleton.CargarEscena("Juego");
    }

}
