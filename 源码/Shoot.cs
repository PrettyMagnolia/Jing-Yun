using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //�ӵ��������
   
    public float bulletspeed = 150f;  //�ӵ��ٶ�  
    public GameObject bullet;  //����һ�������ӵ�
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�ӵ��������
       
        //translate��transform��һ������ ��ʾƽ�� Vector3��x,y,z����ʾ�ռ��е�һ����ά���� ����������ͨ��h��z�����������������Ĵ�С��ʵ�������λ��
        //Updateһִ֡��60�Σ�ÿ֡��new һ��Vector3�Ļ������ڴ���һ�����ģ����Գ�һ�� Time.deltaTime��ÿ֡��ִֻ��һ�� Ҳ����˵ ��һ��ֻ�ƶ�3����λ��speed=3������Ȼ�ͻ��ƶ�speed*60����λ
        if (Input.GetMouseButtonDown(0))  //�������������  �����ʾΪ  Input.GetMouseButton(0)
        {
            //instantiate����¡����,��¡λ��,��¡�Ƕȣ�   transform.positionû��ǰ׺ Ĭ��Ϊthis.transform.potiton ����������λ��
            GameObject a = GameObject.Instantiate(bullet, transform.position, transform.rotation); //����һ����bullet(�ӵ�)һ���Ķ���λ�ú������λ��һ������Ϊ����ű��ǹ����������ģ�
            Rigidbody rgd = a.GetComponent<Rigidbody>(); //��a�������Ը�rgd
            rgd.velocity = transform.forward * bulletspeed; //Rigidbody.velocity ���������˲�������һ���㶨���ٶȣ����������������ٶȡ�
                                                            // transform.forward ����ǰ�ķ���  * speed �����ٶ�   


        }
    }

}
