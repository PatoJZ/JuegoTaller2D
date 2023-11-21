using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [Header("Teclas")]
    public GameObject KeyW;
    private Animator animatorKeyW;
    public GameObject KeyA;
    private Animator animatorKeyA;
    public GameObject KeyD;
    private Animator animatorKeyD;
    public GameObject KeyS;
    private Animator animatorKeyS;

    public GameObject KeyUP;
    private Animator animatorKeyUp;
    public GameObject KeyLeft;
    private Animator animatorKeyLeft;
    public GameObject KeyRight;
    private Animator animatorKeyRight;
    public GameObject KeyDown;
    private Animator animatorKeyDown;

    public GameObject KeySpace;
    private Animator animatorKeySpace;

    public GameObject KeyM;
    private Animator animatorKeyM;
    public GameObject KeyN;
    private Animator animatorKeyN;
    public GameObject KeyE;
    private Animator animatorKeyE;

    [Header("Fondo")]
    
    public GameObject backGround;

    [Header("Hud")]

    public GameObject hudWeapon;
    private Animator hudWeaponAnimator;

    [Header("Player")]
    public GameObject player;
    private Animator playerAnimator;
    public GameObject playerAttack;
    private Animator playerAttackAnimation;
    public GameObject changeWeapon;
    private Animator changeWeaponAnimator;
    
    public GameObject playerSeeSign;
    private Animator playerSeeSignAnimator;
    public GameObject CheckGood;
    public GameObject CheckBad;
    float x, y;
    public float Ax=0, Ay=(-1);
    // Start is called before the first frame update
    void Start()
    {
        //************Teclas*********************
        animatorKeyA = KeyA.GetComponent<Animator>();
        animatorKeyD = KeyD.GetComponent<Animator>();
        animatorKeyS = KeyS.GetComponent<Animator>();
        animatorKeyW = KeyW.GetComponent<Animator>();
        animatorKeyUp = KeyUP.GetComponent<Animator>();
        animatorKeyDown = KeyDown.GetComponent<Animator>();
        animatorKeyLeft = KeyLeft.GetComponent<Animator>();
        animatorKeyRight = KeyRight.GetComponent<Animator>();
        animatorKeyN = KeyN.GetComponent<Animator>();
        animatorKeyM = KeyM.GetComponent<Animator>();
        animatorKeySpace = KeySpace.GetComponent<Animator>();
        animatorKeyE = KeyE.GetComponent<Animator>();
        animatorKeyA.SetTrigger("A");
        animatorKeyD.SetTrigger("D");
        animatorKeyS.SetTrigger("S");
        animatorKeyW.SetTrigger("W");
        animatorKeyN.SetTrigger("N");
        animatorKeyM.SetTrigger("M");
        animatorKeyUp.SetTrigger("Up");
        animatorKeyDown.SetTrigger("Down");
        animatorKeyLeft.SetTrigger("Left");
        animatorKeyRight.SetTrigger("Right");
        animatorKeySpace.SetTrigger("Space");
        animatorKeyE.SetTrigger("E");
        //****************HUD****************
        hudWeaponAnimator = hudWeapon.GetComponent<Animator>();       
        //***************Player****************
        playerAnimator = player.GetComponent<Animator>();
        StartCoroutine(Down());
        playerAttackAnimation = playerAttack.GetComponent<Animator>();
        playerAttackAnimation.SetTrigger("Attack");
        changeWeaponAnimator = changeWeapon.GetComponent<Animator>();
        StartCoroutine(HoeToShovel());
        playerSeeSignAnimator = playerSeeSign.GetComponent<Animator>();
        StartCoroutine(SeeSignDown());
        
        
    }
    public IEnumerator Space()
    {
        animatorKeySpace.SetBool("Hold",true);
        yield return new WaitForSeconds(0.1f);
        animatorKeySpace.SetBool("Hold", false);
    }
    public IEnumerator E()
    {
        animatorKeyE.SetBool("Hold", true);
        yield return new WaitForSeconds(0.1f);
        animatorKeyE.SetBool("Hold", false);
    }

    public IEnumerator N()
    {
        animatorKeyN.SetBool("Hold", true);
        yield return new WaitForSeconds(0.1f);
        animatorKeyN.SetBool("Hold", false);
    }
    public IEnumerator PressEBad()
    {
        
        yield return new WaitForSeconds(1f);
        CheckBad.SetActive(true);
        StartCoroutine(E());
    }
    public IEnumerator PressEGood()
    {

        yield return new WaitForSeconds(1f);
        CheckGood.SetActive(true);
        StartCoroutine(E());
    }
    public IEnumerator SeeSignDown()
    {
        playerSeeSignAnimator.SetTrigger("Down");
        StartCoroutine(PressEBad());
        yield return new WaitForSeconds(2f);
        CheckBad.SetActive(false);
        StartCoroutine(SeeSignRight());
    }
    public IEnumerator SeeSignRight()
    {
        playerSeeSignAnimator.SetTrigger("IdleHoe");
        StartCoroutine(PressEBad());
        yield return new WaitForSeconds(2f);
        CheckBad.SetActive(false);
        StartCoroutine(SeeSignUp());
    }
    public IEnumerator SeeSignUp()
    {
        playerSeeSignAnimator.SetTrigger("Up");
        StartCoroutine(PressEGood());
        yield return new WaitForSeconds(2f);
        CheckGood.SetActive(false);
        StartCoroutine(SeeSignLeft());
    }
    public IEnumerator SeeSignLeft()
    {
        playerSeeSignAnimator.SetTrigger("Left");
        StartCoroutine(PressEBad());
        yield return new WaitForSeconds(2f);
        CheckBad.SetActive(false);
        StartCoroutine(SeeSignDown());
    }
    public IEnumerator M()
    {
        animatorKeyM.SetBool("Hold", true);
        yield return new WaitForSeconds(0.1f);
        animatorKeyM.SetBool("Hold", false);
    }
    public IEnumerator HoeToShovel()
    {
        changeWeaponAnimator.SetTrigger("IdleShovel");
        hudWeaponAnimator.SetTrigger("HoeToShovel");
        StartCoroutine(M());
        yield return new WaitForSeconds(1);
        StartCoroutine(ShovelToTools());    
    }
    public IEnumerator ShovelToTools()
    {
        changeWeaponAnimator.SetTrigger("IdleTools");
        hudWeaponAnimator.SetTrigger("ShovelToTools");
        StartCoroutine(M());
        yield return new WaitForSeconds(1);

        StartCoroutine(ToolsToHoe());
    }
    public IEnumerator ToolsToHoe()
    {
        changeWeaponAnimator.SetTrigger("IdleHoe");
        hudWeaponAnimator.SetTrigger("ToolsToHoe");
        StartCoroutine(M());
        yield return new WaitForSeconds(1);

        StartCoroutine(HoeToTools());
    }
    public IEnumerator HoeToTools()
    {
        changeWeaponAnimator.SetTrigger("IdleTools");
        hudWeaponAnimator.SetTrigger("HoeToTools");
        StartCoroutine(N());
        yield return new WaitForSeconds(1);

        StartCoroutine(ToolsToShovel());
    }
    public IEnumerator ToolsToShovel()
    {
        changeWeaponAnimator.SetTrigger("IdleShovel");
        hudWeaponAnimator.SetTrigger("ToolsToShovel");
        StartCoroutine(N());
        yield return new WaitForSeconds(1);

        StartCoroutine(ShovelToHoe());
    }
    public IEnumerator ShovelToHoe()
    {
        changeWeaponAnimator.SetTrigger("IdleHoe");
        hudWeaponAnimator.SetTrigger("ShovelToHoe");
        StartCoroutine(N());
        yield return new WaitForSeconds(1);

        StartCoroutine(HoeToShovel());
    }
    public IEnumerator Down()
    {
        x = 0;
        y = -1;
        yield return new WaitForSeconds(2);
        StartCoroutine(DownRight());
    }
    public IEnumerator DownLeft()
    {
        x = -1;
        y = -1;
        yield return new WaitForSeconds(2);
        StartCoroutine(Down());
    }
    public IEnumerator DownRight()
    {
        x = 1;
        y = -1;
        yield return new WaitForSeconds(2);
        StartCoroutine(Right());
    }
    public IEnumerator Left()
    {
        x = -1;
        y = 0;
        yield return new WaitForSeconds(2);
        StartCoroutine(DownLeft());
    }
    public IEnumerator Right()
    {
        x = 1;
        y = 0;
        yield return new WaitForSeconds(2);
        StartCoroutine(UpRight());
    }
    public IEnumerator Up()
    {
        x = 0;
        y = 1;
        yield return new WaitForSeconds(2);
        StartCoroutine(UpLeft());
    }
    public IEnumerator UpLeft()
    {
        x = -1;
        y = 1;
        yield return new WaitForSeconds(2);
        StartCoroutine(Left());
    }
    public IEnumerator UpRight()
    {
        x = 1;
        y = 1;
        yield return new WaitForSeconds(2);
        StartCoroutine(Up());
    }
    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetFloat("Horizontal",x);
        playerAnimator.SetFloat("Vertical", y);
        
        playerAttackAnimation.SetFloat("Horizontal", Ax);
        playerAttackAnimation.SetFloat("Vertical", Ay);
       

        switch (y)
        {
            case -1:
                animatorKeyS.SetBool("Hold",true);
                animatorKeyDown.SetBool("Hold", true);
                break;
            case 0:
                animatorKeyS.SetBool("Hold", false);
                animatorKeyW.SetBool("Hold", false);
                animatorKeyDown.SetBool("Hold", false);
                animatorKeyUp.SetBool("Hold", false);
                break;
            case 1:
                animatorKeyW.SetBool("Hold", true);
                animatorKeyUp.SetBool("Hold", true);
                break;
        }
        switch (x)
        {
            case -1:
                animatorKeyA.SetBool("Hold", true);
                animatorKeyLeft.SetBool("Hold", true);
                break;
            case 0:
                animatorKeyA.SetBool("Hold", false);
                animatorKeyD.SetBool("Hold", false);
                animatorKeyLeft.SetBool("Hold", false);
                animatorKeyRight.SetBool("Hold", false);
                break;
            case 1:
                animatorKeyD.SetBool("Hold", true);
                animatorKeyRight.SetBool("Hold", true);
                break;
        }
        
        backGround.transform.position = new Vector3(backGround.transform.position.x-(x)*Time.deltaTime, backGround.transform.position.y-(y )*Time.deltaTime, backGround.transform.position.z);

    }
}
