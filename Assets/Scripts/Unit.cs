using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public int maxHP;
    public int currentHP;
    public int activedot;
    public int defense;
    public int atkbuff;

    public void TakeDoT()
    {
        if (activedot > 0)
        {
            this.currentHP -= activedot;
            activedot--;
        }
    }
}
