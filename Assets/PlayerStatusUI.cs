using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : SingletonMonoBehavior<PlayerStatusUI>
{
    Image hpGuage;
    void Start()
    {
        hpGuage = transform.Find("HPGauge").GetComponent<Image>();
    }

    internal void UpdateHP(int hp, int maxHp)
    {
        hpGuage.fillAmount = hp / maxHp;
    }
}
