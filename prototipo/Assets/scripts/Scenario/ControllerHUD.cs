using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class ControllerHUD : MonoBehaviour
{
    public TMP_Text textPoint;
    public TMP_Text textHealth;

    public GameObject dialogBox;
    public TMP_Text dialogText;

    public GameObject npcDialogBox;
    public TMP_Text npcDialogText;
    public TMP_Text npcName;
    public Image npcFace;

    public Image hudWeapon;
    private Animator hudWeaponAnimator;
    // Start is called before the first frame update
    private void Start()
    {
        hudWeaponAnimator = hudWeapon.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //Cambiar texto
        if (textPoint!=null)
        {
            textPoint.text = "Puntaje total=" + (ControllerSave.instance.point);
        }
        if (textHealth!=null)
        {
            Debug.Log("Existe");
            textHealth.text = "Vidas=" + (ControllerSave.instance.life);
        }
    }
    public void ShowText(string text)
    {
        dialogBox.SetActive(true);
        dialogText.text = text;
        Time.timeScale = 0;
    }
    public void HideText()
    {
        dialogBox.SetActive(false);
        dialogText.text = "";
        Time.timeScale = 1;
    }
    public void NpcShowText(string text,string name,Sprite image)
    {
        npcDialogBox.SetActive(true);
        npcDialogText.text = text;
        npcName.text = name;
        npcFace.sprite = image;
        Time.timeScale = 0;
    }
    public void NpcHideText()
    {
        npcDialogBox.SetActive(false);
        npcDialogText.text = "";
        npcName.text = "";
        npcFace.sprite = null;
        Time.timeScale = 1;
    }
    public void ChangeWeapon(PlayerAttack playerAttack)
    {
        switch (playerAttack.weapon)
        {
            case PlayerAttack.Directions.HOE:
                if (Input.GetKeyDown("n"))
                {
                    hudWeaponAnimator.SetTrigger("HoeToTools");
                }
                else if (Input.GetKeyDown("m"))
                {
                    hudWeaponAnimator.SetTrigger("HoeToShovel");
                }
                break;
            case PlayerAttack.Directions.SHOVEL:
                if (Input.GetKeyDown("n"))
                {
                    hudWeaponAnimator.SetTrigger("ShovelToHoe");
                }
                else if (Input.GetKeyDown("m"))
                {
                    hudWeaponAnimator.SetTrigger("ShovelToTools");
                }
                break;
            case PlayerAttack.Directions.TOOLS:
                if (Input.GetKeyDown("n"))
                {
                    hudWeaponAnimator.SetTrigger("ToolsToShovel");
                }
                else if (Input.GetKeyDown("m"))
                {
                    hudWeaponAnimator.SetTrigger("ToolsToHoe");
                }
                break;
        }
    }
    
}
