using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Header("Escena")]
    public int NewScene;
    public AudioClip click;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadNewScene()
    {
        ControllerSound.instance.ExecuteSound(click);
        SceneManager.LoadScene(NewScene);
    }
    public void LoadOptions()
    {
        
        ControllerSound.instance.ExecuteSound(click);
        SceneManager.LoadScene(NewScene);
        DontDestroy.instance.GetComponent<Options>().ActiveOptionsInicio();
    }
    public void LoadInicio()
    {
        ControllerSound.instance.ExecuteSound(click);
        SceneManager.LoadScene(NewScene);
        DontDestroy.instance.GetComponent<Options>().DesactiveOptionsInicio();
    }
    public void EndGame()
    {
        ControllerSound.instance.ExecuteSound(click);
        Application.Quit();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PJ"))
        {
            SceneManager.LoadScene(NewScene);
        }
    }
}
