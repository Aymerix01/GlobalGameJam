using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public float maxHP;
    public float currentHP;
    public float activedot;
    public float defense;
    public float atkbuff;

    public void TakeDoT()
    {
        if (activedot > 0)
        {
            currentHP -= activedot;
            activedot--;
        }
    }
}
