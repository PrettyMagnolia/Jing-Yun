using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//�л�������
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class MainMenuGUI : MonoBehaviour
{
    public static int mode = 0;//ģʽ��1Ϊ����ģʽ��2Ϊ����ģʽ
    public AudioClip beep;
    static private int w = 400;
    static private int h = 60;
    static private int bw = 800;
    static private int bh = 600;
    //private AudioSource audio;
    public GUISkin menuSkin;
    public Rect menuArea;
    private Rect playButton = new Rect(Screen.width / 2 - w / 2, Screen.height / 2 - h - 80, w, h);
    private Rect instructionsButton = new Rect(Screen.width / 2 - w / 2, Screen.height / 2 - h, w, h);
    private Rect quitButton = new Rect(Screen.width / 2 - w / 2, Screen.height / 2 - h + 80, w, h);
    public Rect instructions;
    private Rect box = new Rect(Screen.width / 2 - bw / 2, Screen.height / 2 - bh / 2, bw, bh);
    private Rect backButton = new Rect(Screen.width / 2 - w / 2, Screen.height/2 + bh / 2-2*h, w, h);

    Rect menuAreaNormalized;
    string menuPage = "main";
    // Use this for initialization

    void Start()
    {
        menuPage = "main";
        Initialize();
        //audio = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        menuPage = "main";
        //menuAreaNormalized =
           //new Rect(menuArea.x * Screen.width - (menuArea.width * 0.5f), menuArea.y * Screen.height - (menuArea.height * 0.5f), menuArea.width, menuArea.height);
    }
    void OnGUI()
    {
        GUI.skin = menuSkin;
        //GUI.BeginGroup(menuAreaNormalized);

        //���˵�
        if (menuPage == "main")
        {
            
            if (GUI.Button(new Rect(playButton), "��ʼ"))
            {           
                //audio.PlayOneShot(beep);
                menuPage = "��ʼ";
            }
            if (GUI.Button(new Rect(instructionsButton), "˵��"))
            {
                //audio.PlayOneShot(beep);
                menuPage = "˵��";
            }
            if (GUI.Button(new Rect(quitButton), "�˳�"))
            {
                StartCoroutine("ButtonAction", "quit");
                QuitApplication();
            }
        }

        //instruction�˵�
        else if (menuPage == "˵��")
        {
            GUI.Box(new Rect(box), "��Ϸ��ɫ����Ϸ�ĳ���Ϊ���ﴫͳϷ���Ļ������Σ������������ܿ�Ԫ��������Ϸ�Ŀ����ԡ���Ϸ����Ϊ���侩����\nĿ�������𼧡���\n\n" +
                                              "����ģʽ���ƶ����ת���ӽǣ�������������ڹ������е�2�֣�����������е�1�֣���ջ�����������1�֡���\nZ����ͣ��\n\n" +
                                              "����ģʽ����Q��E�������ң��Ե�������1�֡����������䵽���˵�10�֣�ײ��������������Z����ͣ��\n\n");
            //GUI.Label(new Rect(instructions), "��Ϸ��ɫ����Ϸ�ĳ���Ϊ���ﴫͳϷ���Ļ������Σ������������ܿ�Ԫ��������Ϸ�Ŀ����ԡ���Ϸ����Ϊ���侩����Ŀ�������𼧡���\n" +
            //                                  "����ģʽ���ƶ����ת���ӽǣ�������������ڹ������е�2�֣�����������е�1�֣���յ���1�֡���Z����ͣ��\n" +
            //                                  "����ģʽ����Q��E�������ң��Ե�������1�֡���Z����ͣ��");
            if (GUI.Button(new Rect(backButton), "����"))
            {
                //audio.PlayOneShot(beep);
                menuPage = "main";
            }
        }

        //��ʼ�˵�
        else if (menuPage == "��ʼ")
        {
            if(GUI.Button(new Rect(playButton),"����ģʽ"))
            {
                //audio.PlayOneShot(beep);
                Classical_Human();//�ű�����������
                Cursor.visible = false;
                StartCoroutine("ButtonAction", "scene");
            }
            if(GUI.Button(new Rect(instructionsButton),"����ģʽ"))
            {
                //audio.PlayOneShot(beep);
                Create.ModeTwoStart = true;//��ʼ��������Ϣ
                RunMode();
                StartCoroutine("ButtonAction", "Demo_Scene");
            }
            if (GUI.Button(new Rect(quitButton), "����"))
            {
                //audio.PlayOneShot(beep);
                menuPage = "main";
            }
        }

        //����ģʽ�˵�
        //else if(menuPage == "����ģʽ")
        //{
        //    if(GUI.Button(new Rect(playButton),"��������"))
        //    {
        //        //audio.PlayOneShot(beep);
        //        Classical_Human();//�ű�����������
        //        Cursor.visible = false;
        //        StartCoroutine("ButtonAction", "scene");
        //    }
        //    if(GUI.Button(new Rect(instructionsButton),"�˻��й�"))
        //    {
        //        //audio.PlayOneShot(beep);
        //        Classical_AI();
        //        Cursor.visible = false;
        //        StartCoroutine("ButtonAction", "scene");
        //    }
        //    if (GUI.Button(new Rect(quitButton), "����"))
        //    {
        //        //audio.PlayOneShot(beep);
        //        menuPage = "��ʼ";
        //    }
        //}
        //GUI.EndGroup();
    }
    IEnumerator ButtonAction(string levelName)
    {
        //audio.PlayOneShot(beep);
        yield return new WaitForSeconds(0.35f);

        if (levelName != "quit")
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Application.Quit();
            Debug.Log("Have Quit");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void QuitApplication()//�˳���Ϸ
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    private void Initialize()
    {
        
        /*�رյ�һ�������Ķ���*/
        //����AudioListener����һ������
        //GameObject.Find("StartCamera").GetComponent<AudioListener>().enabled = true;
        //GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;

        // SceneManager.LoadScene("StartMenu");//���ó�ʼ����   

        //�ر�׼����ʾ
        //GameObject.Find("Aimer").SetActive(false);

        //��ʼ�׶ν�����Ϸ�ű�
        //GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = false;//���ű�
        //GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = false;
        //GameObject.Find("Controller").GetComponent<NotesControl>().enabled = false;//���������Ľű�
        //GameObject.Find("Controller").GetComponent<ScoreTextScript>().enabled = false;//��ʼ�׶ιرռƷְ�
        //GameObject.Find("Canvas").SetActive(false);

        ///*�رյڶ��������Ķ���*/
        //GameObject.Find("Archer_1").SetActive(false);
        //GameObject.Find("CameraGroup").SetActive(false);
        //GameObject.Find("Directional Light").SetActive(false);
        //GameObject.Find("GameObject").SetActive(false);
        //GameObject.Find("EventSystem").SetActive(false);

    }
    private void Classical_Human()
    {
        //ģʽ����
        mode = 1;
        //GameObject.Find("ModeSet").SetActive(true);
        ////��StartGame�������������ű�����֪��Ϸ��ʼ
        //GameObject.Find("Controller").SetActive(true);
        //NotesControl.StartGame = true;

        ////�л�����
        //SceneManager.LoadScene("scene");

        //����AudioListener����һ������
        //GameObject.Find("StartCamera").GetComponent<AudioListener>().enabled = false;
        //GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = true;



        //GameObject.Find("Main Camera").SetActive(true);
       
        //GameObject.Find("Canvas").SetActive(true);

    }
    private void Classical_AI()
    {
        //ģʽ����
        mode = 2;
        //GameObject.Find("ModeSet").SetActive(true);
        ////��StartGame�������������ű�����֪��Ϸ��ʼ
        //GameObject.Find("Controller").SetActive(true);
        //NotesControl.StartGame = true;

        //����AI���е��Զ���������
        Face.AIGaming = true;

        ////�л�����
        //SceneManager.LoadScene("scene");
        //GameObject.Find("StartCamera").GetComponent<AudioListener>().enabled = false;
        //GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = true;



        //GameObject.Find("Main Camera").SetActive(true);

        //GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = false;
        //GameObject.Find("Canvas").SetActive(false);




    }
    private void RunMode()
    {
        mode = 3;
        Cursor.visible = false;
    }


}
