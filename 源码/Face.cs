using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public static bool AIGaming = false;
    private MeshRenderer mr;
    public int IsDestroyed;//判断该物体是否被摧毁，并传给shader
    public static float MoveSpeed = 1.0f;
    private Vector3 FacePos;
    private Vector3 LightPos;
    public AudioClip audioClip;//被摧毁时发出的音符
    private bool flag = true;
    // Start is called before the first frame update
    void Start()
    {       
        IsDestroyed = 0;
        //mr = gameObject.GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {

        if (gameObject.transform.position.y == GameObject.Find("A3BornPos").GetComponent<Transform>().position.y && AIGaming && gameObject.name != "EndFlag") 
            StartCoroutine(WaitForDeath());
    }
    IEnumerator WaitForDeath()
    {
        float init_y=GameObject.Find("A3BornPos").GetComponent<Transform>().position.y;
        float end_y = GameObject.Find("light").GetComponent<Transform>().position.y;
        float delta_y = init_y - end_y;
        //Debug.Log(delta_y);
        yield return new WaitForSecondsRealtime(delta_y / MoveSpeed);
        MusicDeath();
    }
    // Update is called once per frame
    void Update()
    {
        //mr.sharedMaterial.SetInt("_IsDestroyed", IsDestroyed);//传入shader，并启用shader中的溶解特效
        transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
        FacePos = this.GetComponent<Transform>().position;//获取实时位置       
        if(AIGaming)
        {
            LightPos = GameObject.Find("light").GetComponent<Transform>().position;//获取激光线位置
            //if (FacePos.y <= LightPos.y && FacePos.z == LightPos.z)//撞到激光线消除
            //{
            //    MusicDeath();
            //}
            if(FacePos.y<10 && flag)
            {
                flag = false;
                //扣分
                ScoreTextScript.score--;
            }
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        IsDestroyed = 1;
        MusicDeath();
    }
    private void MusicDeath()
    {   
        AudioSource.PlayClipAtPoint(audioClip, transform.localPosition);
        gameObject.GetComponent<SphereCollider>().enabled = false;//关闭触发器，防止在播放死亡动画时造成二次击中加分
        Destroy(gameObject);
    }
}