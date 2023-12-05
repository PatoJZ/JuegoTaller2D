using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abrir : MonoBehaviour
{
    public float pointNecesary;
    public string key;
    public bool needKey;
    public GameObject locket;
    public GameObject keyE;
    private Animator keyEAnimator;
    private void Start()
    {
        keyEAnimator = keyE.GetComponent<Animator>();
        keyEAnimator.SetTrigger("E");
        if (needKey)
        {
            locket.SetActive(true);
        }
    }
    private IEnumerator PressKey()
    {
        keyEAnimator.SetBool("Hold", true);
        yield return new WaitForSeconds(1);
        StartCoroutine(UnPressKey());
    }
    private IEnumerator UnPressKey()
    {
        keyEAnimator.SetBool("Hold", false);
        yield return new WaitForSeconds(1);
        StartCoroutine(PressKey());
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PJ"))
        {
            keyE.SetActive(true);
            keyEAnimator.SetTrigger("E");
            StartCoroutine(UnPressKey());
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        
        if (collision.gameObject.CompareTag("PJ"))
        {
            if (Input.GetKey("e"))
            {
                if (needKey)
                {
                    if (collision.gameObject.GetComponent<PlayerAttack>().HaveItem(key))
                    {
                        Destroy(locket);
                    }
                    if (ControllerSave.instance.point >= pointNecesary && collision.gameObject.GetComponent<PlayerAttack>().HaveItem(key))
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    if (ControllerSave.instance.point >= pointNecesary)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PJ"))
        {
            StopAllCoroutines();
            keyE.SetActive(false);
            
        }
    }
}

