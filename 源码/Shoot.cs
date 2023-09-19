using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //子弹射击部分
   
    public float bulletspeed = 150f;  //子弹速度  
    public GameObject bullet;  //定义一个对象：子弹
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //子弹射击部分
       
        //translate是transform的一个属性 表示平移 Vector3（x,y,z）表示空间中的一个三维向量 我们这里来通过h和z的输入来控制向量的大小来实现相机的位移
        //Update一帧执行60次，每帧都new 一个Vector3的话，对内存是一个消耗，所以乘一个 Time.deltaTime后每帧都只执行一次 也就是说 按一下只移动3个单位（speed=3），不然就会移动speed*60个单位
        if (Input.GetMouseButtonDown(0))  //如果按下鼠标左键  代码表示为  Input.GetMouseButton(0)
        {
            //instantiate（克隆对象,克隆位置,克隆角度）   transform.position没有前缀 默认为this.transform.potiton 即这个物体的位置
            GameObject a = GameObject.Instantiate(bullet, transform.position, transform.rotation); //创建一个和bullet(子弹)一样的对象，位置和相机的位置一样，因为这个脚本是挂在相机上面的；
            Rigidbody rgd = a.GetComponent<Rigidbody>(); //把a刚体属性给rgd
            rgd.velocity = transform.forward * bulletspeed; //Rigidbody.velocity 这个方法是瞬间给物体一个恒定的速度，将物体提升至该速度。
                                                            // transform.forward 是向前的方向  * speed 发射速度   


        }
    }

}
