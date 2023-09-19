using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//切换场景用
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class MainMenuGUI : MonoBehaviour
{
    public static int mode = 0;//模式：1为经典模式，2为漫游模式
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

        //主菜单
        if (menuPage == "main")
        {
            
            if (GUI.Button(new Rect(playButton), "开始"))
            {           
                //audio.PlayOneShot(beep);
                menuPage = "开始";
            }
            if (GUI.Button(new Rect(instructionsButton), "说明"))
            {
                //audio.PlayOneShot(beep);
                menuPage = "说明";
            }
            if (GUI.Button(new Rect(quitButton), "退出"))
            {
                StartCoroutine("ButtonAction", "quit");
                QuitApplication();
            }
        }

        //instruction菜单
        else if (menuPage == "说明")
        {
            GUI.Box(new Rect(box), "游戏特色：游戏的初衷为宣扬传统戏剧文化的音游，并结合射击与跑酷元素增加游戏的可玩性。游戏音乐为经典京剧曲\n目“霸王别姬”。\n\n" +
                                              "经典模式：移动鼠标转动视角，鼠标左键射击。在光区击中得2分，光区以外击中得1分，射空或错过音符倒扣1分。按\nZ键暂停。\n\n" +
                                              "漫游模式：按Q和E控制左右，吃到音符得1分。左键射击，射到敌人得10分，撞到敌人死亡。按Z键暂停。\n\n");
            //GUI.Label(new Rect(instructions), "游戏特色：游戏的初衷为宣扬传统戏剧文化的音游，并结合射击与跑酷元素增加游戏的可玩性。游戏音乐为经典京剧曲目“霸王别姬”。\n" +
            //                                  "经典模式：移动鼠标转动视角，鼠标左键射击。在光区击中得2分，光区以外击中得1分，射空倒扣1分。按Z键暂停。\n" +
            //                                  "漫游模式：按Q和E控制左右，吃到音符得1分。按Z键暂停。");
            if (GUI.Button(new Rect(backButton), "返回"))
            {
                //audio.PlayOneShot(beep);
                menuPage = "main";
            }
        }

        //开始菜单
        else if (menuPage == "开始")
        {
            if(GUI.Button(new Rect(playButton),"经典模式"))
            {
                //audio.PlayOneShot(beep);
                Classical_Human();//脚本与物体启用
                Cursor.visible = false;
                StartCoroutine("ButtonAction", "scene");
            }
            if(GUI.Button(new Rect(instructionsButton),"漫游模式"))
            {
                //audio.PlayOneShot(beep);
                Create.ModeTwoStart = true;//开始存人物信息
                RunMode();
                StartCoroutine("ButtonAction", "Demo_Scene");
            }
            if (GUI.Button(new Rect(quitButton), "返回"))
            {
                //audio.PlayOneShot(beep);
                menuPage = "main";
            }
        }

        //经典模式菜单
        //else if(menuPage == "经典模式")
        //{
        //    if(GUI.Button(new Rect(playButton),"单机游玩"))
        //    {
        //        //audio.PlayOneShot(beep);
        //        Classical_Human();//脚本与物体启用
        //        Cursor.visible = false;
        //        StartCoroutine("ButtonAction", "scene");
        //    }
        //    if(GUI.Button(new Rect(instructionsButton),"人机托管"))
        //    {
        //        //audio.PlayOneShot(beep);
        //        Classical_AI();
        //        Cursor.visible = false;
        //        StartCoroutine("ButtonAction", "scene");
        //    }
        //    if (GUI.Button(new Rect(quitButton), "返回"))
        //    {
        //        //audio.PlayOneShot(beep);
        //        menuPage = "开始";
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
    private void QuitApplication()//退出游戏
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    private void Initialize()
    {
        
        /*关闭第一个场景的东西*/
        //设置AudioListener仅有一个开启
        //GameObject.Find("StartCamera").GetComponent<AudioListener>().enabled = true;
        //GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;

        // SceneManager.LoadScene("StartMenu");//设置初始场景   

        //关闭准心显示
        //GameObject.Find("Aimer").SetActive(false);

        //初始阶段禁用游戏脚本
        //GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = false;//鼠标脚本
        //GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = false;
        //GameObject.Find("Controller").GetComponent<NotesControl>().enabled = false;//控制音符的脚本
        //GameObject.Find("Controller").GetComponent<ScoreTextScript>().enabled = false;//初始阶段关闭计分板
        //GameObject.Find("Canvas").SetActive(false);

        ///*关闭第二个场景的东西*/
        //GameObject.Find("Archer_1").SetActive(false);
        //GameObject.Find("CameraGroup").SetActive(false);
        //GameObject.Find("Directional Light").SetActive(false);
        //GameObject.Find("GameObject").SetActive(false);
        //GameObject.Find("EventSystem").SetActive(false);

    }
    private void Classical_Human()
    {
        //模式设置
        mode = 1;
        //GameObject.Find("ModeSet").SetActive(true);
        ////把StartGame变量传入其他脚本，告知游戏开始
        //GameObject.Find("Controller").SetActive(true);
        //NotesControl.StartGame = true;

        ////切换场景
        //SceneManager.LoadScene("scene");

        //设置AudioListener仅有一个开启
        //GameObject.Find("StartCamera").GetComponent<AudioListener>().enabled = false;
        //GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = true;



        //GameObject.Find("Main Camera").SetActive(true);
       
        //GameObject.Find("Canvas").SetActive(true);

    }
    private void Classical_AI()
    {
        //模式设置
        mode = 2;
        //GameObject.Find("ModeSet").SetActive(true);
        ////把StartGame变量传入其他脚本，告知游戏开始
        //GameObject.Find("Controller").SetActive(true);
        //NotesControl.StartGame = true;

        //启动AI特有的自动消除功能
        Face.AIGaming = true;

        ////切换场景
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
