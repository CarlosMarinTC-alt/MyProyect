using UnityEngine;

public class Cambiar_Posicion : MonoBehaviour
{
    public Vector2 Posicion_pasillo = new Vector2(8.92f, 0.55f);
    public Vector2 Posicion_habitacion = new Vector2(0.27f, 1.71f);
    public Vector2 Posicion_cama = new Vector2(5.35f, 0.52f);
    public Vector2 Posicion_hab_tios = new Vector2(18.25f, 0.82f);

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Transform protaTransform = player.transform;
            int indiceEscenario = Escenario.ultimoIndiceEscenario; // <-- valor recibido

            switch (indiceEscenario)
            {
                case 0:
                    protaTransform.position = Posicion_pasillo;
                    break;
                case 1:
                    protaTransform.position = Posicion_habitacion;
                    break;
                case 3:
                    protaTransform.position = Posicion_pasillo;
                    break;
                case 4:
                    protaTransform.position = Posicion_hab_tios;
                    break;
                case 99:
                    protaTransform.position = Posicion_cama;
                    break;
            }
        }
    }
}
