using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 3f;  //����һ���������͵ı��� ����������ƶ��ٶ� public���Ϳ����ڴ���������޸� ���Դ������޸ĵ�������Ϊ�ű������е�����
    private float h;  //����h����ȡHorizontal�����᷽������룬��A����D�������룩
    private float z;  //����z����ȡVertical�����᷽������룬��W����S�������룩
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");  //��ȡAD��������  ��ˮƽ����
        float z = Input.GetAxis("Vertical");    //��ȡWS��������  ��ֱ����
        transform.Translate(new Vector3(h, 0, z) * Time.deltaTime * moveSpeed);
    }
}
