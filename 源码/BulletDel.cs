using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDel : MonoBehaviour
{
    private Vector3 BulletPos;//记录子弹位置
    private bool HitFlag;//判断是否击中目标
    private float timer;//计时变量


    // Start is called before the first frame update
    void Start()
    {
        HitFlag = false;
        timer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        BulletPos = gameObject.transform.position;
        //计时器，一秒后仍未发生碰撞则判定为无效射击，扣一分
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (!HitFlag)
            {
                ScoreTextScript.IsComboing = false;//中断连击
                ScoreTextScript.CurrentCombo = 0;
                ScorePlus(-1);
            }
            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ahhhh");
        //判断连击数
        DoCombo();

        //判断加多少分
        DoAddScore();

        HitFlag = true;
        Destroy(gameObject);
    }

    private void ScorePlus(int delta)//加分操作
    {
        //控制ScoreTextScript脚本
        ScoreTextScript.score += delta;
    }
    private bool IsLevelOne()
    {
        return (BulletPos.y >= 13.25f && BulletPos.y <= 13.75f);
    }
    private bool IsLevelTwo()
    {
        return (BulletPos.y < 13.25f || BulletPos.y > 13.75f);
    }
    private int Max(int a, int b)
    {
        return a > b ? a : b;
    }

    private void DoAddScore()
    {
        if (IsLevelOne())
        {
            ScorePlus(2);
        }
        else if (IsLevelTwo())
        {
            ScorePlus(1);
        }
    }
    private void DoCombo()
    {
        ScoreTextScript.IsComboing = true;
        ScoreTextScript.CurrentCombo++;
        ScoreTextScript.MaxCombo = Max(ScoreTextScript.CurrentCombo, ScoreTextScript.MaxCombo);
    }
}
