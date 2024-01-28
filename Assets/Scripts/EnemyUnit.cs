using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyUnit : Unit
{
    [SerializeField] public string UnitName;
    public enum nextMove { Stunned, Attack, Defense, AetD };
    public nextMove attaque;
    [SerializeField] float probaatt;
    [SerializeField] float probadef;
    [SerializeField] float probaAeD;
    [SerializeField] public float damage;
    [SerializeField] public string[] craint;
    [SerializeField] public string[] resiste;

    public void NextMove()
    {
        int roll = new System.Random().Next(100);

        if (roll <= probaatt * 100)
        {
            attaque = nextMove.Attack;
        }
        else if (roll > probaatt * 100 && roll < (probaatt + probadef) * 100)
        {
            attaque = nextMove.Defense;
        }
        else
        {
            attaque = nextMove.AetD;
        }
    }  
}
