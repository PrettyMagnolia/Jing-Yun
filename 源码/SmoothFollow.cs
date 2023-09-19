using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmoothFollow : MonoBehaviour
{
    private string MoveMode;//标记向哪个方向移动，决定摄像机的移动方式
    public float targetDistance = 0.5F;
    public float smoothTime = 10;
    public Transform target;

    //private float distance = 1F;

    private bool IsRotating = false;//判断现在是否在旋转
    private float CurrentRotate = 0f;//用于记录单次旋转的已转过的角度
    private float SumRotate;//总旋转角度
    public float RotateSpeed = 300f;//旋转速度
    private string TurnDirection;//转弯方向，为left或right



    float initialDistanceY;
    float initialDistanceXZ;

    
    void Start()
    {
        MoveMode = CharacterControllerSimple.MoveMode;
        initialDistanceY = transform.position.y - target.position.y;
        initialDistanceXZ = transform.position.x - target.position.x;
    }
    void Update()
    {
        //摄像机转向时切换模式
        if (MoveMode != CharacterControllerSimple.MoveMode)//MoveMode是上一个方向，CharacterControllerSimple.MoveMode是当前方向
        {
            string previous = MoveMode;
            string current = CharacterControllerSimple.MoveMode;
            //判断左转还是右转
            if (previous == "Z+" && current == "X-" ||
                previous == "X-" && current == "Z-" ||
                previous == "Z-" && current == "X+" ||
                previous == "X+" && current == "Z+")
                TurnDirection = "left";
            else if (previous == "Z+" && current == "X+" ||
                previous == "X+" && current == "Z-" ||
                previous == "Z-" && current == "X-" ||
                previous == "X-" && current == "Z+")
                TurnDirection = "right";


            IsRotating = true;
            CurrentRotate = 0f;
            MoveMode = CharacterControllerSimple.MoveMode;
        }


        //旋转
        if (IsRotating)
        {
            if (TurnDirection == "left")
            {
                this.transform.RotateAround(target.position, Vector3.up, -Time.deltaTime * RotateSpeed);
                SumRotate = 90f;
            }
            else if (TurnDirection == "right")
            {
                this.transform.RotateAround(target.position, Vector3.up, Time.deltaTime * RotateSpeed);
                SumRotate = 90f;
            }

            CurrentRotate += Time.deltaTime * RotateSpeed;
            if (SumRotate - CurrentRotate < 1e-2)//转过的角度达到要求，则停止旋转
            {
                IsRotating = false;
                
            }
        }

        //旋转和摄像头跟随不能同时进行，否则不能以人物为旋转轴旋转
        if (!IsRotating)
        {

            
            if(MoveMode=="Z+")
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x,
                                                    target.transform.position.y + initialDistanceY,
                                                   target.transform.position.z + initialDistanceXZ), Time.deltaTime * 10);
            }
            else if(MoveMode=="Z-")
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x,
                                                    target.transform.position.y + initialDistanceY,
                                                   target.transform.position.z - initialDistanceXZ), Time.deltaTime * 10);
            }
            else if (MoveMode == "X+")
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x + initialDistanceXZ,
                                                    target.transform.position.y + initialDistanceY,
                                                   target.transform.position.z), Time.deltaTime * 10);
            }
            else if (MoveMode == "X-")
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x - initialDistanceXZ,
                                                    target.transform.position.y + initialDistanceY,
                                                   target.transform.position.z), Time.deltaTime * 10);
            }
        }
    }

}



