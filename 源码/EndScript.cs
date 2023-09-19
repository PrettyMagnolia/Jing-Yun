using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           //������Դ��
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public static bool JumpToEnd = false;//��NotesControl����

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
    private void DoWhenEnd()//��Ϸ����ʱ�л�����
    {
        if (JumpToEnd)
        {
            EndPos = GameObject.Find("EndFlag").GetComponent<Transform>().position;
            GameObject.Find("EndFlag").GetComponent<Face>().enabled = true;//"�������"�˶�
            if (EndPos.y <= 10f)
            {
                FinalMenuGUI.DisplayEndMenu = true;
                SceneManager.LoadScene("EndMenu");
            }
        }
    }
}
