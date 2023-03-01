using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar entre escenas

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si pulsamos la tecla Escape salimos del juego
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Exit game");
            //Esto apaga el juego
            Application.Quit();
        }
    }

    //Método para cuando hacemos click en el botón
    public void OnButtonClick()
    {
        //Cargamos una escena por su nombre
        SceneManager.LoadScene("Level-1");
    }
}
