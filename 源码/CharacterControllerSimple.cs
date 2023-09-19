using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
public class CharacterControllerSimple : MonoBehaviour
{
    //虚拟按键实现持续前进
    [DllImport("user32.dll", EntryPoint = "keybd_event")]
    public static extern void keybd_event(
    byte bVk,    //虚拟键值 对应按键的ascll码十进制值
    byte bScan,// 0
    int dwFlags,  //0 为按下，1按住，2为释放
    int dwExtraInfo  // 0
    );

    public static bool CanAttack = true;

    public static string MoveMode = "X+";//移动模式，表示朝哪个方向移动
    public float curSpeed = 50F;
    public float RunSpeed = 3.0F;//直行跑步速度
    public float TurnSpeed = 4.0F;//左右移动速度
    public float rotateSpeed = 100.0F;

    CharacterController controller;
    Animator animator;


    bool is_death;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        CanAttack = true;
        is_death = false;
        MoveMode = "X+";
    }
    void Update()
    {
        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        //Vector3 forward = transform.TransformDirection(Vector3.forward);


        //controller.SimpleMove(forward * curSpeed);
        if (is_death)
        {
            //撤销虚拟按键
            keybd_event(87, 0, 2, 0);//按W，W的ASCII码是87
            keybd_event(83, 0, 2, 0);//按S，S的ASCII码是83
            keybd_event(68, 0, 2, 0);//按D，D的ASCII码是68
            keybd_event(65, 0, 2, 0);//按A，A的ASCII码是65
            //播放死亡动画
            animator.SetTrigger("Death");
        }
        else
        {
            //虚拟按键
            if (!Input.GetKey(KeyCode.W) && MoveMode == "Z+")
            {
                keybd_event(87, 0, 1, 0);//按W，W的ASCII码是87
            }
            else if (!Input.GetKey(KeyCode.S) && MoveMode == "Z-")
            {
                keybd_event(83, 0, 1, 0);//按S，S的ASCII码是83
            }
            else if (!Input.GetKey(KeyCode.D) && MoveMode == "X+")
            {
                keybd_event(68, 0, 1, 0);//按D，D的ASCII码是68
            }
            else if (!Input.GetKey(KeyCode.A) && MoveMode == "X-")
            {
                keybd_event(65, 0, 1, 0);//按A，A的ASCII码是65
            }
        }


        Vector3 direction = transform.eulerAngles;
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion") ||
            this.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") ||
            this.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {

            float curSpeed = Mathf.Abs(RunSpeed * Input.GetAxis("Vertical")) + Mathf.Abs(RunSpeed * Input.GetAxis("Horizontal"));
            animator.SetFloat("Speed", curSpeed);

            float angleMedian = 0;
            float arrowsPressed = 0;
            //往X-直行
            if (MoveMode == "X-")
            {
                if (Input.GetKey(KeyCode.A))
                    GoLeft(1, angleMedian, arrowsPressed);

                if (Input.GetKey(KeyCode.Q))
                    GoBack(2, angleMedian, arrowsPressed);

                if (Input.GetKey(KeyCode.E))
                    GoFront(2, angleMedian, arrowsPressed);
            }
            //按X+直行
            if (MoveMode == "X+")
            {
                if (Input.GetKey(KeyCode.D))
                    GoRight(1, angleMedian, arrowsPressed);

                if (Input.GetKey(KeyCode.Q))
                    GoFront(2, angleMedian, arrowsPressed);

                if (Input.GetKey(KeyCode.E))
                    GoBack(2, angleMedian, arrowsPressed);

            }
            //往Z+直行
            if (MoveMode == "Z+")
            {
                if (Input.GetKey(KeyCode.W))
                    GoFront(1, angleMedian, arrowsPressed);

                if (Input.GetKey(KeyCode.Q))
                    GoLeft(2, angleMedian, arrowsPressed);

                if (Input.GetKey(KeyCode.E))
                    GoRight(2, angleMedian, arrowsPressed);

            }
            //往Z-直行
            if (MoveMode == "Z-")
            {
                if (Input.GetKey(KeyCode.S))
                    GoBack(1, angleMedian, arrowsPressed);

                if (Input.GetKey(KeyCode.Q))
                    GoRight(2, angleMedian, arrowsPressed);

                if (Input.GetKey(KeyCode.E))
                    GoLeft(2, angleMedian, arrowsPressed);
            }



            if (arrowsPressed > 0)
            {
                Debug.Log(new Vector3(0, angleMedian / arrowsPressed, 0) + " " + angleMedian + " " + arrowsPressed);
                transform.eulerAngles = new Vector3(0, angleMedian / arrowsPressed, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && CanAttack)
        {
            animator.SetTrigger("Attack_1");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    animator.SetTrigger("Attack_2");
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    animator.SetTrigger("Death");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    animator.SetTrigger("Use_1");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    animator.SetTrigger("Use_2");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    animator.SetTrigger("Use_3");
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    animator.SetTrigger("Damage");
        //}
    }
    //碰撞检测，处理转向
    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "SetZ+":
                MoveMode = "Z+";
                break;
            case "SetZ-":
                MoveMode = "Z-";
                break;
            case "SetX+":
                MoveMode = "X+";
                break;
            case "SetX-":
                MoveMode = "X-";
                break;
            case "A3(Clone)":
            case "B3(Clone)":
            case "C4(Clone)":
            case "D4(Clone)":
            case "E4(Clone)":
            case "F4(Clone)":
            case "G4(Clone)":
            case "A4(Clone)":
            case "B4(Clone)":
            case "C5(Clone)":
                ScoreTextScript2.score2++;//撞到音符，加分
                break;
            case "Warrior_1":
                is_death = true;
                StartCoroutine(GoDeath());
                break;

        }
    }
    private void GoFront(int mode, float angleMedian, float arrowsPressed)//(x,y,z)为单位向量
    {
        float speed = (mode == 1) ? RunSpeed : TurnSpeed;
        if (mode == 1)//模式参数，输入1为直行，输入2为转向
            transform.eulerAngles = new Vector3(0, 0, 0);//突变角度
        transform.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;//位移
        if (Input.GetKey(KeyCode.A))
        {
            angleMedian = 315;
        }
        else
        {
            arrowsPressed++;
        }
    }
    private void GoLeft(int mode, float angleMedian, float arrowsPressed)//(x,y,z)为单位向量
    {
        float speed = (mode == 1) ? RunSpeed : TurnSpeed;
        if (mode == 1)
            transform.eulerAngles = new Vector3(0, 270, 0);
        transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        angleMedian += 270;
        arrowsPressed++;
    }
    private void GoRight(int mode, float angleMedian, float arrowsPressed)//(x,y,z)为单位向量
    {
        float speed = (mode == 1) ? RunSpeed : TurnSpeed;
        if (mode == 1)
            transform.eulerAngles = new Vector3(0, 90, 0);
        transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        angleMedian += 90;
        arrowsPressed++;
    }
    private void GoBack(int mode, float angleMedian, float arrowsPressed)
    {
        float speed = (mode == 1) ? RunSpeed : TurnSpeed;
        if (mode == 1)
            transform.eulerAngles = new Vector3(0, 180, 0);
        transform.position += new Vector3(0, 0, -1) * speed * Time.deltaTime;
        angleMedian += 180;
        arrowsPressed++;
    }
    IEnumerator GoDeath()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        FinalMenuGUI2.DisplayEndMenu2 = true;
        Create.ModeTwoStart = false;
        SceneManager.LoadScene("EndMenu2");
    }
}
