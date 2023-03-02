using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar entre escenas
using UnityEngine.UI; //Librería para la interfaz de Unity
using TMPro; //Librería para usar los componentes del TextMeshPro

public class GameManager : MonoBehaviour
{
    //Referencias para las imágenes de las vidas
    public Image live1, live2, live3;
    //Texto de GameOver
    public TextMeshProUGUI gameOverText;

    //Iniciamos el contador de vidas
    public int lives = 3;

    //Hacemos un Singleton del script GameManager, para poder usar sus propiedades desde cualquier script
    public static GameManager sharedInstance;

    private void Awake()
    {
        //Primero comprobamos si la instancia del Singleton está vacía
        if(sharedInstance == null)
            //La relleno con todo el contenido de este código
            sharedInstance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Desactivamos la imagen GameOver
        gameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Controlamos las imágenes de las vidas, dependiendo de cuantas nos quedan
        //Si nos quedan menos de 3 vidas
        /*if(lives < 3)
        {
            //Desactivamos la imagen de la vida 3
            live3.enabled = false; //enabled desactiva un componente de un GO
        }
        //Si nos quedan menos de 2 vidas
        if(lives < 2)
        {
            //Desactivamos la imagen de la vida 2
            live2.enabled = false; //enabled desactiva un componente de un GO
        }
        //Si nos quedan menos de 1 vidas
        if(lives < 1)
        {
            //Desactivamos la imagen de la vida 1
            live1.enabled = false; //enabled desactiva un componente de un GO
        }*/
        //Nos damos cuenta de que al ver el valor de una sola variable, podemos sustituir lo de arriba por un switch
        switch(lives)
        {
            //En el caso en el que las vidas sean 3
            case 3:
                //Activamos la imagen de la vida 3
                live3.enabled = true;
                //Activamos la imagen de la vida 2
                live2.enabled = true;
                //Activamos la imagen de la vida 1
                live1.enabled = true;
                break;
            //En el caso en el que las vidas sean 2
            case 2:
                //Desactivamos la imagen de la vida 3
                live3.enabled = false;
                //Activamos la imagen de la vida 2
                live2.enabled = true;
                //Activamos la imagen de la vida 1
                live1.enabled = true;
                break;
            //En el caso en el que las vidas sean 1
            case 1:
                //Desactivamos la imagen de la vida 3
                live3.enabled = false;
                //Desactivamos la imagen de la vida 2
                live2.enabled = false;
                //Activamos la imagen de la vida 1
                live1.enabled = true;
                break;
            //En el caso en el que las vidas sean 0
            case 0:
                //Desactivamos la imagen de la vida 3
                live3.enabled = false;
                //Desactivamos la imagen de la vida 2
                live2.enabled = false;
                //Desactivamos la imagen de la vida 1
                live1.enabled = false;
                //Activamos la imagen GameOver
                gameOverText.enabled = true;
                break;
            //En cualquier otro caso
            default:
                //Desactivamos la imagen de la vida 3
                live3.enabled = false;
                //Desactivamos la imagen de la vida 2
                live2.enabled = false;
                //Desactivamos la imagen de la vida 1
                live1.enabled = false;
                break;
        }

        //Vamos a contar cuantos bloques hay en esta partida
        //Creamos un array donde meter todos los bloques que tenemos en esta partida
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        //FindGameObjectsWithTag = busca objetos por etiqueta
        //Si el tamaño del array es 0 (osea se ha quedado vacío, no quedan bloques)
        if(blocks.Length == 0)
        {
            //Invocamos al método para hacer el cambio de escena tras 2 segundos
            Invoke("GoToNextScene", 2.0f);
        }

        //Si pulsamos la tecla Escape salimos del juego
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Exit game");
            //Esto apaga el juego
            Application.Quit();
        }
    }

    //Método para cambiar entre escenas
    private void GoToNextScene()
    {
        //Cambiamos de escena yendo a la siguiente que tengamos preparada en la Build
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //buildIndex es el número de la escena actual en los Build Settings
    }
}
