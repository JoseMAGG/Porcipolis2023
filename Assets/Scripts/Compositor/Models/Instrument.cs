using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Instrument
{


 
    public  int PreviusTab;
    public int ActualPage; //actual Page who Contains value Text
    public int PagesNum; //Amount of Pages

    public List<Page> pagesData = new List<Page>();

    public List<Page> Pages;

    public string name;

    public int numNotes;



    #region Constructors
    public Instrument() { }

    public Instrument(int pagesNum,string name,int numNotes)
    {
        this.numNotes = numNotes;
        this.name = name;
        this.PagesNum = pagesNum;
        CreatePages();
        Pages[0] = new Page(true, 1);
        ActualPage = 0;
        PreviusTab = 0;
    }

    public Instrument(string str,string name)
    {
        DesdeString(str);
        PagesDataToPage();
        this.PagesNum = pagesData.Count;
        ActualPage = 0;
        ResetPagesData();
        string[] insProp = name.Split(',');
        this.name = insProp[0];
        this.numNotes = int.Parse(insProp[1]);
    }

    #endregion

    #region CRUD
    public void DeletePages()
    {
       
        PreviusTab = ActualPage;
        Pages[PreviusTab].IsActive = false;
        SetActualPage();
        Pages[ActualPage].IsActive = true;
        PagesNum -= 1;

    }
    public void AddPages()
    {

        PreviusTab = ActualPage;

        PagesNum += 1;

        for (int i = 0; i < Pages.Count; i++) //active first inactive page
        {
            if (!Pages[i].IsActive) {
                
                Pages[i].IsActive = true;
                
                Pages[i].Value = PagesNum;
                ActualPage = i;
                break;
            }
        }

   

    }

    public void CreatePages()
    {
        Pages = new List<Page>();
        for (int i = 0; i < CompositorUI.numTabs; i++)
        {
            Pages.Add( new Page(false,-1));
        }
        
    }
    public void SetActualPage()
    {
        if (ActualPage == 9 || Pages[ActualPage].Value == (PagesNum ))
        {
            for (int i = 0; i < Pages.Count; i++)
            {
                if (Pages[i].IsActive && (Pages[i].Value + 1) == Pages[ActualPage].Value)
                {
                    ActualPage = i;
                    return;
                }
            }
            ActualPage -= 1; // No necesario
        }
        else {
            for (int i = 0; i < Pages.Count; i++)
            {
                if (Pages[i].IsActive && (Pages[i].Value-1)==Pages[ActualPage].Value) {
                    ActualPage = i;
                    return;
                }
            }
            ActualPage += 1; // No necesario

        }
           
    }
    public void ResetPage(int i)
    {
        int value = Pages[i].Value;

        if (value > Pages[ActualPage].Value) {
            value -= 1;
        }


        for (int x = ActualPage; x < Pages.Count; x++)
        {
            if (Pages[x].IsActive)
            {
                Pages[x].Value = value;
                value++;
            }
        }

        if (i == PagesNum)
        {

            for (int x= ActualPage-2; x>=0; x--)
            {
                if (Pages[x].IsActive) {
                    PreviusTab = x;
                    break;
                }
            }
        }
        else {
                      
            #region Useful(?)
            for (int x = ActualPage - 1; x >= 0; x--)
            {
                if (Pages[x].IsActive)
                {
                    PreviusTab = x;
                    break;
                }
            }
            #endregion
        }

        Pages[i] = new Page(false,-1);
        
    }

    public Page GetPage(int index) {

        foreach (var page in Pages)
        {
            
            if (page.Value == index) {
                return page;
            }
        }

        return null;
    }
    public Page GetPageActive(int index)
    {

        foreach (var page in Pages)
        {

            if (page.Value == index && page.IsActive)
            {
                return page;
            }
        }

        return null;
    }


  
    #endregion

    #region Save
    public string HaciaString()
    {
        PagesToPagesData();
        string result = "";

        foreach (Page pagina in pagesData)
        {
            if (!result.Equals(""))
            {
                result += "+";
            }

            result += pagina.HaciaString();

        }
        ResetPagesData();

        return result;

    }

    public void PagesToPagesData()
    {
        //Sor page for value;
        for (int i = 0; i < Pages.Count; i++)
        {
            Page newPage = GetPageActive(i + 1);
            if (newPage != null)
                pagesData.Add(newPage);
        }
    }

    public void PagesDataToPage() {

        CreatePages();

        for (int i = 0; i < pagesData.Count; i++)
        {
            
            Pages[i] = new Page(true, (i + 1));
            Pages[i].Grid = pagesData[i].Grid;
            
        }

    }
    #endregion


    #region Load
    public void DesdeString(string str)
    {
        string[] arreglo;
        arreglo = str.Split('+');
        pagesData = new List<Page>();
        for (int i = 0; i < arreglo.Length; i++)
        {

            pagesData.Add(new Page(arreglo[i]));

        }


    }

    #endregion

    void ResetPagesData() {
        pagesData = new List<Page>();
    }
}
