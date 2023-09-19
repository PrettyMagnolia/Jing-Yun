using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float arrowspeed = 15f;  //子弹速度  

    private void Start()
    {
        Destroy(gameObject, 5);//5秒没射到物体，判定为射空，自动清除物体
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
