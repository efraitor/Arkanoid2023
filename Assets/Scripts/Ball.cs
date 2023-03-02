using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Velocidad de la pelota
    public float speed = 25;
    
    //Referencia a la posición inicial de la pelota
    public Vector2 ballInit;

    // Start is called before the first frame update
    void Start()
    {
        //Reiniciamos la bola
        RestartBallMovement();
        //Recogemos la posición inicial de la pelota
        ballInit = transform.position;
        //Llamamos a la Corrutina
        StartCoroutine(UpgradeDifficulty());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    * El objeto collision del paréntesis contiene la información del choque (es el objeto que ha chocado con el que lleva el código)
    * En particular, nos interesa saber cuando choca con una pala.
    * - collision.gameObject: tiene información del objeto contra el cuál he colisionado
    * - collision.transform.position: tiene información de la posición de ese objeto
    * - collision.collider: tiene información del collider del objeto
    */
    /*Este método es de Unity y detecta la colisión entre dos GO.
    * Al chocar el objeto contra el que choca, le pasa su colisión por parámetro */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Cada vez que choca la pelota contra algo reproduce su sonido
        GetComponent<AudioSource>().Play();
        //Si la pelota ha colisionado con la pala izquierda
        if (collision.gameObject.name == "Racket")
        {
            //Obtenemos el factor de golpeo, pasándole la posición de la pelota, la posición de la pala, y lo que mide de ancho el collider de la pala(es decir, lo que mide la pala)
            float xF = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            /* Le damos una nueva dirección a la pala
            * En este caso con una Y hacia arriba
            * Y nuestro factor de golpeo calculado
            * Normalizado todo el vector a 1, para que la bola no acelere */
            Vector2 direction = new Vector2(xF, 1).normalized;
            //Le decimos a la bola que salga con esa velocidad previamente calculada
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    /*
    * 1 - La bola choca contra la parte derecha de la raqueta
    * 0 - La bola choca contra el centro de la raqueta
    * -1 - La bola choca contra la parte izquierda de la raqueta
    */
    /* Es un método de tipo 3. En este caso le pasamos 3 parámetros:
    * - posición actual de la pelota
    * - posición actual de la pala
    * - ancho de la pala
    * Y el método tal y como le indicamos nos devuelve una variable de tipo float */
    private float HitFactor(Vector2 ballPosition, Vector2 racketPosition, float racketWidth)
    {
        return (ballPosition.x - racketPosition.x) / racketWidth;
    }

    //Método para resetear la pelota
    public void ResetBall()
    {
        //Reseteamos la bola a la velocidad inicial que tenía    
        speed = 10f;
        //Paramos la velocidad de la pelota
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //Ponemos a la pelota en su posición inicial
        transform.position = ballInit;
        //Si aún nos quedan vidas restantes, reseteamos el movimiento de la bola y sino no
        if(GameManager.sharedInstance.lives > 0)
        {
            //Esperamos unos segundos y volvemos a decirle a la bola que se mueva
            Invoke("RestartBallMovement", 2.0f);
        }
    }

    //Método para relanzar la bola
    private void RestartBallMovement()
    {
        //La bola se mueve hacia arriba
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;// Vector2.up = new Vector2(0, 1)
    }

    //Corrutina para aumentar la dificultad del juego acelerando la bola
    //La corrutina se establece fuera del tiempo del bucle de juego, se usa en acciones que deben hacerse en un momento puntual, independientes de lo demás
    private IEnumerator UpgradeDifficulty()
    {
        //Hacemos un bucle siempre verdadero, para que cada segundo aumente un poco la velocidad de la pelota
        while (true) //Mientras la condición del bucle se cumpla lo hace. Así le indicamos que la condición siempre será verdad
        {
            //Hace que la corrutina se espere un segundo
            yield return new WaitForSeconds(1.0f);
            //Aumento de la velocidad de la bola
            //speed += 0.5f;
            speed *= 1.005f;
        }
    }
}
