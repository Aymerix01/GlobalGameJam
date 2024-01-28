using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] public float maxHP;
    public float currentHP;
    public float activedot;
    [SerializeField] public float dotdamage;
    public float defense;
    public float atkbuff;
    [SerializeField] public SpriteRenderer effect;

    public void TakeDoT()
    {
        if (activedot > 0)
        {
            currentHP -= activedot;
            activedot--;
        }
    }

    private void Update()
    {
        if (activedot >= 1)
        {
            effect.enabled = true ;
        }
        else
        {
            effect.enabled = false;
        }
    }
}
