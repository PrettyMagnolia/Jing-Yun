using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript2 : MonoBehaviour
{
    public static int score2 = 0;
    public static Text txt_score2;//��ʾ�������ı�
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        score2 = 0;
        txt_score2 = GameObject.Find("ScoreText2").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        txt_score2.text = "������" + score2.ToString();
    }
}
