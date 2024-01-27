using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBattleHUD : MonoBehaviour
{
    public Slider hpSlider;
    public Slider apSlider;

    public void SetHUD(PlayerUnit unit) //Initialisation du HUD 
    {
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.maxHP;
        apSlider.maxValue = unit.maxAP;
        apSlider.value = unit.maxAP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetAP(int ap)
    {
        apSlider.value = ap;
    }
}
