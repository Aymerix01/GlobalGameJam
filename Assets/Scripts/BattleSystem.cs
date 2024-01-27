using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public IEnumerator Turn(Card c)
    {
        Debug.Log("Card played");
        playerUnit.TakeDoT();
        enemyUnit.TakeDoT();

        APbarre.fillAmount = (playerUnit.currrentAP - c.pA) / playerUnit.maxAP;

        //Effet de carte
        Damage(enemyUnit, c.PlayerDamage);
        Damage(playerUnit,c.EnemyDamage);
        Heal(c.PlayerHeal);
        switch (c.PlayerEffect)
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

        }
        HealthBarre.fillAmount = playerUnit.currentHP / playerUnit.maxHP;
        EnemyBarre.fillAmount = enemyUnit.currentHP / enemyUnit.maxHP;

        yield return new WaitForSeconds(1);

        if (state == BattleState.WON)
        {
            EndBattle();
        }
        else
        {
            state = BattleState.TURN;
        }

        //Action de l'ennemi
        switch (enemyUnit.attaque)
        {
            case EnemyUnit.nextMove.Attack:
                Damage(playerUnit, enemyUnit.damage); break;
            case EnemyUnit.nextMove.Defense:
                Block(enemyUnit, 4); break;
            case EnemyUnit.nextMove.AetD:
                Damage(playerUnit, enemyUnit.damage - 3);
                Block(enemyUnit,2); break;
            case EnemyUnit.nextMove.Stunned:
                break;

        }
        HealthBarre.fillAmount = playerUnit.currentHP / playerUnit.maxHP;
        enemyUnit.NextMove();


        yield return new WaitForSeconds(1);

        if (state == BattleState.LOST)
        {
            EndBattle();
        }
        else
        {
            state = BattleState.TURN;
        }

        yield return new WaitForSeconds(1);

        HealthBarre.fillAmount = playerUnit.currentHP / playerUnit.maxHP;
        EnemyBarre.fillAmount = enemyUnit.currentHP / enemyUnit.maxHP;
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            //Go to next fight
        }
        else if (state == BattleState.LOST)
        {
            //Go to Game Over
        }
    }


    public void Damage(Unit u,int damage)
    {
        u.currentHP = Mathf.Max( u.currentHP- damage + u.defense + u.atkbuff,0);
        u.defense = 0;
        u.atkbuff = 0;
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

    public void DoT(Unit u,int value)
    {
        u.activedot = value;
    }

    public void Block(Unit u, int value)
    {
        u.defense = value;
    }

    public void Heal(int value)
    {
        playerUnit.currentHP = Mathf.Min(playerUnit.currentHP + value,playerUnit.maxHP);
    }

    public void Buff(int value)
    {
        playerUnit.atkbuff = value;
    }
}
