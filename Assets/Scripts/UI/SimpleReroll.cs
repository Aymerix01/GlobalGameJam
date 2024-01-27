using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleReroll : MonoBehaviour
{
    [SerializeField] private Image RerollBackground;
    private void OnTriggerEnter2D(Collider2D other)
    {
        RerollBackground.gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        RerollBackground.gameObject.SetActive(false);
    }
}
