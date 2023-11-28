using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDialog : BasicInteraction
{
    public string[] dialog;
    int dialogCounter;
    ControllerHUD controllerHUD;
    public GameObject keyE;
    private Animator keyEAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        controllerHUD = FindObjectOfType<ControllerHUD>();
        keyEAnimator = keyE.GetComponent<Animator>();
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

    public override bool Interact(Vector2 playerFacing,Vector2 playerPos)
    {
        bool success = FacingObject(playerFacing);
        if (success)
        {
            NextDialog();
        }
        else
        {
            EndDialog();
        }
        
        return success;
    }
    private void NextDialog()
    {
        if (dialogCounter==dialog.Length)
        {
            EndDialog();
        }
        else
        {
            controllerHUD.ShowText(dialog[dialogCounter]);
            dialogCounter++;
        }
    }
    private void EndDialog()
    {
        controllerHUD.HideText();
        dialogCounter = 0;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PJ"))
        {
            keyE.SetActive(true);
            StartCoroutine(UnPressKey());
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PJ"))
        {
            StopAllCoroutines();
            keyE.SetActive(false);
        }
    }
}
