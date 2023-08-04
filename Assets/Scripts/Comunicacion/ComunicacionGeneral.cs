using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [Space]
    [Header("Excepciones")]
    [SerializeField] private string qrConformatoInvalido;
    [SerializeField] private string visitaASiMismo;
    [SerializeField] private string qrYaUtilizado;
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
        int qrsGenerados = GetNumeroQrsGenerados();

        string texto = qrsGenerados + "|" + MorionTools.nombreUsuario;
        texto += "|" + (MorionTools.Cargar("sensible"));
        texto += "|" + (MorionTools.Cargar("escala"));
        texto += "|" + (MorionTools.Cargar("desplasamiento"));
        Texture2D t2d = GenerarQR(texto);
        mySprite = Sprite.Create(t2d, new Rect(0.0f, 0.0f, t2d.width, t2d.height), new Vector2(0.5f, 0.5f));
        imaQR.sprite = mySprite;
        //imagenCamara.texture = GenerarQR(texto) as Texture;
        print("Generado " + texto);
    }

    private static int GetNumeroQrsGenerados()
    {
        string qrs = MorionTools.Cargar("QR");
        int qrsGenerados;
        if (qrs == "") qrsGenerados = 0;
        else qrsGenerados = int.Parse(qrs);
        qrsGenerados++;
        MorionTools.Guardar("QR", qrsGenerados.ToString());

        return qrsGenerados;
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

        if (strs.Length != 5)
        {
            Mensajes.singleton.Mensaje(qrConformatoInvalido);
            print("El formato del QR es incorrecto, genere uno nuevo");
            return;
        }

        if (strs[1].Equals(MorionTools.nombreUsuario))
        {
            Mensajes.singleton.Mensaje(visitaASiMismo);
            print("No puede visitarse a usted mismo");
            return;
        }

        bool yaVisitado = ValidarVisitado(strs);

        if (yaVisitado)
        {
            Mensajes.singleton.Mensaje(qrYaUtilizado);
            print("Ya ha utilizado este QR antes, utilice uno nuevo");
            return;
        }

        float sensible = float.Parse(strs[2]);
        float escala = float.Parse(strs[3]);
        float desplasamiento = float.Parse(strs[4]);

        goMarrano.SetActive(true);

        cerdoMaterial.SetValues(sensible, escala, desplasamiento);

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

    private static bool ValidarVisitado(string[] strs)
    {
        bool yaVisitado = false;
        string visitados = MorionTools.Cargar("Visita_" + strs[1]);
        if (visitados == "") MorionTools.Guardar("Visita_" + strs[1], strs[0]);
        else
        {
            string[] visitadosArray = visitados.Split('|');
            yaVisitado = visitadosArray.Contains(strs[0]);
            if (!yaVisitado) MorionTools.Guardar("Visita_" + strs[1], visitados + "|" + strs[0]);
        }

        return yaVisitado;
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
