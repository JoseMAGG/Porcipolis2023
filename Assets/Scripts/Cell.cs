using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [Header("Cell Properties")]

    public int row;
    public int col;
    public string nombre;
    public InstrumentUI instrumentUI;
    public Instrument instrument;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ActivarDesactivar());
    }

    void Activar()
    {
        instrumentUI.ChangeSpriteCell(true, this.row, this.col);

        instrument.Pages[instrument.ActualPage].Grid[row, col].isActive = true;
        instrument.Pages[instrument.ActualPage].Grid[row, col].name = nombre;
    }
    void Desactivar()
    {

        instrumentUI.ChangeSpriteCell(false, this.row, this.col);

        instrument.Pages[instrument.ActualPage].Grid[row, col].isActive = false;
        instrument.Pages[instrument.ActualPage].Grid[row, col].name = nombre;

    }

    public void ActivarDesactivar()
    {
        if (instrument.Pages[instrument.ActualPage].Grid[row, col].isActive)
        {
            Desactivar();
        }
        else
        {
            Activar();
        }
        CompositorUI.instance.newChanges = true;
    }


}

