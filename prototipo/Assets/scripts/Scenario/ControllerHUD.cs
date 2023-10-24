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
    // Start is called before the first frame update

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
}
