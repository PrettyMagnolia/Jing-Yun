using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoPause : MonoBehaviour
{
    //the ButtonPauseMenu
    public GameObject ingameMenu;


    public Text txt_score2;//显示分数的文本
    private int pause_score;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnPause();
        }
     
    }
    public void OnPause()//按Z暂停
    {
        if (MainMenuGUI.mode == 1 || MainMenuGUI.mode == 2)
        {
            GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = false;
            GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = false;
            //GameObject.Find("Aimer").SetActive(false);
        }
        else if (MainMenuGUI.mode == 3)
        {
            //暂停时按左键不能射箭
            CharacterControllerSimple.CanAttack = false;
        }
        Time.timeScale = 0;       
        ingameMenu.SetActive(true);
        Cursor.visible = true;//显示鼠标
        if (MainMenuGUI.mode == 3) 
        {
            txt_score2 = GameObject.Find("CurrentScoreText").GetComponent<Text>();

            pause_score = ScoreTextScript2.score2;
            txt_score2.text = "分数：" + pause_score.ToString();
        }
    }

    public void OnResume()//点击“继续游戏”时执行此方法
    {
        Cursor.visible = false;//隐藏鼠标
        if (MainMenuGUI.mode == 1 || MainMenuGUI.mode == 2) 
        {
            GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = true;
            GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = true;
            //GameObject.Find("Aimer").SetActive(true);
        }
        else if (MainMenuGUI.mode == 3)
        {
            CharacterControllerSimple.CanAttack = true;
            Create.ModeTwoStart = true;//开始存人物信息
        }
        Time.timeScale = 1f;       
        ingameMenu.SetActive(false);
            
    }

    public void OnGoMain()//点击“返回菜单”时执行此方法
    {
        if(MainMenuGUI.mode == 2)
            CharacterControllerSimple.CanAttack = true;
        Cursor.visible = true;//显示鼠标
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }
}
