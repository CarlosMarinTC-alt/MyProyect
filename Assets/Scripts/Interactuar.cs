using UnityEngine;
using TMPro;
using System.Collections;

public class Interactuar : MonoBehaviour
{
    public KeyCode teclaInteraccion = KeyCode.Space;

    [Header("Indicador de interacción")]
    public Sprite iconoSprite;
    public Vector3 offset = new Vector3(0f, 3.5f, 0f);

    private bool puedeInteractuar = false;
    private Transform protaTransform;
    private GameObject iconoInstanciado;

    [Header("Diálogo")]
    public GameObject CuadroDeDialogo;
    public TextMeshProUGUI TextoEjemplo;
    public GameObject ProtaGrande;
    private GameObject Prota;
    private MonoBehaviour ProtaScript;


    private string[] lineasDialogo = {
        "Hola",
        "Como estas"
    };

    private int indiceDialogo = 0;
    private bool mostrandoDialogo = false;

    void Start()
    {
        if (CuadroDeDialogo != null) CuadroDeDialogo.SetActive(false);
        if (CuadroDeDialogo != null) ProtaGrande.SetActive(false);
        if (TextoEjemplo != null) TextoEjemplo.enabled = false;
        Prota = GameObject.FindGameObjectWithTag("Player");
        ProtaScript = Prota.GetComponent<MonoBehaviour>();
    }

    void Update()
    {
        if (puedeInteractuar && protaTransform != null)
        {
            if (iconoInstanciado != null)
                iconoInstanciado.transform.position = protaTransform.position + offset;

            if (Input.GetKeyDown(teclaInteraccion))
            {
                if (!mostrandoDialogo)
                {
                    StartCoroutine(MostrarDialogo());
                }
                else
                {
                    SiguienteLinea();
                }
            }
        }
    }

    private IEnumerator MostrarDialogo()
    {
        mostrandoDialogo = true;
        indiceDialogo = 0;
        
        ProtaScript.enabled = false;

        CuadroDeDialogo.SetActive(true);
        ProtaGrande.SetActive(true);
        TextoEjemplo.enabled = true;
        TextoEjemplo.SetText(lineasDialogo[indiceDialogo]);

        yield break; 
    }

    private void SiguienteLinea()
    {
        indiceDialogo++;

        if (indiceDialogo < lineasDialogo.Length)
        {
            TextoEjemplo.SetText(lineasDialogo[indiceDialogo]);
        }
        else
        {
            CuadroDeDialogo.SetActive(false);
            ProtaGrande.SetActive(false);
            TextoEjemplo.enabled = false;
            mostrandoDialogo = false;

            ProtaScript.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeInteractuar = true;
            protaTransform = collision.transform;

            if (iconoSprite != null && iconoInstanciado == null)
            {
                iconoInstanciado = new GameObject("IconoInteraccion");
                var sr = iconoInstanciado.AddComponent<SpriteRenderer>();
                sr.sprite = iconoSprite;
                sr.sortingOrder = 10;
                iconoInstanciado.transform.position = protaTransform.position + offset;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeInteractuar = false;

            if (iconoInstanciado != null)
            {
                Destroy(iconoInstanciado);
                iconoInstanciado = null;
            }
        }
    }
}
