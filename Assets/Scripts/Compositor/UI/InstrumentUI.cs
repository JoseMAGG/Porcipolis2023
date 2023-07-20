using System.Collections;
using System.Collections.Generic;
using Scripts.Compositor.UI;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class InstrumentUI : MonoBehaviour
{
    public string nickname;
    public int rows;

    [Space]
    [Header("Main grid")]
    //[HideInInspector] 
    public List<Rows> grid;
    [HideInInspector] public List<GameObject> tabs;
    [HideInInspector] public List<Text> tabsText;
    [Space]
    [Header("Buttons")]
    //public GameObject addButton;
    //public GameObject removeButton;
    [Space]
    [Header("Structure Obj")]
    public GameObject gridObj;
    public GameObject tabsObj;
    [Space]
    public ColorSpritesGridTabs colorSprites;
    [System.Serializable]
    public class Rows // It just to show 2d array on Inspector
    {
        public List<GameObject> row_n;

        public Rows()
        {
            row_n = new List<GameObject>();
        }
    }

 
     bool firstTime = true;


    GameController gameController;
    Compositor compositor;
    CompositorUI compositorUI;
    [HideInInspector] public Instrument instrument;

    private void Awake()
    {
        SetGrid();
        SetTabsAndTabsText();

      
    }

    public void Start()
    {
       
  
        
        compositorUI = CompositorUI.instance;
        gameController = GameController.instance;
        compositorUI.instrumentsUI.Add(this);

        compositor = GameController.instance.compositor;
        instrument = compositor.Instruments.Find(x => x.name.Equals(nickname));

        SetParentToCells();
   




    }

    public void SetInitialPropierties() 
    {

        compositor = GameController.instance.compositor;
        instrument = compositor.Instruments.Find(x => x.name.Equals(nickname));
        if (instrument is null)
        {
            Debug.Log("It is null");

        }
        SetParentToCells();


    }

    public void SetParentToCells()
    {
        foreach (var row in grid)
        {
            foreach (var cell in row.row_n)
            {
                cell.gameObject.GetComponent<Cell>().instrumentUI = this;
                cell.gameObject.GetComponent<Cell>().instrument= instrument;
            }
        }
    }



    public void SetGrid()
    {
        grid = new List<Rows>();
        for (int i = 0; i < rows; i++)
        {
            Rows emptyRows = new Rows();
           
            for (int j = 0; j < CompositorUI.cols; j++)
            {
               
                emptyRows.row_n.Add(gridObj.transform.GetChild(i).GetChild(j).gameObject);
            }
            grid.Add(emptyRows);
        }
    }

    void InitialiceGrid()
    {
    }

    public void SetTabsAndTabsText()
    {
        for (int i = 0; i < CompositorUI.numTabs; i++)
        {
            GameObject tabi= tabsObj.transform.GetChild(i).gameObject;

            tabs.Add(tabi);
            tabsText.Add (tabi.transform.GetChild(0).GetComponent<Text>());
        }
    }


    #region Update UI

    public void EnableUI()
    {
      
        //removeButton.SetActive(instrument.PagesNum == 1 ? false : true); //Disable/Active remove Button

        EnableTab();
    }

    public void EnableTab( )
    {
        bool enableFirstPage = false;

        for (int i = 0; i < instrument.Pages.Count; i++)
        {
            if (instrument.Pages[i].IsActive)
            {

                tabs[i].SetActive(true);
                if (!enableFirstPage)
                {
                    firstTime = true;
                    ShowActualPage((i + 1).ToString());
                    enableFirstPage = true;
                }
            }
            else
            {
                tabs[i].SetActive(false);
            }
        }
    }

    public void ShowActualPage(Text index)
    {

        if (instrument.Pages[instrument.ActualPage].Value.Equals(int.Parse(index.text))) return;

        string oldTab = instrument.Pages[instrument.ActualPage].Value.ToString();
 
        ChangeImageTab(SearchIndexText(index.text), SearchIndexText(oldTab));

        //Update actual page
        if (index != null) instrument.ActualPage = instrument.Pages.FindIndex(page => page.Value == int.Parse(index.text));           
    
        
        LoadGrid();

        //compositorUI.SetShowTimeLine(instrument.ActualPage == (AudioManager.actualBlock - 1))
    }


    public void ShowActualPage(string ind)
    {
        if (firstTime)
        {
            ChangeImageTab(SearchIndexText(ind), -1);
            firstTime = false;
        }
        else
        {
            string oldTab = instrument.Pages[instrument.PreviusTab].Value.ToString();
            ChangeImageTab(SearchIndexText(ind), SearchIndexText(oldTab));

        }

        LoadGrid();
    }


    public int GetActualPage()
    {
        return instrument.ActualPage;
    }

    public void LoadGrid()
    {
        //Load notes into grid
        for (int i = 0; i < instrument.numNotes; i++) //Rows
        {
            for (int j = 0; j < CompositorUI.cols; j++) //Cols
            {
               ChangeSpriteCell(instrument.Pages[instrument.ActualPage].Grid[i, j].isActive, i, j);

            }
        }
    }

    void ChangeImageTab(int newTab, int oldTab)
    {
        if (newTab != -1)
        {
            Image tab = tabs[newTab].GetComponent<Image>();
            tab.sprite = compositorUI.spriteOnTab;
            tab.color = colorSprites.spriteOnTab;

            if (oldTab == -1)
            {
                return;
            }

            tab = tabs[oldTab].GetComponent<Image>();
            tab.sprite = compositorUI.spriteOffTab;
            tab.color = colorSprites.spriteOffTab;
        }

    }



    int SearchIndexText(string ind)
    {
        for (int i = 0; i < tabsText.Count; i++)
        {
            if (tabsText[i].text.Equals(ind) && tabs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }

    public void ChangeSpriteCell(bool signal, int row, int col)
    {
        Image imageCell = grid[row].row_n[col].GetComponent<Image>();

        InstrumentUI.ColorSpritesGridTabs colorSprite = colorSprites;

        if (signal)
        {
            if (row % 2 == 0)
            {
                imageCell.sprite = compositorUI.spriteDarkOnCells;
                imageCell.color = colorSprite.spriteDarkOnCells;
            }
            else
            {
                imageCell.sprite = compositorUI.spriteLightOnCells;
                imageCell.color = colorSprite.spriteLightOnCells;
            }

        }
        else
        {
            if (row % 2 == 0)
            {
                imageCell.sprite = compositorUI.spriteDarkOffCells;
                imageCell.color = colorSprite.spriteDarkOffCells;
            }
            else
            {
                imageCell.sprite = compositorUI.spriteLightOffCells;
                imageCell.color = colorSprite.spriteLightOffCells;
            }
        }

    }

    #endregion


    #region Add and Remove tabs
    public void AddTab()
    {


        //int pagesNum = instrument.PagesNum;

        //#region Special Cases
        //if (pagesNum == 1)
        //{
        //    removeButton.SetActive(true);
        //}

        //if (pagesNum == 9)
        //{
        //    addButton.SetActive(false);
        //}
        //#endregion 

        tabs.Find (x => !x.activeInHierarchy).SetActive(true); // find the first inactive tab and enable it



       instrument.AddPages();
       compositor.UpdateBiggerInstrument();

        ReAssignTabsValue();
        ChangeImageTab(-1, compositor.Instruments[compositor.ActualInstrument].Pages[compositor.Instruments[compositor.ActualInstrument].PreviusTab].Value);
        ShowActualPage(compositor.Instruments[compositor.ActualInstrument].Pages[compositor.Instruments[compositor.ActualInstrument].ActualPage].Value.ToString());

      // compositorUI.SetShowTimeLine(instrument.ActualPage == (AudioManager.actualBlock - 1));

    }

    public void ReAssignTabsValue()
    {
        int cont = 1;
        for (int i = 0; i < tabsText.Count; i++)
        {
            if (tabs[i].activeInHierarchy)
            {
                tabsText[i].text = (cont).ToString();
                ++cont;
            }
        }
    }


    public void RemoveTab()
    {
       
        int pagesNum = instrument.PagesNum;
        int actualPage = instrument.Pages[instrument.ActualPage].Value;

        if (pagesNum > 1)
        {
            //#region Special Case :Pagesnum <10
            //if (pagesNum < 10 && !addButton.activeInHierarchy)
            //{
            //    addButton.SetActive(true);
            //}
            //if (pagesNum - 1 == 1)
            //{
            //    removeButton.SetActive(false);
            //}
            //#endregion

            #region Delete
            for (int i = 0; i < tabs.Count; i++)
            {
                if (tabsText[i].text.Equals(actualPage.ToString()) && tabs[i].activeInHierarchy)
                {
                    tabs[i].SetActive(false);  //Disable tab               
                    instrument.DeletePages(); //Delete from instrument

                    ShowActualPage(compositor.Instruments[compositor.ActualInstrument].Pages[compositor.Instruments[compositor.ActualInstrument].ActualPage].Value.ToString()); //Show actual Page
                    ReAssignTabsValue(); //reassign values of each tab text
                    instrument.ResetPage(compositor.Instruments[compositor.ActualInstrument].PreviusTab);
                    break;
                }
            }
            compositor.BiggerInstrument = 0;
            compositor.UpdateBiggerInstrument();

            #endregion


        }
        //compositorUI.SetShowTimeLine(instrument.ActualPage == (AudioManager.actualBlock - 1));



    }


    #endregion



    [System.Serializable]
    public struct ColorSpritesGridTabs
    {
        public Color spriteDarkOffCells;
        public Color spriteDarkOnCells;
        public Color spriteLightOffCells;
        public Color spriteLightOnCells;
        public Color spriteOnTab;
        public Color spriteOffTab;
    }


}
