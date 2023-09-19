using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDel : MonoBehaviour
{
    private Vector3 BulletPos;//��¼�ӵ�λ��
    private bool HitFlag;//�ж��Ƿ����Ŀ��
    private float timer;//��ʱ����


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
        //��ʱ����һ�����δ������ײ���ж�Ϊ��Ч�������һ��
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (!HitFlag)
            {
                ScoreTextScript.IsComboing = false;//�ж�����
                ScoreTextScript.CurrentCombo = 0;
                ScorePlus(-1);
            }
            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ahhhh");
        //�ж�������
        DoCombo();

        //�жϼӶ��ٷ�
        DoAddScore();

        HitFlag = true;
        Destroy(gameObject);
    }

    private void ScorePlus(int delta)//�ӷֲ���
    {
        //����ScoreTextScript�ű�
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
