using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    public void playGame()
    {
       
        SceneManager.LoadScene("sc_Justin_SampleScene");
    }

    public void instructions()
    {
        SceneManager.LoadScene("sc_Owen_instructionsScene");
    }
    public void back()
    {
        SceneManager.LoadScene("sc_Owen_startScreen");
    }
    public static void WinScreen()
    {
        SceneManager.LoadScene("sc_EndingScene");
    }
}
