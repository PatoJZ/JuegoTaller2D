using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ControllerHUD : MonoBehaviour
{
    [Header("Heart")]
    public Image[] playerHearts;
    public Sprite[] heartStatus;
    public int currentHeart;
    [SerializeField] private int hp;


    static int minhearts = 1;
    static int maxhearts = 4;


    public TMP_Text textPoint;

    [Header("Power up")]
    public GameObject Velocity;
    public Slider VelocityTimer;
    private bool timeActivateVelocity = false;
    private float timerVelocity;

    public GameObject force;
    public Slider forceTimer;
    private bool timeActivateForce = false;
    private float timerForce;

    public GameObject attackVelocity;
    public Slider attackVelocityTimer;
    private bool timeActivateAttackVelocity = false;
    private float timerAttackVelocity;

    [Header("Pause")]
    public GameObject Pause;
    public GameObject controles;
    public GameObject opciones;

    [Header("Dialog Box")]
    public GameObject dialogBox;
    public TMP_Text dialogText;

    [Header("Dialog Box Npc")]
    public GameObject npcDialogBox;
    public TMP_Text npcDialogText;
    public TMP_Text npcName;
    public Image npcFace;

    [Header("Weapon Change")]
    public Image hudWeapon;
    private Animator hudWeaponAnimator;

    [Header("Item")]
    
    public Image[] numOfItems;
    public Sprite imageDefault;
    
    [Header("roundManager")]
    
    public GameObject barraRondas;
    public Slider sliderBarra;
    public Animator anima;
    public GameObject[] roundsManager;
    public int[] zone ;
    public int indice=0;
    public float enemyPoints;
    public Image actualNum;
    public Image numAfter;
    public Sprite[] numeros;
    private int rondaActual;
    private int totalrondas;
    public float sumPoints;
    private bool inicio;
    
    // Start is called before the first frame update
    private void Start()
    {
        hudWeaponAnimator = hudWeapon.GetComponent<Animator>();
        DefineRonda();
        ResetBarra();
    }
    // Update is called once per frame
    void Update()
    {
        //corazones
        currentHeart = Mathf.Clamp(currentHeart, minhearts, maxhearts);
        hp = Mathf.Clamp(Mathf.RoundToInt(ControllerSave.instance.life), 0, currentHeart * 4);
        UpdateCurrentHearts();
        //tiempos de powerup
        if (timeActivateVelocity)
        {
            ChangeCountVelocity();
        }
        if (timeActivateForce)
        {
            ChangeCountForce();
        }
        if (timeActivateAttackVelocity)
        {
            ChangeCountAttackVelocity();
        }
        //pausa
        if (Input.GetKeyDown(KeyCode.Escape) && !Pause.activeSelf && !dialogBox.activeSelf && !npcDialogBox.activeSelf)
        {
            Time.timeScale = 0;
            Pause.SetActive(true);

        } else if (Input.GetKeyDown(KeyCode.Escape) && !dialogBox.activeSelf && !npcDialogBox.activeSelf &&!controles.activeSelf && !opciones.activeSelf)
        {
            Time.timeScale = 1;
            Pause.SetActive(false);
        }
        //visor de las rondas
        if (roundsManager[indice]!=null||(enemyPoints!= sumPoints || rondaActual != totalrondas))
        {
            barraRondas.SetActive(true);
            BarraRonas();
            
        }

        else
        {
            barraRondas.SetActive(false);
        }

    }
    // botones menu
    public void Continue()
    {
        Time.timeScale = 1;
        Pause.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Abrir()
    {
        controles.SetActive(true);
    }
    public void Cerrar()
    {
        controles.SetActive(false);
    }
    public void Abrir2()
    {
        opciones.SetActive(true);
    }
    public void Cerrar2()
    {
        opciones.SetActive(false);
    }
    //corazones

    private void UpdateCurrentHearts()
    {
        int aux = hp;
        for (int i =0;i<maxhearts;i++)
        {
            if (i<currentHeart)
            {
                playerHearts[i].enabled = true;
                playerHearts[i].sprite = GetHeartStatus(aux);
                aux -= 4;
            }
            else
            {
                
                playerHearts[i].enabled = false;
            }
        }
    }
    public void UpdateCurrentHP(int i)
    {
        hp += i;
        hp = Mathf.Clamp(hp, 0, currentHeart * 4);
        UpdateCurrentHearts();
    }
    private Sprite GetHeartStatus(int x)
    {
        switch (x)
        {
            case >=4: return heartStatus[4];
            case 3: return heartStatus[3];
            case 2: return heartStatus[2];
            case 1: return heartStatus[1];
            default: return heartStatus[0];
        }
    }
    //power ups
    public void ActivateVelocity(float maxTime)
    {
        Velocity.SetActive(true);
        timeActivateVelocity = true;
        VelocityTimer.maxValue = maxTime;
        timerVelocity = maxTime;
    }
    private void ChangeCountVelocity()
    {
        timerVelocity -= Time.deltaTime;
        if (timerVelocity>=0)
        {
            VelocityTimer.value = timerVelocity;
        }
        else
        {
            timeActivateVelocity = false;
            Velocity.SetActive(false);
            
        }
    }
    public void ActivateAttackVelocity(float maxTime)
    {
        attackVelocity.SetActive(true);
        timeActivateAttackVelocity = true;
        attackVelocityTimer.maxValue = maxTime;
        timerAttackVelocity = maxTime;
    }
    private void ChangeCountAttackVelocity()
    {
        timerAttackVelocity -= Time.deltaTime;
        if (timerAttackVelocity >= 0)
        {
            attackVelocityTimer.value = timerAttackVelocity;
        }
        else
        {
            timeActivateAttackVelocity = false;
            attackVelocity.SetActive(false);

        }
    }
    public void ActivateForce(float maxTime)
    {
        force.SetActive(true);
        timeActivateForce = true;
        forceTimer.maxValue = maxTime;
        timerForce = maxTime;
    }
    private void ChangeCountForce()
    {
        timerForce -= Time.deltaTime;
        if (timerForce >= 0)
        {
            forceTimer.value = timerForce;
        }
        else
        {
            timeActivateForce = false;
            force.SetActive(false);

        }
    }
    //dialog box cartel y npc
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
    //Cambiar arma
    public void ChangeWeapon(PlayerAttack playerAttack)
    {
        switch (playerAttack.weapon)
        {
            case PlayerAttack.Directions.HOE:
                if (Input.GetKeyDown("n"))
                {
                    if(playerAttack.weaponBlock)
                        hudWeaponAnimator.SetTrigger("HoeToShovelB");
                    else
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
                    if (playerAttack.weaponBlock)
                        hudWeaponAnimator.SetTrigger("ShovelToHoeB");
                    else
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
    public void UpdateItem(List<Sprite> imageItem)
    {
        for (int i = 0; i < numOfItems.Length; i++)
        {
                
            if (imageItem.Count > 0 && i < imageItem.Count)
                numOfItems[i].sprite = imageItem[i];
            else
                numOfItems[i].sprite = imageDefault;
                
        }
        
    }
    // sistemas de rondas
    public void DeleteRoundManagers()
    {
        int i=0;

        foreach(GameObject a in roundsManager)
        {
            if (i==indice&& a!=null)
            {
                roundsManager[i]=null;
                a.GetComponent<RoundManager>().RemoverSpawner();
            }
            
            i++;
        }
    }
    public void DefineRonda()
    {
        if (roundsManager[indice] != null)
        {
            sliderBarra.maxValue = roundsManager[indice].GetComponent<RoundManager>().enemigosPorRonda;
            enemyPoints = 0;
            if (!inicio)
            {
                actualNum.sprite = numeros[0];
                numAfter.sprite = numeros[1];
                inicio = true;
            }
            else
            {
                actualNum.sprite = numeros[roundsManager[indice].GetComponent<RoundManager>().rondaActual - 1];
                numAfter.sprite = numeros[roundsManager[indice].GetComponent<RoundManager>().rondaActual];
            }
        }
        
    }
    public void ResetBarra()
    {
        rondaActual=0;
        totalrondas= roundsManager[indice].GetComponent<RoundManager>().rondaFinal;
    }
    public void BarraRonas()
    {
        
        if (sumPoints< enemyPoints&& !Pause.activeSelf)
        {
            sumPoints += Time.unscaledDeltaTime;
            anima.SetBool("move",true);
        }
        else
        {
            if (!Pause.activeSelf)
            {
                anima.SetBool("move", false);
                sumPoints = enemyPoints;
            }
        }
        sliderBarra.value = sumPoints;
        if (sliderBarra.value == sliderBarra.maxValue&& rondaActual!=totalrondas) 
        {
            DefineRonda();
            rondaActual++;
        }
    }

}
