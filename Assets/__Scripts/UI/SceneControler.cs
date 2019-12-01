using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler : MonoBehaviour
{
   public void LoadLevel(int levelNum)
    {
      
        SceneManager.LoadScene("Level" + levelNum);
    }


    public void LoadMainMenu()
    {
        
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("MainGameMenu");
    }
}
