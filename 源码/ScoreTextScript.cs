using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           //导入资源库
using UnityEngine.SceneManagement;
public class ScoreTextScript : MonoBehaviour
{
    public static int score;//记录游戏分数
    public static int MaxCombo;//记录最大连击数
    public static bool IsComboing;//判断连击是否保持
    public static int CurrentCombo;//记录当前连击数
    public static Text txt_score;//显示分数的文本
    public static Text txt_MaxCombo;//显示最大连击数的文本
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
        txt_MaxCombo.text = "连击：" + CurrentCombo.ToString();
        txt_score.text = "分数：" + score.ToString();
        if (GameObject.Find("CurrentScoreText") != null)
        {
            pause_score = GameObject.Find("CurrentScoreText").GetComponent<Text>();
            pause_score.text = "分数：" + score.ToString();
        }
    }
    private int Max(int a, int b)
    {
        return a > b ? a : b;
    }
}
