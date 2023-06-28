using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MGameManager : MonoBehaviour
{
    public AudioSource music;

    public int multiplier = 2;
    public int racha = 0;

    public GameObject overText;
    public GameObject panel;

    public static MGameManager singleton;

    public GameObject winN;
    public GameObject not;
    public int numChild;

    void Awake()
    {
        singleton = this;
    }


    void Start()
    {
        //numChild = not.transform.childCount;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("RockMeter", 25);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
        ResetRacha();

    }

    public void AddRacha()
    {
        if(PlayerPrefs.GetInt("RockMeter") + 1 < 50)
            PlayerPrefs.SetInt("RockMeter", PlayerPrefs.GetInt("RockMeter") + 1);
        racha++;
        if (racha >= 24) multiplier = 4;
        else if (racha >= 16) multiplier = 3;
        else if (racha >= 8) multiplier = 2;
        else multiplier = 1;
        UpdateGUI();
        
    }

    public void ResetRacha()
    {
        PlayerPrefs.SetInt("RockMeter", PlayerPrefs.GetInt("RockMeter") - 2);
        if (PlayerPrefs.GetInt("RockMeter") < 0)
            Lose();
            racha = 0;
        multiplier = 1;
        UpdateGUI();
    }

    public void Win()
    {
        // Time.timeScale = 0;
        panel.SetActive(true);
        overText.GetComponent<Text>().text = "Has ganado! \nPuntaje: " + PlayerPrefs.GetInt("Score");
        PararNotas();
        music.Stop();
    }

    public void Lose()
    {
        // Time.timeScale = 0;
        panel.SetActive(true);
        overText.GetComponent<Text>().text = "Suerte para la próxima \nPuntaje: " + PlayerPrefs.GetInt("Score");
        PararNotas();
        music.Stop();
        Destroy(winN);
    }

    public void UpdateGUI()
    {
        PlayerPrefs.SetInt("Racha", racha);
        PlayerPrefs.SetInt("Mult", multiplier);
    }


    public int GetScore()
    {
        return 100 * multiplier;
    }

    public void Replay()
    {
        SceneManager.LoadScene("MiniJuego_2");
    }


    public void PararNotas()
    {
        numChild = not.transform.childCount;
        for (int i = 0; i < numChild; i++)
        {
            not.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

}
