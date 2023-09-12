using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashControl : MonoBehaviour
{

    public static int SceneNumber;

    // Start is called before the first frame update
    void Start()
    {

        //validacion de las escenas.- ayuda a generar la secuencia de ejecución
        if (SceneNumber == 0)
        {
            //redireccionar a la segunda escena en un tiempo determinado (segundos)
            StartCoroutine(toSplashScreenInfo());

        }

        if (SceneNumber == 1)
        {
            //llamar a la escena de menu
            StartCoroutine(toSplashScreenMenu());
        }

    }

    public IEnumerator toSplashScreenInfo()
    {
        yield return new WaitForSeconds(5);
        SceneNumber = 1;
        SceneManager.LoadScene(1);
        
    }
    public IEnumerator toSplashScreenMenu()
    {
        yield return new WaitForSeconds(5);
        SceneNumber = 2;
        SceneManager.LoadScene(2);

    }

}
