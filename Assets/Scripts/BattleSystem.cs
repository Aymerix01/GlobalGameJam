using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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

        if (c.PlayerEffect == Card.EffectType.BuffAttack)
        {
            Buff(3);
        }
        else if (c.PlayerEffect == Card.EffectType.BuffDefence)
        {
            Block(playerUnit, 4);
        }

        if (Array.IndexOf(enemyUnit.craint, c.name) > -1)
        {

            Damage(enemyUnit, c.PlayerDamage + playerUnit.atkbuff*f(c.PlayerDamage));
            HealPlayer(c.PlayerHeal);

            if (c.PlayerEffect == Card.EffectType.DOT)
            {
                DoT(enemyUnit, playerUnit.dotdamage);
            }
        } 
        else if (Array.IndexOf(enemyUnit.resiste, c.name) > -1)
        {
            Damage(playerUnit, c.EnemyDamage);
            HealFoe(c.EnemyHeal);
            if (c.EnemyEffect == Card.EffectType.DOT)
            {
                DoT(playerUnit, enemyUnit.dotdamage);
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
        playerUnit.TakeDoT();
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
        enemyUnit.TakeDoT();
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
        float mitigateddamage = Mathf.Max(damage - u.defense, 0);
        u.currentHP = Mathf.Max( u.currentHP - mitigateddamage,0);
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

    public void DoT(Unit u,float value)
    {
        u.activedot += value;
    }

    public void Block(Unit u, float value)
    {
        u.defense = value;
    }

    public void HealPlayer(float value)
    {
        playerUnit.currentHP = Mathf.Min(playerUnit.currentHP + value,playerUnit.maxHP);
        HealthBarre.fillAmount = playerUnit.currentHP / playerUnit.maxHP;
    }

    public void HealFoe(float value)
    {
        enemyUnit.currentHP = Mathf.Min(enemyUnit.currentHP + value, enemyUnit.maxHP);
        EnemyBarre.fillAmount = enemyUnit.currentHP / enemyUnit.maxHP;
    }

    public void Buff(float value)
    {
        playerUnit.atkbuff = value;
    }

    float f(float value)
    {
        if(value == 0)
        {
            return 0f;
        }
        else
        {
            return 1f;
        }
    }
}
