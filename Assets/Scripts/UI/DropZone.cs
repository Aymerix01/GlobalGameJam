using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        spriteRenderer.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        spriteRenderer.enabled = false;
    }
}
