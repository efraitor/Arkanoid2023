using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para saber cuando la pelota se ha metido en la zona de muerte
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Vemos si la pelota se ha metido en el Trigger
        if(collision.gameObject.CompareTag("Ball"))
        {
            //Le quitamos una vida al jugador
            GameManager.sharedInstance.lives--;
            //Desactivamos la pelota
            //collision.gameObject.SetActive(false);
            //Llamamos al método que resetea la pelota
            collision.gameObject.GetComponent<Ball>().ResetBall();
        }
    }
}
