using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Header("Escena")]
    public int NewScene;
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
        SceneManager.LoadScene(NewScene);
    }
    public void EndGame()
    {
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
