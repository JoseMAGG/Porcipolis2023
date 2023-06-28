using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Page 
{
    [System.Serializable]
    public class Tripleta
    {
        public int i;
        public int j;
        public string name;
        

        public Tripleta()
        {
        }
        public Tripleta(int ii, int jj, string name)
        {
            i = ii;
            j = jj;
            this.name = name;

        }
        public Tripleta(string str)
        {
            this.DesdeString(str);
        }

        public string HaciaString()
        {
            return i + "/" + j + "/" + name;
        }

        public void DesdeString(string str)
        {
            string[] arreglo = str.Split('/');

            i = int.Parse(arreglo[0]);
            j = int.Parse(arreglo[1]);
            name = arreglo[2];
           

        }
    }

   
   
    public struct NoteData {
         public bool isActive;
         public string name;
        
    }
    public List<Tripleta> tripletas=new List<Tripleta>();
    public NoteData[,] Grid = new NoteData[12, 8];

    public bool IsActive;
    public int Value;

 

  

    #region Constructor
    public Page() { }


    public Page(bool state, int value)
    {
        
        IsActive = state;
        this.Value = value;
        CreateGrid();
      


    }


    public Page(string str)
    {
        this.DesdeString(str);
  
        TripletasToGrid();
        ResetTripletas();
    }


    #endregion

    #region Save 
    public string HaciaString()
    {
        string result = "";
        GridToTripletas();

        foreach (Tripleta tripleta in tripletas)
        {
            if (!result.Equals(""))
            {
                result += "@";
            }

            result += tripleta.HaciaString();

        }
        ResetTripletas();

        return result;

    }


    #endregion

    #region Load
    public void DesdeString(string str)
    {
        string[] arreglo;
        arreglo = str.Split('@');
        tripletas = new List<Tripleta>();
        for (int i = 0; i < arreglo.Length; i++)
        {

            tripletas.Add(new Tripleta(arreglo[i]));

        }
        

    }

    #endregion


    void ResetTripletas() {
        tripletas = new List<Tripleta>();
    }

    void GridToTripletas()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                if (Grid[i, j].isActive)
                {
                    tripletas.Add(new Tripleta(i, j,Grid[i,j].name));
                }
            }
        }
    }

    void TripletasToGrid() {
       CreateGrid();

        foreach (var tripleta in tripletas)
        {
            Grid[tripleta.i, tripleta.j].isActive = true;
            Grid[tripleta.i, tripleta.j].name = tripleta.name;
        }

    }

    void CreateGrid() {


        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Grid[i, j].isActive = false;
                Grid[i, j].name = "";

            }
        }
    }


}