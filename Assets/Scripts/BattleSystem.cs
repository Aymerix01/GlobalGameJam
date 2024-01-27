using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.PlasticSCM.Editor.WebApi;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerInstance;
    public GameObject enemyInstance;

    public Transform playerPosition;
    public Transform enemyPosition;

    PlayerUnit playerUnit;
    EnemyUnit enemyUnit;

    public playerBattleHUD playerHUD;
    public enemyBattleHUD enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerObject = Instantiate(playerInstance, playerPosition);
        playerUnit = playerObject.GetComponent<PlayerUnit>();

        GameObject enemyObject = Instantiate(enemyInstance, enemyPosition);
        enemyUnit = enemyObject.GetComponent<EnemyUnit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAction()
    {
        //Effet de carte

        playerHUD.SetHP(playerUnit.currentHP);
        enemyHUD.SetHP(enemyUnit.currentHP);

        playerUnit.TakeDoT();
        enemyUnit.TakeDoT();

        playerHUD.SetHP(playerUnit.currentHP);
        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(1);

        if (state == BattleState.WON)
        {
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyAction());
        }
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

    IEnumerator EnemyAction()
    {
        //Action de l'ennemi
        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1);

        if (state == BattleState.LOST)
        {
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerAction();
        }
    }

    public void PlayerTurn()
    {

    }

    public void Damage(Unit u,int damage)
    {
        u.currentHP -= damage - u.defense;
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
        playerUnit.currentHP += value;
    }
}
