using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    private Vector3 mousePositionOffset;
    private Camera camera;
    public Vector3 InitialPosition;
    private Vector3 InitialScale;
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
        InitialScale = transform.localScale;
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

    private void OnMouseEnter()
    {
        if (IsDraggable)
        {
            transform.localScale = transform.localScale * 2;
            transform.position -= new Vector3(0, InitialPosition.y / 2, 0);
        }
    }
    private void OnMouseExit()
    {
        if (IsDraggable)
        {
            transform.localScale = InitialScale;
            transform.position = InitialPosition;
        }
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
                StartCoroutine(BattleSystem.GetComponent<BattleSystem>().Turn(GetComponent<Card>()));
                Destroy(gameObject);
                deckController.RemoveCardInHand(this);
            }
            else if (HasReroll)
            {
                transform.localScale = InitialScale;
                transform.position = InitialPosition;
                deckController.RerollOneCard(this, InitialPosition);
            }
            else
            {
                ReturnToInitialPosition();
            }
        }
    }
}
