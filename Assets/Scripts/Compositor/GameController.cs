using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class GameController : MonoBehaviour
{

    public List<InstrumentPropierties> instrumentProp = new List<InstrumentPropierties>();

    [HideInInspector] public Compositor compositor;
    CompositorUI compositorUI;
    public static int numInstruments;

    public static GameController instance;

    #region Awake,Start...
    private void Awake()
    {

        instance = this;
        Load();
    }
 
    void Start()
    {
        compositorUI = CompositorUI.instance;
        numInstruments = compositorUI.instrumentsUI.Count;

        
    }

    private void Update()
    {
       // Debug.Log( 1+ instrumentProp.FindIndex(x=>x.name==CompositorUI.actualInstUI.nickname));
    }
    #endregion

    #region Save And Load
    public void Save()
    {
        string data = JsonConvert.SerializeObject(compositor);
        MorionTools.Guardar ( Compositor.trackName ,data );
     

        string nombrePistas = MorionTools.Cargar("nombrePistas");
        if (nombrePistas != null)
        {
            if (!nombrePistas.Contains(Compositor.trackName))
            {
                nombrePistas += Compositor.trackName + ",";

                MorionTools.Guardar("nombrePistas", nombrePistas);
            }
        }

        //GuardarNombreNumNotes();
    }

    public void GuardarNombreNumNotes()
    {
        string result = "";
        foreach (var instrument in compositor.Instruments)
        {
            
            result += instrument.name + "," + instrument.numNotes + "$";
            
        }

        result = result.Substring(0, result.Length - 1);

        Debug.Log(result);
        MorionTools.Guardar(Compositor.trackName + "_instrumentNames",result);
    }

    public void Load()
    {
  

       string loadedData = MorionTools.Cargar(Compositor.trackName);
     //  string instrumentNames= MorionTools.Cargar(Compositor.trackName + "_instrumentNames");
        if (loadedData.Equals("")) // New Compositor
        {
            compositor = new Compositor(instrumentProp.Count);  
            return;
        }

        compositor = JsonConvert.DeserializeObject<Compositor>(loadedData);

        //compositor.DesdeString(loadedData,instrumentNames); //Load compositor data
    }

    #endregion

    [System.Serializable]
    public class InstrumentPropierties
    {
        public string name;
        public int numNotes;

        public InstrumentPropierties(string name, int numNotes)
        {
            this.name = name;
            this.numNotes = numNotes;

        }
    }

}
