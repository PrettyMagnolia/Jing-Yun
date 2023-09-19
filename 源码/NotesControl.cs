using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;
using System;

public class NotesControl : MonoBehaviour
{
    private string FileName = "/musicAll.text";//存储音符的文件名

    float speed = Face.MoveSpeed;
    public static bool DoEnding = false;

    int ite = 1;//指针，表明现在演奏到了第几个音符

    private float timer = 0;//计时变量
    public float MusicSpeed;
    private float BeatTime;//一拍的时长

    private bool[] NoteOn = new bool[1000];//0置空，代表第i个音还未被演奏
    private bool[] TimerOn = new bool[1000];//代表第i个计时器开启


    private string[] NoteType = new string[1000];
    private string[] TimeType = new string[1000];
    private float[] WaitTime = new float[1000];

    public GameObject NotePosition;//音符生成位置设置了一个对象
    public GameObject note;//音符种类对象 

    public static bool StartGame = false;//判断是否开始游戏

    private float four;
    private float four_;
    private float eight;
    private float eight_;
    private float sixteen;
    // Start is called before the first frame update
    void Start()
    {
        ite = 1;
        GameObject.Find("EndFlag").GetComponent<Face>().enabled = false;//初始不让结束标记运动
        
       

        MusicSpeed = 30.0f;
        BeatTime = 60.0f / MusicSpeed;

        four = BeatTime * 1.0f / 2.0f;
        four_ = BeatTime * 3.0f / 4.0f;
        eight = BeatTime * 1.0f / 4.0f;
        eight_ = BeatTime * 3.0f / 8.0f;
        sixteen = BeatTime * 1.0f / 8.0f;


        for (int i = 0; i < NoteOn.Length; i++)
            NoteOn[i] = false;
        NoteOn[1] = true;//开启第一个音符的演奏权限
        for (int i = 0; i < TimerOn.Length; i++)
            TimerOn[i] = false;

        for (int i = 0; i < 1000; i++)
        {
            NoteType[i] = "0";
            TimeType[i] = "0";
            WaitTime[i] = 0;
        }
        //文件读写
        LoadMusic();//从外部文件中读入乐谱

        //给WaitTime[]赋值
        for (int i = 1; i < TimeType.Length; i++)
        {
            if (TimeType[i] == "four")
                WaitTime[i] = four;
            else if (TimeType[i] == "four_")
                WaitTime[i] = four_;
            else if (TimeType[i] == "eight")
                WaitTime[i] = eight;
            else if (TimeType[i] == "eight_")
                WaitTime[i] = eight_;
            else if (TimeType[i] == "sixteen")
                WaitTime[i] = sixteen;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        PlayMusic();
        if (MusicIsOver())
            EndScript.JumpToEnd = true;//开始在EndScript脚本内做结束操作


    }
    private void OnEnable()
    {
        ite = 1;
        DoEnding = false;
    }
    private void Timer(float waitTime, int NumTimer)
    {
        if (TimerOn[NumTimer])
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                timer = 0;
                TimerOn[NumTimer] = false;//计时器关闭
                NoteOn[NumTimer + 1] = true;//开启下一个音符
                ite++;
            }
        }

    }
    private bool Timer(float waitTime)
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            timer = 0;
            return true;
        }
        return false;
    }
    private void SetNote(string _NoteTypeStr)//设置音符音高
    {
        string _NotePositionStr = _NoteTypeStr + "BornPos";
        note = GameObject.Find(_NoteTypeStr);
        NotePosition = GameObject.Find(_NotePositionStr);
    }
    private void NewNote()
    {
        GameObject.Instantiate(note, NotePosition.transform.position, NotePosition.transform.rotation);
    }
    private void MakeNote(string NoteType, int NumNote)
    {

        if (NoteOn[NumNote])
        {
            SetNote(NoteType);
            NewNote();
            NoteOn[NumNote] = false;//关闭该音符演奏
            TimerOn[NumNote] = true;//开启该音符计时器
        }

    }

    private void PlayNote(string NoteType, float waitTime, int num)
    {
        if (NoteType != "0")
        {
            MakeNote(NoteType, num);
            Timer(waitTime, num);
        }
    }
    private void PlayMusic()
    {
        PlayNote(NoteType[ite], WaitTime[ite], ite);
    }

    private bool MusicIsOver()
    {
        return NoteType[ite] == "0";
    }
    public void LoadMusic()//装载乐谱
    {
        int i = 1;
        XmlDocument xml = new XmlDocument();
        xml.Load(Application.streamingAssetsPath + FileName);
        XmlNodeList list = xml.SelectSingleNode("Save").ChildNodes;
        foreach (XmlElement xe in list)
        {
            NoteType[i] = xe.GetAttribute("pitch").ToString();
            TimeType[i] = xe.GetAttribute("zone").ToString();
            i++;
        }
    }
}