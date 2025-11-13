using UnityEngine;

public class Escenario_Aparece : MonoBehaviour
{
    public GameObject EscenarioAparece;
    public GameObject EscenarioDesaparece;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EscenarioAparece.SetActive(true);
            EscenarioDesaparece.SetActive(false);
        }
    }
}
