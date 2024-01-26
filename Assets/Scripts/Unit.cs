using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public string UnitName;
    [SerializeField] public int maxHP;
    public int currentHP;
    public string nextMove;
    [SerializeField] public int damage;
}
