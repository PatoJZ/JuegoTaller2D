using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudArma : MonoBehaviour
{
    private Animator animatorWeapon;
    private void Start()
    {
        animatorWeapon = GetComponent<Animator>();
    }
    public void Hoe()
    {
        animatorWeapon.SetTrigger("Hoe");
    }
    public void Shovel()
    {
        animatorWeapon.SetTrigger("Shovel");
    }
    public void Tools()
    {
        animatorWeapon.SetTrigger("Tools");
    }
}
