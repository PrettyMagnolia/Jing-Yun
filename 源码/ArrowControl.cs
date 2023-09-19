using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float arrowspeed = 15f;  //�ӵ��ٶ�  
    private bool CanAttack;

    private void Update()
    {
        CanAttack = CharacterControllerSimple.CanAttack;
        if (Input.GetMouseButtonDown(0) && CanAttack)
        {
            GameObject arrow = GameObject.Instantiate(arrowPrefab, transform.position, transform.rotation); //����һ����bullet(�ӵ�)һ���Ķ���λ�ú������λ��һ������Ϊ����ű��ǹ����������ģ�
            Rigidbody rgd = arrow.GetComponent<Rigidbody>(); //��a�������Ը�rgd
            rgd.velocity = transform.forward * arrowspeed; //Rigidbody.velocity ���������˲�������һ���㶨���ٶȣ����������������ٶȡ�
                                                           // transform.forward ����ǰ�ķ���  * speed �����ٶ�   
        }

    }

}
