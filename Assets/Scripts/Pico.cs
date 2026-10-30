using UnityEngine;
using UnityEngine.SceneManagement;

public class Pico : MonoBehaviour
{
    Jugador dueno;

    void Awake()
    {
        dueno = GetComponentInParent<Jugador>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Revisar(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Revisar(other);
    }

    void Revisar(Collider2D other)
    {
        if (other.GetComponent<Pico>() != null) return;

        Jugador victima = other.GetComponentInParent<Jugador>();
        if (victima == null || victima == dueno) return;

        Vector2 haciaAtacante =
            (dueno.transform.position - victima.transform.position).normalized;

        bool estaAtras = Vector2.Dot(victima.Direccion, haciaAtacante) < 0f;

        bool mismoSentido = Vector2.Dot(victima.Direccion, dueno.Direccion) > 0f;

        if (estaAtras && mismoSentido)
        {
            Debug.Log(dueno.name + " tocó la espalda de " + victima.name + ". Reiniciando...");

            if (Dinamica.instancia != null)
                Dinamica.instancia.Perder(victima.name);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}