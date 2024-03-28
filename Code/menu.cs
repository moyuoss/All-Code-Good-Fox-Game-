using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void start_game()
    {
        SceneManager.LoadScene(1);
    }
    public void main_memu()
    {
        
    }
    public void exit_game()
    {
        SceneManager.LoadScene(0);
    }
}
