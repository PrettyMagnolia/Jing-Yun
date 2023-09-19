// Instantiates 10 copies of Prefab each 2 units apart from each other

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;
using System;

public class Create : MonoBehaviour
{
    enum Direction{ SetZ_plus = 1, SetZ_minor = 2, SetX_plus = 3, SetX_minor = 4 };


    float create_range;
    Vector3 location;
    Transform prefab;

    //�ļ�����ָ��
    private int ite;
    //�ļ���д����
    private string[] NoteType = new string[1000];
    private string WasteSpace;//���մ�ģʽ���õı�ʾ�����ࡱ���ַ���
    private string FileName = "/musicAll.text";


   

    public static bool ModeTwoStart = false;
    public Transform[] prefabs;
    private GameObject human;
    System.Random random = new System.Random();

    private Vector3 DefaultSize = new Vector3(0.2f, 0.8f, 0.6f);//����Ĭ�ϴ�С
    private float EnlargeTimes = 2;//����������
    const int line = 10;  //���м���·
    const int column = 4;  //��Ԫ���ʾһ��·����Ϣ
    
    float[,] Road = new float[line, column];
    public Transform[] Start;
    public Transform[] End;

    const int gap = 5;
    const int height = 12;

    public float RandomRange(float min, float max)
    {
        var r = random.NextDouble();
        return (float)(r * (max - min) + min);
    }

  
    private void OnEnable()
    {
        ite = 1;
        LoadMusic();//������������ϴ��ⲿ�ļ�װ���ڲ�����
        SmallFace();
        load_road_data();
        create_road_entity();
        BigFace();
        
    }
    private void Update()
    {
        if (ModeTwoStart)
            human = GameObject.Find("Archer_1");
    }
    public void load_road_data()
    {
        //Road1
        AddRoad(0, "X+");
        //Road2
        AddRoad(1, "Z+");
        //Road3
        AddRoad(2, "X-");
        //Road4
        AddRoad(3, "Z-");
        //Road5
        AddRoad(4, "X-");
        //Road6
        AddRoad(5, "Z+");
        //Road7
        AddRoad(6, "X-");
        //Road8
        AddRoad(7, "Z-");
        //Road9
        AddRoad(8, "X+");
    }

    public void create_road_entity()
    {
        for (int i = 0; i < line; i++)
        {
            for (int j = 0; j < System.Math.Abs(Road[i, 1] - Road[i, 2]) / gap; j++)
            {
                prefab = GetNextNote();//��ȡ��һ������
                //Debug.Log(ite);
                create_range = RandomRange(Road[i, 3] - 2, Road[i, 3] + 2) ;

                //��ȡλ��
                if (Road[i, 0] == 1 || Road[i, 0] == 2)//��Z����ֱ��
                    location = new Vector3(create_range, height, Road[i, 1] + j * gap);

                else if (Road[i, 0] == 3 || Road[i, 0] == 4) //��X����ֱ��
                {
                    location = new Vector3(Road[i, 1] + j * gap, height, create_range);                
                }
                //Debug.Log(location.x.ToString() + "," + location.y.ToString() + "," + location.z.ToString());

                if (ModeTwoStart && prefab != null) 
                {
                    Quaternion Rotation;
                    if (Road[i, 0] == 1)
                        Rotation = GameObject.Find("SetZ+").transform.rotation;
                    else if (Road[i, 0] == 2)
                        Rotation = GameObject.Find("SetZ-").transform.rotation;
                    else if (Road[i, 0] == 3)
                        Rotation = GameObject.Find("SetX+").transform.rotation;
                    else//Road[i, 0] == 4
                        Rotation = GameObject.Find("SetX-").transform.rotation;
                    if (i == 0)
                        Debug.Log("Create1");
                    if (i == 1)
                        Debug.Log("Created2");
                    Instantiate(prefab, location, Rotation);
                }

            }
            

        }
    }
    public void LoadMusic()//���ⲿ�ļ�װ������
    {
        int i = 1;
        XmlDocument xml = new XmlDocument();
        xml.Load(Application.streamingAssetsPath + FileName);
        XmlNodeList list = xml.SelectSingleNode("Save").ChildNodes;
        foreach (XmlElement xe in list)
        {
            NoteType[i] = xe.GetAttribute("pitch").ToString();
            WasteSpace = xe.GetAttribute("zone").ToString();
            i++;
        }
    }
    private Transform GetNextNote()//������һ������
    {
        Transform prefab;
        switch(NoteType[ite])
        {
            case "A3":
                prefab = prefabs[0];
                break;
            case "B3":
                prefab = prefabs[1];
                break;
            case "C4":
                prefab = prefabs[2];
                break;
             case "D4":
                prefab = prefabs[3];
                break;
            case "E4":
                prefab = prefabs[4];
                break;
            case "F4":
                prefab = prefabs[5];
                break;
            case "G4":
                prefab = prefabs[6];
                break;
            case "A4":
                prefab = prefabs[7];
                break;
            case "B4":
                prefab = prefabs[8];
                break;
            case "C5":
                prefab = prefabs[9];
                break;
            default:
                //Debug.Log("�գ�");
                //Debug.Log(ite);
                //Debug.Log("abc " + NoteType[9]);
                prefab = null;
                break;
        }
        ite++;
        return prefab;
    }
    private float GetZ(Transform FlagObject)
    {
        return FlagObject.position.z;
    }
    private float GetX(Transform FlagObject)
    {
        return FlagObject.position.x;
    }
    private void SmallFace()//��С���״�С
    {
        //������֮ǰ�Ȱ�������ģʽ2�Ĵ�С
        for (int i = 0; i < 10; i++) 
            prefabs[i].localScale = DefaultSize * EnlargeTimes;//Ĭ�ϴ�С*������
    }
    private void BigFace()
    {
        for (int i = 0; i < 10; i++)
            prefabs[i].localScale = DefaultSize;
    }
    private void AddRoad(int i, string direction)
    {
        switch (direction)
        {
            case "Z+":
                Road[i, 0] = (float)Direction.SetZ_plus;  //��ʾx���䣬��Z+��
                break;
            case "Z-":
                Road[i, 0] = (float)Direction.SetZ_minor;
                break;
            case "X+":
                Road[i, 0] = (float)Direction.SetX_plus;
                break;
            case "X-":
                Road[i, 0] = (float)Direction.SetX_minor;
                break;
            default:
                Debug.Log("�������");
                break;
        }
        if (direction == "Z+" || direction == "Z-")
        {
            Road[i, 1] = GetZ(Start[i]);//z������
            Road[i, 2] = GetZ(End[i]);//z������
            Road[i, 3] = GetX(Start[i]);//x
        }
        else if (direction == "X+" || direction == "X-")  
        {
            Road[i, 1] = GetX(Start[i]);//z������
            Road[i, 2] = GetX(End[i]);//z������
            Road[i, 3] = GetZ(Start[i]);//x
        }
    }
}