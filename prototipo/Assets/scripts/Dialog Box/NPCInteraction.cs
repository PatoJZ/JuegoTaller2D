using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : BasicInteraction
{
    public string[] dialog;
    public string npcName;
    public Sprite npcFace;
    int dialogCounter;
    ControllerHUD controllerHUD;
    NpcRandomPatrol npcRandomPatrol;

    // Start is called before the first frame update
    
    void Start()
    {
        npcRandomPatrol = GetComponent<NpcRandomPatrol>();
        controllerHUD = FindObjectOfType<ControllerHUD>();
    }
    public override bool Interact(Vector2 playerFacing,Vector2 playerPos)
    {
        bool success = FacingNpc(playerFacing,playerPos,transform.position);
        if (success)
        {
            npcRandomPatrol.FacePlayer(playerFacing);
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
        if (dialogCounter == dialog.Length)
        {
            EndDialog();
        }
        else
        {
            controllerHUD.NpcShowText(dialog[dialogCounter],npcName,npcFace);
            dialogCounter++;
        }
    }
    private void EndDialog()
    {
        controllerHUD.NpcHideText();
        dialogCounter = 0;
    }
}
