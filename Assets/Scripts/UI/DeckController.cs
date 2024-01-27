using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class DeckController : MonoBehaviour
{
    [SerializeField] private GameObject[] Cards = new GameObject[12];
    [SerializeField] private Transform[] CardPos = new Transform[4];
    [SerializeField] private GameObject CardBack;

    private DragDrop dragDrop;
    private GameObject CardBackSpawned;
    private List<DragDrop> CardsInHand;
    private List<DragDrop> CardsInDeck;
    private void Awake()
    {

        CardsInHand = new List<DragDrop>();
        CardsInDeck = new List<DragDrop>();
        int i = 0;
        foreach (GameObject card in Cards)
        {
            if (card != null)
            {
                CardsInDeck.Add(Instantiate(card).GetComponent<DragDrop>());
                CardsInDeck[i].transform.position = transform.position;
            }
            i++;
        }
        foreach (Transform cardPos in CardPos)
        {
            int randomCard = Random.Range(0, CardsInDeck.Count);
            CardsInDeck[randomCard].transform.position = cardPos.position;
            CardsInDeck[randomCard].IsDraggable = true;
            CardsInHand.Add(CardsInDeck[randomCard]);
            CardsInDeck.RemoveAt(randomCard);
        }
        CardBackSpawned = Instantiate(CardBack, transform);
    }

    public void UpdateCardPlayable(Vector3 pos)
    {
        if (CardsInDeck.Count > 0)
        {
            int randomCard = Random.Range(0, CardsInDeck.Count);
            CardsInDeck[randomCard].transform.position = pos;
            CardsInDeck[randomCard].InitialPosition = pos;
            CardsInDeck[randomCard].IsDraggable = true;
            CardsInHand.Add(CardsInDeck[randomCard]);
            CardsInDeck.RemoveAt(randomCard);
        }
        else
        {
            Destroy(CardBackSpawned);
        }
    }

    public void RemoveCardInHand(DragDrop dd)
    {
        CardsInHand.Remove(dd);
    }

    public void RerollOneCard(DragDrop dd, Vector3 pos)
    {
        Debug.Log("Reroll");
        RemoveCardInHand(dd);
        CardsInDeck.Add(dd);
        CardsInDeck[CardsInDeck.Count-1].transform.position = transform.position;
        CardsInDeck[CardsInDeck.Count - 1].IsDraggable = false;
        UpdateCardPlayable(pos);
    }

    public void RerollAllCards()
    {
        int i = 0;
        CardsInHand.RemoveAll(x => !x);
        if (CardsInHand.Count > 0)
        {
            Debug.Log(CardsInHand.Count);
            foreach (DragDrop card in CardsInHand.ToArray())
            {
                if (i < 4)
                {
                    RerollOneCard(card, CardPos[i].transform.position);
                }
                i++;

            }
        }
    }
}
