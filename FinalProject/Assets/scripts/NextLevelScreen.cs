using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NextLevelScreen : MonoBehaviour
{


    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void ExitButton(){
        SceneManager.LoadScene("Level2");
    }
}
