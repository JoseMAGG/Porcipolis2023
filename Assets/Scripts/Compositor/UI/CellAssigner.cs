using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAssigner : MonoBehaviour
{
    void Awake()
    {
        AssignCells();
    }

    private void AssignCells()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform note = transform.GetChild(i);
            if (note.gameObject.activeInHierarchy)
            {
                for (int j = 0; j < note.childCount; j++)
                {
                    note.GetChild(j).gameObject.AddComponent<Cell>();
                    Cell noteCell = note.GetChild(j).gameObject.GetComponent<Cell>();
                    noteCell.row = i;
                    noteCell.col = j;
                    noteCell.nombre = note.name;
                }
            }
        }
    }
}
