using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetourMeni : MonoBehaviour
{
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
}