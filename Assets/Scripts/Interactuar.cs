using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactuar : MonoBehaviour
{
    public KeyCode teclaInteraccion = KeyCode.Space;

    [Header("Indicador de interacción")]
    public Sprite iconoSprite;
    public Vector3 offset = new Vector3(0f, 3.5f, 0f);
    public Vector2 cuadroDialogoPosicion = new Vector2(0.27f, 0.52f);
    private bool puedeCambiar = false;
    private Transform protaTransform;
    private GameObject iconoInstanciado;

    public GameObject CuadroDeDialogo; 
    public GameObject TextoEjemplo;

    void Update()
    {
        if (puedeCambiar && protaTransform != null)
        {
            if (iconoInstanciado != null)
                iconoInstanciado.transform.position = protaTransform.position + offset;

            if (Input.GetKeyDown(teclaInteraccion))
            {
                CuadroDeDialogo.SetActive(true);
                TextoEjemplo.SetActive(true);
            }
        }
        else if(Input.GetKeyDown(teclaInteraccion) && CuadroDeDialogo.activeSelf)
        {
            CuadroDeDialogo.SetActive(false);
            TextoEjemplo.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeCambiar = true;
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
            puedeCambiar = false;

            if (iconoInstanciado != null)
            {
                Destroy(iconoInstanciado);
                iconoInstanciado = null;
            }
        }
    }
}
