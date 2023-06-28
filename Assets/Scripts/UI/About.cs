using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class About : MonoBehaviour
{
    [Multiline(3)]
    public string[] frases;
    public float tiempoEntreLetras=0.1f;
    public float tiempoEntreFrases;
    public Text txtFrases;
    int indice;
    
    

    private IEnumerator Start()
    {
        do
        {
            for (int i = 0; i <= frases[indice].Length; i++)
            {
                txtFrases.text = frases[indice].Substring(0, i);
                if (i%2==0)
                {
                    txtFrases.text = txtFrases.text + "|";
                }
                yield return new WaitForSeconds(tiempoEntreLetras);
            }
            yield return new WaitForSeconds(tiempoEntreFrases);
            for (int i = 0; i <= frases[indice].Length; i++)
            {
                txtFrases.text = frases[indice].Substring(0, frases[indice].Length - i);
                yield return new WaitForSeconds(tiempoEntreLetras/2f);
            }
            indice = (indice + 1) % frases.Length;
        } while (true);
    }
}
