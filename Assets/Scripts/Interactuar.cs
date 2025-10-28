using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactuar : MonoBehaviour
{
    [Header("Entrada")]
    public KeyCode teclaInteraccion = KeyCode.Space;

    [Header("Icono de interacción")]
    public Sprite iconoSprite;                     // Ícono que muestra "puedes interactuar"
    public Vector3 iconOffset = new Vector3(0f, 3.5f, 0f);
    public int iconSortingOrder = 50;

    [Header("Pensamientos (por pasos)")]
    [TextArea(2, 4)]
    public string[] pensamientos;                  // Cada elemento es un "paso" del texto
    public Vector3 globoOffset = new Vector3(0f, 2.1f, 0f);
    public int textoSortingOrder = 60;             // Orden de dibujo del texto (encima del ícono)

    private bool enRango = false;
    private Transform protaTransform;

    // Estado del diálogo/pensamiento
    private bool dialogoActivo = false;
    private int idxPensamiento = 0;

    // Instancias creadas dinámicamente
    private GameObject iconoGO;
    private GameObject globoGO;
    private TextMesh globoText; // Usamos TextMesh (mundo) para simplificar

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    private void Update()
    {
        if (!enRango || protaTransform == null) return;

        // Mantener el icono/globo sobre el prota
        if (iconoGO != null) iconoGO.transform.position = protaTransform.position + iconOffset;
        if (globoGO != null && globoGO.activeSelf) globoGO.transform.position = protaTransform.position + globoOffset;

        // Interacción por pasos
        if (Input.GetKeyDown(teclaInteraccion))
        {
            if (!dialogoActivo)
            {
                // Iniciar diálogo
                IniciarDialogo();
            }
            else
            {
                // Avanzar paso
                AvanzarDialogo();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        enRango = true;
        protaTransform = other.transform;

        // Al entrar al rango, mostramos el icono
        MostrarIcono(true);
        // Reinicia el índice por si re-entró
        idxPensamiento = 0;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        enRango = false;
        protaTransform = null;

        // Oculta todo al salir
        MostrarIcono(false);
        CerrarDialogo();
    }

    // ---------------- Diálogo por pasos ----------------
    private void IniciarDialogo()
    {
        // Si no hay frases, muestra una por defecto
        if (pensamientos == null || pensamientos.Length == 0)
        {
            MostrarGlobo("Buscas si hay medicamentos\n...\nlos encontraste");
            dialogoActivo = true; // igual marcamos activo para permitir cerrar en el siguiente Space
            return;
        }

        idxPensamiento = 0;
        MostrarGlobo(pensamientos[idxPensamiento]);

        dialogoActivo = true;
        // Mientras hay diálogo, puedes ocultar el icono si quieres:
        MostrarIcono(false);
    }

    private void AvanzarDialogo()
    {
        if (pensamientos == null || pensamientos.Length == 0)
        {
            // No hay más contenido, cerramos
            CerrarDialogo();
            return;
        }

        idxPensamiento++;

        if (idxPensamiento >= pensamientos.Length)
        {
            // Fin del diálogo
            CerrarDialogo();
        }
        else
        {
            // Siguiente línea
            MostrarGlobo(pensamientos[idxPensamiento]);
        }
    }

    private void CerrarDialogo()
    {
        dialogoActivo = false;
        if (globoGO != null) globoGO.SetActive(false);

        // Al terminar, vuelve a mostrarse el icono si el jugador sigue en rango
        if (enRango) MostrarIcono(true);
    }

    // ---------------- UI en mundo ----------------
    private void MostrarGlobo(string texto)
    {
        AsegurarGlobo();
        globoText.text = texto;
        globoGO.transform.position = protaTransform.position + globoOffset;
        globoGO.SetActive(true);
    }

    private void AsegurarGlobo()
    {
        if (globoGO != null) return;

        globoGO = new GameObject("GloboPensamiento");
        globoText = globoGO.AddComponent<TextMesh>();
        globoText.text = "";
        globoText.anchor = TextAnchor.MiddleCenter;
        globoText.alignment = TextAlignment.Center;
        globoText.characterSize = 0.2f; // ajusta a tu escala
        globoText.fontSize = 64;        // calidad
        globoText.color = Color.white;

        var mr = globoGO.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.sortingOrder = textoSortingOrder;
            // mr.sortingLayerName = "UI"; // opcional si usas una Sorting Layer específica
        }

        globoGO.SetActive(false);
    }

    private void MostrarIcono(bool mostrar)
    {
        if (mostrar)
        {
            if (iconoGO == null && iconoSprite != null && protaTransform != null)
            {
                iconoGO = new GameObject("IconoInteraccion");
                var sr = iconoGO.AddComponent<SpriteRenderer>();
                sr.sprite = iconoSprite;
                sr.sortingOrder = iconSortingOrder;
                iconoGO.transform.position = protaTransform.position + iconOffset;
            }
            if (iconoGO != null) iconoGO.SetActive(true);
        }
        else
        {
            if (iconoGO != null)
            {
                Object.Destroy(iconoGO);
                iconoGO = null;
            }
        }
    }
}
