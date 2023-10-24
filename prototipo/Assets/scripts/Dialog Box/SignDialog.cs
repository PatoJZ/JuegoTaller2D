using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDialog : BasicInteraction
{
    public string[] dialog;
    int dialogCounter;
    ControllerHUD controllerHUD;
    
    // Start is called before the first frame update
    void Start()
    {
        controllerHUD = FindObjectOfType<ControllerHUD>();      
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
}
