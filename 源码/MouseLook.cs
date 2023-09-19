using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoBehaviour
{
    //�ӽ�ת������
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 } //����һ��ö�٣��ƶ�xy������ֻ���ƶ�x������y
    public RotationAxes axes = RotationAxes.MouseXAndY;                 //����һ��ö�ٱ����������������޸��ƶ�ģʽ
    public float sensitivityX = 15f;                                    //����һ���ƶ��ٶ�
    public float sensitivityY = 15f;
    public float minimumY = -60f;       //���帩�����ֵ���������ֵ��Ҫ��Ȼ��ת��ͷ
    public float maximumY = 60f;        //���帩�����ֵ���������ֵ��Ҫ��Ȼ��ת��ͷ
    float rotationY = 0f;               //�洢ʵ��ת����Yֵ



    void Start()
    {

    }
    // Update ����Ĵ��� ÿһ֡��������
    void Update()
    {
        Cursor.visible = false;//�������
        //�ӽ�ת������
        switch (axes)               //�ж��û�����������ת��ʽ
        {
            case RotationAxes.MouseXAndY:
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY; //
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                break;
            case RotationAxes.MouseX:
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
                break;
            case RotationAxes.MouseY:
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                break;
            default:
                break;
        }

    }



}
