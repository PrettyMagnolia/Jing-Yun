using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoPause : MonoBehaviour
{
    //the ButtonPauseMenu
    public GameObject ingameMenu;


    public Text txt_score2;//��ʾ�������ı�
    private int pause_score;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnPause();
        }
     
    }
    public void OnPause()//��Z��ͣ
    {
        if (MainMenuGUI.mode == 1 || MainMenuGUI.mode == 2)
        {
            GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = false;
            GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = false;
            //GameObject.Find("Aimer").SetActive(false);
        }
        else if (MainMenuGUI.mode == 3)
        {
            //��ͣʱ������������
            CharacterControllerSimple.CanAttack = false;
        }
        Time.timeScale = 0;       
        ingameMenu.SetActive(true);
        Cursor.visible = true;//��ʾ���
        if (MainMenuGUI.mode == 3) 
        {
            txt_score2 = GameObject.Find("CurrentScoreText").GetComponent<Text>();

            pause_score = ScoreTextScript2.score2;
            txt_score2.text = "������" + pause_score.ToString();
        }
    }

    public void OnResume()//�����������Ϸ��ʱִ�д˷���
    {
        Cursor.visible = false;//�������
        if (MainMenuGUI.mode == 1 || MainMenuGUI.mode == 2) 
        {
            GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = true;
            GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = true;
            //GameObject.Find("Aimer").SetActive(true);
        }
        else if (MainMenuGUI.mode == 3)
        {
            CharacterControllerSimple.CanAttack = true;
            Create.ModeTwoStart = true;//��ʼ��������Ϣ
        }
        Time.timeScale = 1f;       
        ingameMenu.SetActive(false);
            
    }

    public void OnGoMain()//��������ز˵���ʱִ�д˷���
    {
        if(MainMenuGUI.mode == 2)
            CharacterControllerSimple.CanAttack = true;
        Cursor.visible = true;//��ʾ���
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }
}
