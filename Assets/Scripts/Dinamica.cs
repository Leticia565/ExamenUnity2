using UnityEngine;
using UnityEngine.SceneManagement;

public class Dinamica : MonoBehaviour
{
    public static Dinamica instancia;
    public static bool terminada;

    void Awake()
    {
        instancia = this;
        terminada = false;
        Time.timeScale = 1f;
    }

    public void Perder(string perdedor)
    {
        if (terminada) return;  

        terminada = true;
        Debug.Log(perdedor + " recibió el picotazo por atrás. Reiniciando...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}