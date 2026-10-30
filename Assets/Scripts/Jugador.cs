using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    public bool esJugador1 = true; 
    public float velocidad = 5f;

    public Vector2 limites = new Vector2(8f, 4.5f);

    Rigidbody2D rb;
    Vector2 input;

    public Vector2 Direccion
    {
        get
        {
            if (esJugador1) return transform.right;
            return -transform.right;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        input = Vector2.zero;

        if (esJugador1)
        {
            if (kb.wKey.isPressed) input.y += 1;
            if (kb.sKey.isPressed) input.y -= 1;
            if (kb.aKey.isPressed) input.x -= 1;
            if (kb.dKey.isPressed) input.x += 1;

            input = input.normalized;

            if (input != Vector2.zero)
            {
                float angulo = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angulo);
            }
        }

        if (!esJugador1)
        {
            if (kb.upArrowKey.isPressed) input.y += 1;
            if (kb.downArrowKey.isPressed) input.y -= 1;
            if (kb.leftArrowKey.isPressed) input.x -= 1;
            if (kb.rightArrowKey.isPressed) input.x += 1;

            input = input.normalized;

            if (input != Vector2.zero)
            {
                float angulo = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg + 180f;
                transform.rotation = Quaternion.Euler(0, 0, angulo);
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * velocidad;
        Vector2 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, -limites.x, limites.x);
        pos.y = Mathf.Clamp(pos.y, -limites.y, limites.y);
        rb.position = pos;
    }

    public void Morir()
    {
        Debug.Log(name + " murió");
        Dinamica.instancia.Perder(name);
    }
}