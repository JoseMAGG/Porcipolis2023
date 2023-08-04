using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{


    // Array withall notes
    #region Main Variables
    [Header("Audio Propierties")]
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 2f)]
    public float pitch;
    [Range(.1f, 2f)]
    public float pitchBemol;
    [Space]
    [HideInInspector] public static int actualBlock;
    [HideInInspector] public int cols;

    bool play;



    #endregion
    [Space]
    public InstrumentsMusicData[] instrumentsMusicData;
    CompositorUI compositorUI;
    GameController gameController;
    List<Instrument> instruments = new List<Instrument>();




    private void Awake()
    {
        #region Add Component AudioSource
        foreach (var instrumentM in instrumentsMusicData)
        {
            foreach (Note n in instrumentM.notes)
            {
                n.name = instrumentM.name + "_" + n.name;
                n.source = gameObject.AddComponent<AudioSource>();
                n.source.clip = n.clip;
                n.source.volume = volume;

                if (n.name.EndsWith("#"))
                {
                    n.source.pitch = pitchBemol;
                }
                else
                    n.source.pitch = pitch;
            }
        }
        #endregion
    }


    private void Start()
    {
        play = false;

        #region Aux Variables Initialization
        cols = 0;

        #endregion
        compositorUI = CompositorUI.instance;
        gameController = GameController.instance;
        instruments = gameController.compositor.Instruments;
        actualBlock = 1;
    }


    public int GetActualBlock()
    {
        return actualBlock;
    }


    IEnumerator PlayActiveNotes()
    {
        bool morePagesInOtherInst = false;
        int pageCount = 1;
        CompositorUI.actualInstUI.instrument.ActualPage = pageCount - 1;
        CompositorUI.actualInstUI.ShowActualPage(pageCount.ToString());

        while (play)
        {
            Instrument actualInst = CompositorUI.actualInstUI.instrument;
            bool isBiggest = actualInst.PagesNum == compositorUI.compositor.BiggerInstrument;
            bool isLastPage = actualInst.PagesNum == actualInst.ActualPage + 1;

            morePagesInOtherInst = isLastPage && !isBiggest;
            if (!morePagesInOtherInst || actualInst.PagesNum >= pageCount)
            {
                compositorUI.SetTimeLineColor(cols); //Change color timeline

                actualInst.ActualPage = pageCount - 1;
                CompositorUI.actualInstUI.ShowActualPage(pageCount.ToString());
            }
            else
            {
                actualInst.ActualPage = actualInst.PagesNum - 1;
                CompositorUI.actualInstUI.ShowActualPage(actualInst.PagesNum.ToString());
            }

            SearchActiveNotes();

            yield return new WaitForSecondsRealtime(compositorUI.GetSpeedSlider());

            compositorUI.ResetTimeLineColor(cols); //Change color timeline
            if (cols == 7)
            {
                if (pageCount == compositorUI.compositor.BiggerInstrument)
                {
                    morePagesInOtherInst = false;
                    pageCount = 1;
                }
                else
                {
                    pageCount++;
                }
            }

            SetPropierties();
        }
    }

    void Play(string name)
    {

        foreach (var instrumentData in instrumentsMusicData)
        {
            Note s = Array.Find(instrumentData.notes, note => note.name == name);

            if (!(s is null))
            {
                s.source.Play();
                return;

            }
        }

    }

    void SearchActiveNotes()
    {

        Page actualPage;

        foreach (var instrument in instruments)
        {
            if (instrument.GetPage(actualBlock) == null)
            {
                continue;
            }
            actualPage = instrument.GetPage(actualBlock);

            for (int i = 0; i < instrument.numNotes; i++) //Throw each row
            {
                if (actualPage.Grid[i, cols].isActive)
                {
                    Play(instrument.name + "_" + actualPage.Grid[i, cols].name); //Reproduce note
                }
            }
        }

    }

    void SetPropierties()
    {

        cols += 1;
        if ((cols % CompositorUI.cols) == 0) //restart propierties
        {

            cols = 0;
            if (actualBlock < (gameController.compositor.BiggerInstrument))
            {
                actualBlock++;
            }
            else
            {
                actualBlock = 1;
            }
        }

    }

    public void Play()
    {
        if (!play)
        {
            play = true;
            StartCoroutine(PlayActiveNotes());
        }
    }

    public void Pause()
    {
        play = false;
    }
    public void Stop()
    {
        compositorUI.ResetTimeLineColor(cols);
        cols = 0;
        play = false;
        StopAllCoroutines();
    }

    #region Class Inst and Note
    [System.Serializable]
    public class InstrumentsMusicData
    {
        public string name;
        public Note[] notes;
    }
    [System.Serializable]
    public class Note
    {

        public string name;

        public AudioClip clip;

        [HideInInspector]
        public AudioSource source;
    }
    #endregion



}
