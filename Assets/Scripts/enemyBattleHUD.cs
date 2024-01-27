using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyBattleHUD : MonoBehaviour
{
    public Text nameText;
    public Slider hpSlider;

    public void SetHUD(EnemyUnit unit) //Initialisation du HUD
    {
        nameText.text = unit.UnitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
