using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Tutorial tuto;
    // Start is called before the first frame update
    void Start()
    {
        tuto = FindObjectOfType<Tutorial>();
    }

    public void PressSpace()
    {
        tuto.StartCoroutine(tuto.Space());
    }
    public void AttackDownLeft()
    {
        tuto.Ax = -1;
        tuto.Ay = -1;
    }
    public void AttackDown()
    {
        tuto.Ax = 0;
        tuto.Ay = -1;
    }
    public void AttackDownRight()
    {
        tuto.Ax = 1;
        tuto.Ay = -1;
    }
    public void AttackLeft()
    {
        tuto.Ax = -1;
        tuto.Ay = 0;
    }
    public void AttackRight()
    {
        tuto.Ax = 1;
        tuto.Ay = 0;
    }

    public void AttackUpLeft()
    {
        tuto.Ax = -1;
        tuto.Ay = 1;
    }
    public void AttackUp()
    {
        tuto.Ax = 0;
        tuto.Ay = 1;
    }
    public void AttackUpRight()
    {
        tuto.Ax = 1;
        tuto.Ay = 1;
    }
}
