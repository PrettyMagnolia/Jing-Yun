using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;//�л�������
public class FinalMenuGUI : MonoBehaviour
{
    public static bool DisplayEndMenu = false;//��Ϸ�Ƿ��Ѿ�����

    int FinalScore;
    int FinalMaxCombo;

    public GUISkin menuSkin;
    public AudioClip beep;
    //private AudioSource audio;

    string menuPage = "final";

    private static int w = 400;
    private static int h = 60;
    // Start is called before the first frame update
    void Start()
    {
        FinalSet();
        FinalScore = ScoreTextScript.score;
        FinalMaxCombo = ScoreTextScript.MaxCombo;
    }

    private void FinalSet()//��������Ľű��������
    {
        Cursor.visible = true;//��ʾ���
        //GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = false;
        //GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnGUI()
    {
        GUI.skin = menuSkin;
        //GUI.BeginGroup(menuAreaNormalized);
        if (DisplayEndMenu)
        {
            int Box_Width = 500, Box_Height = 300;
           
                GUI.Box(new Rect(Screen.width / 2 - Box_Width / 2, Screen.height / 2 - Box_Height / 2, Box_Width, Box_Height), "游戏结束");
                GUI.Label(new Rect(Screen.width / 2 - 55, Screen.height / 2 - 75, 300, 25), "最终得分：" + FinalScore.ToString());
                GUI.Label(new Rect(Screen.width / 2 - 55, Screen.height / 2 - 50, 300, 25), "最大连击数：" + FinalMaxCombo.ToString());
            if (menuPage == "final")
            {
                if (GUI.Button(new Rect(Screen.width / 2 - w / 2, Screen.height / 2 + 2 * h / 3, w, h), "重新开始"))
                {
                    SaveByXML_2();
                    LoadBy_XML();
                    SceneManager.LoadScene("scene");
                }

                if (GUI.Button(new Rect(Screen.width / 2 - w / 2, Screen.height / 2 + 2 * h / 3 + 4 * h / 3, w, h), "返回主菜单"))
                {
                    SaveByXML();
                    SceneManager.LoadScene("StartMenu");
                }
            }
        }
    }
    IEnumerator ButtonAction(string levelName)
    {
        GetComponent<AudioSource>().PlayOneShot(beep);
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
    public void SaveByXML()
    {
        XmlDocument xmlDocument = new XmlDocument();
        #region CreateXML elements

        XmlElement Root = xmlDocument.CreateElement("Save");
        XmlElement score_element = xmlDocument.CreateElement("score");
        score_element.InnerText = DateTime.Now.ToString();
        Root.AppendChild(score_element);

        //XmlElement combo_element = xmlDocument.CreateElement("combo");
        //combo_element.InnerText = combo.ToString();
        //Root.AppendChild(combo_element);

        #endregion

        xmlDocument.AppendChild(Root);
        xmlDocument.Save(Application.dataPath + "/data.text");
        if (File.Exists(Application.dataPath + "/data.text"))
        {
            Debug.Log("XML save successed");
        }

    }
    public void SaveByXML_2()
    {
        XmlDocument XmlDocument_2 = new XmlDocument();
        XmlElement Root_2 = XmlDocument_2.CreateElement("Save");
        XmlElement Notes1 = XmlDocument_2.CreateElement("n1");
        XmlElement pitch1 = XmlDocument_2.CreateElement("p1");
        XmlElement zone1 = XmlDocument_2.CreateElement("z1");
        pitch1.InnerText = "A3";
        zone1.InnerText = "four";
        Notes1.AppendChild(pitch1);
        Notes1.AppendChild(zone1);
        Root_2.AppendChild(Notes1);


        XmlElement Notes2 = XmlDocument_2.CreateElement("n2");
        XmlElement pitch2 = XmlDocument_2.CreateElement("p2");
        XmlElement zone2 = XmlDocument_2.CreateElement("z2");
        pitch2.InnerText = "B4";
        zone2.InnerText = "eight";
        Notes2.AppendChild(pitch2);
        Notes2.AppendChild(zone2);
        Root_2.AppendChild(Notes2);


        XmlDocument_2.AppendChild(Root_2);

        XmlDocument_2.Save(Application.dataPath + "/music.text");
        if (File.Exists(Application.dataPath + "/music.text"))
        {
            //Debug.Log("music save success");
        }

    }
    public void LoadBy_XML()
    {
        int i = 1;
        if (File.Exists(Application.dataPath + "/music.text"))
        {
            Debug.Log("Load success");
            XmlDocument xmldocument_3 = new XmlDocument();
            xmldocument_3.Load(Application.dataPath + "/music.text");

            XmlNodeList note_list = xmldocument_3.GetElementsByTagName("Save");
            foreach (XmlNode note in note_list)
            {
                XmlNodeList sub_list = note.ChildNodes;
                string a = sub_list[0].InnerText;
                string b = sub_list[1].InnerText;
                i++;
                Debug.Log(a + "   " + "abc");
                Debug.Log("abc");
            }
        }
    }
}
