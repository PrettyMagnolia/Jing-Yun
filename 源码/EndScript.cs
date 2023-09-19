using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           //导入资源库
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public static bool JumpToEnd = false;//由NotesControl传入

    public Vector3 EndPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoWhenEnd();
    }
    private void DoWhenEnd()//游戏结束时切换场景
    {
        if (JumpToEnd)
        {
            EndPos = GameObject.Find("EndFlag").GetComponent<Transform>().position;
            GameObject.Find("EndFlag").GetComponent<Face>().enabled = true;//"结束标记"运动
            if (EndPos.y <= 10f)
            {
                FinalMenuGUI.DisplayEndMenu = true;
                SceneManager.LoadScene("EndMenu");
            }
        }
    }
}
