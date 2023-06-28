using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Compositor 
{
    public List<Instrument> Instruments = new List<Instrument>();
    public static string trackName="";
    public int ActualInstrument;
    public int BiggerInstrument;

 


    #region Constructors
    public Compositor() { }
    public Compositor(int numInstruments)
    {

        CreateInstruments(numInstruments);

        ActualInstrument = 0;
        BiggerInstrument = 1;
    }

    public Compositor(string str,string instInfo)
    {
        this.DesdeString(str,instInfo);
       
    }
    #endregion


    #region SaveData

    public string HaciaString()
    {
        string result = "";

        foreach (Instrument instrumento in Instruments)
        {
            if (!result.Equals(""))
            {
                result += "|";
            }

            result += instrumento.HaciaString();

        }

        return result;

    }

    #endregion

    #region Load Data
    public void DesdeString(string str,string instrumentNames)
    {
        string[] namesIns;
        string[] arreglo;
        arreglo = str.Split('|');
        namesIns = instrumentNames.Split('$');
        Instruments = new List<Instrument>();
        for (int i = 0; i < arreglo.Length; i++)
        {

            Instruments.Add(new Instrument(arreglo[i],namesIns[i]));

        }

        ActualInstrument = 0;
        UpdateBiggerInstrument();


    }

    #endregion

    #region Class Methods
   public  void CreateInstruments(int numInstruments)
    {
        int cont = 0;
        while (cont < numInstruments) { 
            Instruments.Add(new Instrument(1,GameController.instance.instrumentProp[cont].name, GameController.instance.instrumentProp[cont].numNotes));//Configurate constructor
            cont++;
        }
    }

   public  void UpdateBiggerInstrument()
    {

        foreach (var instrument in Instruments)
        {
            if (instrument.PagesNum > BiggerInstrument)
            {
                BiggerInstrument = instrument.PagesNum;
            }

        }

    }



    #endregion



}
