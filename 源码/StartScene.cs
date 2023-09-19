using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    //自动加载，无需调用
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        string startSceneName = "StartMenu";
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals(startSceneName))
        {
            return;
        }
        SceneManager.LoadScene(startSceneName);
    }


}