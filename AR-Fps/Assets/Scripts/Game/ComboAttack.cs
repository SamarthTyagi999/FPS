using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;

    [Header("Stats")]
    public float comboRate;
    public int noOfCombos;
    float lastComboTime;
    int comboStep;



    public void PerformAttack()
    {
        if (comboStep == 0)
        {
            comboStep++;
            PerformCombo();
        }
        else
        {
            CanCombo();
        }   

     }

    void CanCombo()
    {
        if (Time.time - lastComboTime <= comboRate)
        {
            comboStep++;
            PerformCombo();
        }
        else
        {
            comboStep = 0;
            PerformAttack();
        }
    }

    void PerformCombo()
    {
        if (comboStep == 1)
        {
            anim.Play("Sword_Attack01");
        }
        else if (comboStep == 2)
        {
            anim.Play("Sword_Attack02");
        }
        else if (comboStep == 3)
        {
            anim.Play("Sword_Attack03");
        }
        if (comboStep >= noOfCombos)
        {
            comboStep = 0;
        }

        lastComboTime = Time.time;
    }

}
