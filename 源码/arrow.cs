using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float arrowspeed = 15f;  //�ӵ��ٶ�  

    private void Start()
    {
        Destroy(gameObject, 5);//5��û�䵽���壬�ж�Ϊ��գ��Զ��������
    }
    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Warrior_1")
        {
            ScoreTextScript2.score2 += 10;
            Destroy(gameObject);
        }
    }
}
