using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : Unit
{
    [SerializeField] public float maxAP;
    public float currrentAP;

    [SerializeField] private Image APbarre;
    [SerializeField] private Image HealthBarre;

    public Dictionary<string, AudioClip> audioClip;

    private void Start()
    {
        audioClip = new Dictionary<string, AudioClip>()
            {
             {"Musicien(Clone)", Resources.Load<AudioClip>("Musicien")},
             {"Jardinier(Clone)", Resources.Load<AudioClip>("Jardinier")},
             {"Messi(Clone)", Resources.Load<AudioClip>("Messi")},
             {"Fromage(Clone)", Resources.Load<AudioClip>("Fromage")},
             {"Mitraillette(Clone)", Resources.Load<AudioClip>("Mitraillette")},
             {"Cheval(Clone)", Resources.Load<AudioClip>("Cheval")},
             {"Nain(Clone)", Resources.Load<AudioClip>("Nain")}
            };
        Debug.Log(audioClip["Musicien(Clone)"]);
    }
    public void UpdateAP()
    {
        APbarre.fillAmount = currrentAP/ maxAP;
    }
}

