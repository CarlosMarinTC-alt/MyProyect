using UnityEngine;

public class Cambiar_Posicion : MonoBehaviour
{
    public Vector2 Posicion_habitacion = new Vector2(0.27f, 1.71f);
    public Vector2 Posicion_cama = new Vector2(5.35f, 0.52f);
    public Vector2 Posicion_hab_tios = new Vector2(18.25f, 0.82f);
    public Vector2 Posicion_baño = new Vector2(1.83f, -3.12f);
    public Vector2 Posicion_pasilloAbajo = new Vector2(0f, 0f);


    public Vector2 Posicion_regresoP_cuarto = new Vector2(8.92f, 0.55f);
    public Vector2 Posicion_regresoP_tios = new Vector2(4.37f, 4.99f);
    public Vector2 Posicion_regresoP_pasilloAbajo = new Vector2(0f, 0f);
    public Vector2 Posicion_regresoP_baño = new Vector2(6.07f, 2.08f);
    

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Transform protaTransform = player.transform;
            int indiceEscenario = Escenario.ultimoIndiceEscenario; 
            int regresarPasillo = Escenario.regresoPasillo; 

            Debug.Log(regresarPasillo);

            switch (indiceEscenario)
            {
                case 0:
                    break;
                case 1:
                    protaTransform.position = Posicion_habitacion;
                    break;
                case 3:
                    switch (regresarPasillo)
                    {
                        case 0:
                            protaTransform.position = Posicion_regresoP_cuarto;
                            break;
                        case 1:
                            protaTransform.position = Posicion_regresoP_tios;
                            break;
                        case 2:
                            protaTransform.position = Posicion_regresoP_baño;
                            break;
                        case 3:
                            protaTransform.position = Posicion_regresoP_pasilloAbajo;
                            break;        
                    }
                    break;
                case 4:
                    protaTransform.position = Posicion_hab_tios;
                    break;
                case 5:
                    protaTransform.position = Posicion_baño;
                    break;    
                case 99:
                    protaTransform.position = Posicion_cama;
                    break;
            }
        }
    }
}
