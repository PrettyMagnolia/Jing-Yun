using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           //������Դ��
using UnityEngine.SceneManagement;
public class ScoreTextScript : MonoBehaviour
{
    public static int score;//��¼��Ϸ����
    public static int MaxCombo;//��¼���������
    public static bool IsComboing;//�ж������Ƿ񱣳�
    public static int CurrentCombo;//��¼��ǰ������
    public static Text txt_score;//��ʾ�������ı�
    public static Text txt_MaxCombo;//��ʾ������������ı�
    public static Text pause_score;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnEnable()
    {
        score = 0;
        MaxCombo = 0;
        IsComboing = false;
        CurrentCombo = 0;
        txt_score = GameObject.Find("ScoreText").GetComponent<Text>();
        txt_MaxCombo = GameObject.Find("ComboText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!IsComboing)
            CurrentCombo = 0;
        MaxCombo = Max(CurrentCombo, MaxCombo);
        txt_MaxCombo.text = "������" + CurrentCombo.ToString();
        txt_score.text = "������" + score.ToString();
        if (GameObject.Find("CurrentScoreText") != null)
        {
            pause_score = GameObject.Find("CurrentScoreText").GetComponent<Text>();
            pause_score.text = "������" + score.ToString();
        }
    }
    private int Max(int a, int b)
    {
        return a > b ? a : b;
    }
}
