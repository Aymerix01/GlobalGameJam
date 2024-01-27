using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    private BattleSystem bs;

    [Header("Battle Stats")]
    [Range(0,10)]
    public int pA;

    enum CardType { Attack, Utility }
    [SerializeField] CardType type;

    public enum EffectType { None, DOT, BuffDefence, BuffAttack, Stun, Dodge, Schuffle, GainPA }

    [Header("Sucess")]
    [Tooltip("Damage dealt by the player")]
    public int PlayerDamage;

    [Tooltip("Value healed by the player")]
    public int PlayerHeal;

    [Tooltip("Effect applied to the enemy")]
    public EffectType PlayerEffect;


    [Header("Fail")]
    [Tooltip("Damage dealt to the player")]
    public int EnemyDamage;

    [Tooltip("Value healed by the Enemy")]
    public int EnemyHeal;

    [Tooltip("Effect applied to the player")]
    
    public EffectType EnemyEffect;


    [Space (10)]
    [Header("------------------")]
    [Space(10)]


    [Header("Body")]
    [SerializeField] private Sprite cardImage;


    [TextArea]
    [Header("Description")]
    [SerializeField] private string joke;

    [TextArea]
    [SerializeField] private string sucess;

    [TextArea]
    [SerializeField] private string fail;

    [SerializeField] private AudioClip audioClip;

    
    // Start is called before the first frame update
    void Start()
    {
        ReplaceEverything();
    }

    void ReplaceEverything()
    {
        //Top
        //PA
        transform.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = pA + " PA";
        //Type
        if (type == CardType.Attack)
        {
            //top right sprite
            transform.GetChild(3).GetChild(0).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Attack");
            //background top
            transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6588235f, 0.1994776f, 0.1199059f, 1f);
            //background middle
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.909434f, 0.6994405f, 0.5473763f, 1f);
            //background bottom
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8039216f, 0.3721133f, 0.2784314f, 1f);
        }
        if (type == CardType.Utility)
        {
            //top right sprite
            transform.GetChild(3).GetChild(0).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Utility");
            //background top
            transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.300383f, 0.6588235f, 0.1215686f, 1f);
            //background middle
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7683073f, 0.9098039f, 0.5490196f, 1f);
            //background bottom
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6163785f, 0.8037735f, 0.2775293f, 1f);
        }

        //Image
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = cardImage;

        //Description
        //Joke
        transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = joke;
        //Sucess
        transform.GetChild(2).GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Rire : " + sucess;
        //Fail
        transform.GetChild(2).GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Bad : " + fail;
    }

}
