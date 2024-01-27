using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    private Vector3 mousePositionOffset;
    private Camera camera;
    public Vector3 InitialPosition;
    public bool IsDraggable;
    private bool HasPlayThisCard;
    private bool HasReroll;
    private DeckController deckController;
    private GameObject BattleSystem;

    private void Start()
    {
        camera = Camera.main;
        deckController = GameObject.Find("PosDeck").GetComponent<DeckController>();
        InitialPosition = transform.position;
        HasPlayThisCard = false;
        BattleSystem = GameObject.Find("BattleSystem");
    }

    private Vector3 GetMouseWorldposition()
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void ReturnToInitialPosition()
    {
        transform.position = InitialPosition;
    }

    private void OnMouseDown()
    {
        mousePositionOffset = transform.position - GetMouseWorldposition();
    }

    private void OnMouseDrag()
    {
        if (IsDraggable)
        {
            transform.position = GetMouseWorldposition() + mousePositionOffset;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DropZone")
        {
            HasPlayThisCard = true;
        }
        else if (other.gameObject.tag == "Reroll")
        {
            HasReroll = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "DropZone")
        {
            HasPlayThisCard = false;
        }
        else if (other.gameObject.tag == "Reroll")
        {
            HasReroll = false;
        }
    }

    private void OnMouseUp()
    {
        if (IsDraggable)
        {
            if (HasPlayThisCard)
            {
                deckController.UpdateCardPlayable(InitialPosition);
                BattleSystem.GetComponent<BattleSystem>().Turn(GetComponent<Card>());
                Debug.Log("Here to start effect of this card : "+gameObject.name);
                Destroy(gameObject);
                deckController.RemoveCardInHand(this);
            }
            else if (HasReroll)
            {
                deckController.RerollOneCard(this, InitialPosition);
            }
            else
            {
                ReturnToInitialPosition();
            }
        }
    }
}
