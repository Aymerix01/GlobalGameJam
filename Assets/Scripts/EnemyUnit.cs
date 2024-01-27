using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    [SerializeField] public string UnitName;
    public string nextMove;
    [SerializeField] public int damage;
    [SerializeField] public GameObject[] craint;
    [SerializeField] public GameObject[] resiste;
}
