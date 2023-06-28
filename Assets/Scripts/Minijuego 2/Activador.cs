using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activador : MonoBehaviour
{
    public SpriteRenderer s;
    public bool active;
    public GameObject note;
    public Color old;


    void Start()
    {
        old = s.color;
    }

    void Awake()
    {
        s = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        StartCoroutine(Pressed());

        if (active)
        {
            Destroy(note);
            MGameManager.singleton.AddRacha();
            AddScore();
            active = false;
        }
        else
        {
            MGameManager.singleton.ResetRacha();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "WinNote")
        {
            MGameManager.singleton.Win();
        }

        if(col.gameObject.tag == "Note")
        {
            note = col.gameObject;
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }

    public void AddScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + MGameManager.singleton.GetScore());
    }

    public IEnumerator Pressed()
    {
        s.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.05f);
        s.color = old;
    }
}
