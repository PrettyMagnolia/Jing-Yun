using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float arrowspeed = 15f;  //子弹速度  
    private bool CanAttack;

    private void Update()
    {
        CanAttack = CharacterControllerSimple.CanAttack;
        if (Input.GetMouseButtonDown(0) && CanAttack)
        {
            GameObject arrow = GameObject.Instantiate(arrowPrefab, transform.position, transform.rotation); //创建一个和bullet(子弹)一样的对象，位置和相机的位置一样，因为这个脚本是挂在相机上面的；
            Rigidbody rgd = arrow.GetComponent<Rigidbody>(); //把a刚体属性给rgd
            rgd.velocity = transform.forward * arrowspeed; //Rigidbody.velocity 这个方法是瞬间给物体一个恒定的速度，将物体提升至该速度。
                                                           // transform.forward 是向前的方向  * speed 发射速度   
        }

    }

}
