using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
public class ComunicacionGeneral : MonoBehaviour
{
    private WebCamTexture camTexture;
    private Rect screenRect;
    public RawImage imagenCamara;
    public Image imaQR;
    public CargarCerdoMaterial cerdoMaterial;
    public Material materialVisita;
    public GameObject goMarrano;
    public Animator[] animators;
    public bool finalizado = false;
    public bool salida;
    private Sprite mySprite;
    void Start()
    {
        cerdoMaterial = goMarrano.GetComponent<CargarCerdoMaterial>();
        if (salida)
        {
            CrearQR();
        }
        else
        {
            screenRect = new Rect(0, 0, Screen.width, Screen.height);
            camTexture = new WebCamTexture();
            camTexture.requestedHeight = Screen.height;
            camTexture.requestedWidth = Screen.width;
            if (camTexture != null)
            {
                camTexture.Play();
            }
            StartCoroutine(OnGUI2());
        }
    }

    void CrearQR()
    {
        string texto = MorionTools.nombreUsuario;
        texto += "|" + (MorionTools.Cargar("sensible"));
        texto += "|" + (MorionTools.Cargar("escala"));
        texto += "|" + (MorionTools.Cargar("desplasamiento"));
        Texture2D t2d = GenerarQR(texto);
        mySprite = Sprite.Create(t2d, new Rect(0.0f, 0.0f, t2d.width, t2d.height), new Vector2(0.5f, 0.5f));
        imaQR.sprite = mySprite;
        //imagenCamara.texture = GenerarQR(texto) as Texture;
        print("Generado " + texto);
    }

    private void FixedUpdate()
    {
        imagenCamara.texture = camTexture;
    }

    IEnumerator OnGUI2()
    {
        while (!finalizado)
        {
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                // decode the current frame
                var result = barcodeReader.Decode(camTexture.GetPixels32(),
                  camTexture.width, camTexture.height);
                if (result != null)
                {
                    Debug.Log("Leido del QR: " + result.Text);
                    Decodificar(result.Text);
                }
            }
            catch (System.Exception ex) { Debug.LogWarning(ex.Message); }

            yield return new WaitForSeconds(1f);
        }
    }

    public void Decodificar(string texto)
    {
        if (finalizado)
        {
            return;
        }
        //texto = texto.Replace('.', ',');
        string[] strs = texto.Split('|');
        if (strs.Length == 4)
        {
            float sensible = float.Parse(strs[1]);
            float escala = float.Parse(strs[2]);
            float desplasamiento = float.Parse(strs[3]);

            goMarrano.SetActive(true);

            cerdoMaterial.SetValues(sensible, escala, desplasamiento);

            //cerdoMaterial.ActualizarMaterial();


            //materialVisita.SetFloat("_Escala", escala);
            //materialVisita.SetFloat("_Sensible", sensible);
            //materialVisita.SetFloat("_Desplazamiento", desplasamiento);

            imagenCamara.gameObject.SetActive(false);
            GestorEconomia.singleton.SumarRecurso(1, 10);
            enabled = false;
            finalizado = true;
            camTexture.Stop();
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetBool("visita", true);
            }
        }
    }

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }
    public Texture2D GenerarQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    public void DesactivarCamara()
    {
        camTexture.Stop();
    }
}
