using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateNbr : MonoBehaviour
{
    private PlayerUnit player;
    [SerializeField] private TMP_Text Health;
    [SerializeField] private TMP_Text AP;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerUnit>();
    }

    private void Update()
    {
        Health.text = player.currentHP+"";
        AP.text = player.currrentAP + "";
    }
}
