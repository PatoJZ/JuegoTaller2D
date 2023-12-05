using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : BasicInteraction
{
    public string[] dialog;
    public string[] dialogEndMisision;
    public string[] dialogAfter;
    public string npcName;
    public Sprite npcFace;
    public string itemNeed;
    public string[] itemsNeed;
    public string itemGive;
    public Sprite imageItemGive;
    private bool getItem=false;
    int dialogCounter;
    ControllerHUD controllerHUD;
    NpcRandomPatrol npcRandomPatrol;
    PlayerAttack playerAttack;

    // Start is called before the first frame update
    
    void Start()
    {
        npcRandomPatrol = GetComponent<NpcRandomPatrol>();
        controllerHUD = FindObjectOfType<ControllerHUD>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }
    public override bool Interact(Vector2 playerFacing,Vector2 playerPos)
    {
        getItem = GetItem();
        
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
        if (!getItem)
        {
            if (dialogCounter == dialog.Length)
            {
                EndDialog();
            }
            else
            {
                controllerHUD.NpcShowText(dialog[dialogCounter], npcName, npcFace);
                dialogCounter++;
            }
        }
        else
        {
            if (dialogCounter == dialogEndMisision.Length)
            {
                EndDialog();
            }
            else
            {
                controllerHUD.NpcShowText(dialogEndMisision[dialogCounter], npcName, npcFace);
                dialogCounter++;
            }
        }
    }
    private void EndDialog()
    {
        if (getItem)
        {
            for (int i=0; i<dialogEndMisision.Length;i++)
            {
                dialogEndMisision[i] = dialogAfter[i];
            }
        }
        controllerHUD.NpcHideText();
        dialogCounter = 0;
    }
    private bool GetItem()
    {
        return playerAttack.isItem(itemsNeed,itemGive,imageItemGive);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PJ"))
        {
            collision.GetComponent<PlayerControl>().basicInteraction = this;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PJ"))
        {
            collision.GetComponent<PlayerControl>().basicInteraction = null;
        }
    }
}
