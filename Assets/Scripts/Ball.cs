using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Velocidad de la pelota
    public float speed = 25;
    
    //Referencia a la posici�n inicial de la pelota
    public Vector2 ballInit;

    // Start is called before the first frame update
    void Start()
    {
        //Reiniciamos la bola
        RestartBallMovement();
        //Recogemos la posici�n inicial de la pelota
        ballInit = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    * El objeto collision del par�ntesis contiene la informaci�n del choque (es el objeto que ha chocado con el que lleva el c�digo)
    * En particular, nos interesa saber cuando choca con una pala.
    * - collision.gameObject: tiene informaci�n del objeto contra el cu�l he colisionado
    * - collision.transform.position: tiene informaci�n de la posici�n de ese objeto
    * - collision.collider: tiene informaci�n del collider del objeto
    */
    /*Este m�todo es de Unity y detecta la colisi�n entre dos GO.
    * Al chocar el objeto contra el que choca, le pasa su colisi�n por par�metro */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si la pelota ha colisionado con la pala izquierda
        if (collision.gameObject.name == "Racket")
        {
            //Obtenemos el factor de golpeo, pas�ndole la posici�n de la pelota, la posici�n de la pala, y lo que mide de ancho el collider de la pala(es decir, lo que mide la pala)
            float xF = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            /* Le damos una nueva direcci�n a la pala
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
    /* Es un m�todo de tipo 3. En este caso le pasamos 3 par�metros:
    * - posici�n actual de la pelota
    * - posici�n actual de la pala
    * - ancho de la pala
    * Y el m�todo tal y como le indicamos nos devuelve una variable de tipo float */
    private float HitFactor(Vector2 ballPosition, Vector2 racketPosition, float racketWidth)
    {
        return (ballPosition.x - racketPosition.x) / racketWidth;
    }

    //M�todo para resetear la pelota
    public void ResetBall()
    {
        //Paramos la velocidad de la pelota
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //Ponemos a la pelota en su posici�n inicial
        transform.position = ballInit;
        //Si a�n nos quedan vidas restantes, reseteamos el movimiento de la bola y sino no
        if(GameManager.sharedInstance.lives > 0)
        {
            //Esperamos unos segundos y volvemos a decirle a la bola que se mueva
            Invoke("RestartBallMovement", 2.0f);
        }
    }

    //M�todo para relanzar la bola
    private void RestartBallMovement()
    {
        //La bola se mueve hacia arriba
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;// Vector2.up = new Vector2(0, 1)
    }
}
