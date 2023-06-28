﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CompositorUI : MonoBehaviour
{

    public Text textactualIns;
    [Header("Timeline And Slider-----------------------")]

    public Image[] timeline;
    public GameObject timeLinePackage;
    public Color timelineColor;
    public Color initialColorTimeLine;
    [Space]
    public Slider sliderSpeed;
    [Space]
    [Header("Sprites Grid and Tabs-----------------------")]
    public Sprite spriteDarkOffCells;
    public Sprite spriteDarkOnCells;
    public Sprite spriteLightOffCells;
    public Sprite spriteLightOnCells;
    public Sprite spriteOnTab;
    public Sprite spriteOffTab;

    [HideInInspector] public List<InstrumentUI> instrumentsUI;

    public static InstrumentUI actualInstUI;
    GameController gameController;
    Compositor compositor;


    #region Aux variables

    public static int numTabs = 10;
    public static int cols = 8;
    bool showTimeLine = true; //?

    #endregion

    #region singlenton
    public static CompositorUI instance;

    private void Awake() => instance = this;

    #endregion

    public IEnumerator  Start()
    {
        textactualIns.text = "1";
        
        initialColorTimeLine = timeline[2].color; //Set default color of timeline

        gameController = GameController.instance;
        compositor = gameController.compositor;


        yield return new WaitForSeconds(0.5f);

        SetActualInstrument(gameController.instrumentProp[0].name); //Set the first element of instrument propierties as the actual instrument
    }




    public void SetActualInstrument(string name)
    {
        if (instrumentsUI.Count > 0)
        {
   
            instrumentsUI.ForEach(x => x.gameObject.SetActive(false)); //Disable All

            actualInstUI = instrumentsUI.Find(x => x.nickname.Equals(name)); //Find the real one
            actualInstUI.gameObject.SetActive(true);
         
            // actualInstUI.EnableUI();
            instrumentsUI.ForEach(x => x.EnableUI());
        }

    }



    public void SetShowTimeLine(bool condition) => showTimeLine = condition;


    public void SetSpritesColorGrid()
    {

        foreach (var insUI in instrumentsUI)
        {

            for (int i = 0; i < insUI.rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    Color colorCell = insUI.grid[i].row_n[j].GetComponent<Image>().color;

                    InstrumentUI.ColorSpritesGridTabs color = insUI.colorSprites;

                    colorCell = (i % 2 == 0) ? color.spriteDarkOffCells : color.spriteLightOffCells;

                }
            }
        }


    }

    public float GetSpeedSlider() => (sliderSpeed.value - 0.5f) * 2;

    public void SetTimeLineColor(int i)
    {

        if (showTimeLine)
        {
            timeline[i].color = timelineColor;
            return;
        }
        ResetTimeLineColor(i);


    }



    public void ResetTimeLineColor(int i) => timeline[i].color = initialColorTimeLine;



    public void NextIntrument()
    {
        int indexActual = instrumentsUI.FindIndex(x => x.nickname.Equals(actualInstUI.nickname));
    
        int indexNext = (indexActual + 1) % (instrumentsUI.Count);
   

        textactualIns.text =(indexNext + 1).ToString() ;


        actualInstUI = instrumentsUI[indexNext];
        DisableEnable();
    }

    public void DisableEnable()
    { 
        instrumentsUI.ForEach(x => x.gameObject.SetActive(false)); 
        actualInstUI.gameObject.SetActive(true);
    }


    public void PreviusInstrument()
    {
        int indexActual = instrumentsUI.FindIndex(x => x.nickname.Equals(actualInstUI.nickname));
        int indexNext = (indexActual - 1);
        if (indexNext < 0)
        {
            indexNext = instrumentsUI.Count - 1;
        }
        actualInstUI = instrumentsUI[indexNext];
        DisableEnable();
    }









}
