using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;
using System;

public class NotesControl : MonoBehaviour
{
    private string FileName = "/musicAll.text";//�洢�������ļ���

    float speed = Face.MoveSpeed;
    public static bool DoEnding = false;

    int ite = 1;//ָ�룬�����������ൽ�˵ڼ�������

    private float timer = 0;//��ʱ����
    public float MusicSpeed;
    private float BeatTime;//һ�ĵ�ʱ��

    private bool[] NoteOn = new bool[1000];//0�ÿգ������i������δ������
    private bool[] TimerOn = new bool[1000];//�����i����ʱ������


    private string[] NoteType = new string[1000];
    private string[] TimeType = new string[1000];
    private float[] WaitTime = new float[1000];

    public GameObject NotePosition;//��������λ��������һ������
    public GameObject note;//����������� 

    public static bool StartGame = false;//�ж��Ƿ�ʼ��Ϸ

    private float four;
    private float four_;
    private float eight;
    private float eight_;
    private float sixteen;
    // Start is called before the first frame update
    void Start()
    {
        ite = 1;
        GameObject.Find("EndFlag").GetComponent<Face>().enabled = false;//��ʼ���ý�������˶�
        
       

        MusicSpeed = 30.0f;
        BeatTime = 60.0f / MusicSpeed;

        four = BeatTime * 1.0f / 2.0f;
        four_ = BeatTime * 3.0f / 4.0f;
        eight = BeatTime * 1.0f / 4.0f;
        eight_ = BeatTime * 3.0f / 8.0f;
        sixteen = BeatTime * 1.0f / 8.0f;


        for (int i = 0; i < NoteOn.Length; i++)
            NoteOn[i] = false;
        NoteOn[1] = true;//������һ������������Ȩ��
        for (int i = 0; i < TimerOn.Length; i++)
            TimerOn[i] = false;

        for (int i = 0; i < 1000; i++)
        {
            NoteType[i] = "0";
            TimeType[i] = "0";
            WaitTime[i] = 0;
        }
        //�ļ���д
        LoadMusic();//���ⲿ�ļ��ж�������

        //��WaitTime[]��ֵ
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
            EndScript.JumpToEnd = true;//��ʼ��EndScript�ű�������������


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
                TimerOn[NumTimer] = false;//��ʱ���ر�
                NoteOn[NumTimer + 1] = true;//������һ������
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
    private void SetNote(string _NoteTypeStr)//������������
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
            NoteOn[NumNote] = false;//�رո���������
            TimerOn[NumNote] = true;//������������ʱ��
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
    public void LoadMusic()//װ������
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