using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void NextScene()
    {
        Debug.Log("cc");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
