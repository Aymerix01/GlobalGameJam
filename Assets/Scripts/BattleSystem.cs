using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public enum BattleState { START, TURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{  
    public BattleState state;

    [SerializeField] private GameObject playerInstance;
    [SerializeField] private GameObject enemyInstance;

    PlayerUnit playerUnit;
    EnemyUnit enemyUnit;

    [SerializeField] private Image APbarre;
    [SerializeField] private Image HealthBarre;
    [SerializeField] private Image EnemyBarre;

    [SerializeField] private GameObject WIN;
    [SerializeField] private GameObject Lose;

    [SerializeField] private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerUnit = playerInstance.GetComponent<PlayerUnit>();
        enemyUnit = enemyInstance.GetComponent<EnemyUnit>();

        enemyUnit.NextMove();

        yield return new WaitForSeconds(1f);

        state = BattleState.TURN;
    }

    public void Turn(Card c)
    {
        playerUnit.TakeDoT();
        enemyUnit.TakeDoT();
        if (c.audioClip != null)
        {
            audioManager.PlayClip(c.audioClip);
        } 
        else if (playerUnit.audioClip.ContainsKey(c.name))
        {
            audioManager.PlayClip(playerUnit.audioClip[c.name]);
        }
        APbarre.fillAmount = (playerUnit.currrentAP - c.pA) / playerUnit.maxAP;
        playerUnit.currrentAP -= c.pA;
        if (Array.IndexOf(enemyUnit.craint, c.name) > -1)
        {
            Damage(enemyUnit, c.PlayerDamage);
            Heal(c.PlayerHeal);
            if (c.PlayerEffect == Card.EffectType.DOT)
            {
                DoT(enemyUnit);
            }
        } 
        else if (Array.IndexOf(enemyUnit.resiste, c.name) > -1)
        {
            Damage(playerUnit, c.EnemyDamage);
            if (c.PlayerEffect == Card.EffectType.DOT)
            {
                DoT(playerUnit);
            }
        }
        else
        {
            Debug.Log("Nothing");
        }

        //Effet de carte
        /*switch (c.PlayerEffect)
        {
            case Card.EffectType.DOT:
                DoT(enemyUnit, 4);
                break;

            case Card.EffectType.BuffDefence:
                Block(playerUnit, 5);
                break;

            case Card.EffectType.BuffAttack:
                Buff(8);
                break;
            case Card.EffectType.Stun:
                enemyUnit.attaque = EnemyUnit.nextMove.Stunned;
                break;

        }*/
        HealthBarre.fillAmount = playerUnit.currentHP / playerUnit.maxHP;
        EnemyBarre.fillAmount = enemyUnit.currentHP / enemyUnit.maxHP;

        if (playerUnit.currentHP<=0)
        {
            Lose.SetActive(true);
            Debug.Log("LOST");
        }
        else if (enemyUnit.currentHP<=0)
        {
            WIN.SetActive(true);
            Debug.Log("WIN");
        }
    }

    public void TourEnemy()
    {
        if (enemyUnit.audioClip != null)
        {
            audioManager.PlayClip(enemyUnit.audioClip);
        }
        switch (enemyUnit.attaque)
        {
            case EnemyUnit.nextMove.Attack:
                Damage(playerUnit, enemyUnit.damage); break;
            case EnemyUnit.nextMove.Defense:
                Block(enemyUnit, 4); break;
            case EnemyUnit.nextMove.AetD:
                Damage(playerUnit, enemyUnit.damage - 3);
                Block(enemyUnit, 2); break;
            case EnemyUnit.nextMove.Stunned:
                break;

        }
        HealthBarre.fillAmount = playerUnit.currentHP / playerUnit.maxHP;
        enemyUnit.NextMove();

        if (playerUnit.currentHP <= 0)
        {
            Lose.SetActive(true);
            Debug.Log("LOST");
        }
        else if (enemyUnit.currentHP <= 0)
        {
            WIN.SetActive(true);
            Debug.Log("WIN");
        }

        HealthBarre.fillAmount = playerUnit.currentHP / playerUnit.maxHP;
        EnemyBarre.fillAmount = enemyUnit.currentHP / enemyUnit.maxHP;

        playerUnit.currrentAP = playerUnit.maxAP;
        playerUnit.UpdateAP();
    }

    public void Damage(Unit u,float damage)
    {
        u.currentHP = Mathf.Max( u.currentHP- damage + u.defense + u.atkbuff,0);
        u.defense = 0;
        if (u.currentHP<=0 )
        {
            Destroy(u);
            if (u is EnemyUnit)
            {
                state = BattleState.WON;
            }

            else if (u is PlayerUnit)
            {
                state = BattleState.LOST;
            }
        }
    }

    public void DoT(Unit u)
    {
        u.activedot = 3;
    }

    public void Block(Unit u, float value)
    {
        u.defense = value;
    }

    public void Heal(int value)
    {
        playerUnit.currentHP = Mathf.Min(playerUnit.currentHP + value,playerUnit.maxHP);
        HealthBarre.fillAmount = playerUnit.currentHP / playerUnit.maxHP;
    }

    public void Buff(int value)
    {
        playerUnit.atkbuff = value;
    }
}
