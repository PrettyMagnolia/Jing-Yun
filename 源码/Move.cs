using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 3f;  //设置一个共有类型的变量 来设置相机移动速度 public类型可以在代码外进行修改 并以代码外修改的数据作为脚本中运行的数据
    private float h;  //设置h来获取Horizontal（横轴方向的输入，即A键和D键的输入）
    private float z;  //设置z来获取Vertical（纵轴方向的输入，即W键和S键的输入）
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");  //获取AD键的输入  即水平方向
        float z = Input.GetAxis("Vertical");    //获取WS键的输入  垂直方向
        transform.Translate(new Vector3(h, 0, z) * Time.deltaTime * moveSpeed);
    }
}
