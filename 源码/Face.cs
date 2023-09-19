using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public static bool AIGaming = false;
    private MeshRenderer mr;
    public int IsDestroyed;//�жϸ������Ƿ񱻴ݻ٣�������shader
    public static float MoveSpeed = 1.0f;
    private Vector3 FacePos;
    private Vector3 LightPos;
    public AudioClip audioClip;//���ݻ�ʱ����������
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
        //mr.sharedMaterial.SetInt("_IsDestroyed", IsDestroyed);//����shader��������shader�е��ܽ���Ч
        transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
        FacePos = this.GetComponent<Transform>().position;//��ȡʵʱλ��       
        if(AIGaming)
        {
            LightPos = GameObject.Find("light").GetComponent<Transform>().position;//��ȡ������λ��
            //if (FacePos.y <= LightPos.y && FacePos.z == LightPos.z)//ײ������������
            //{
            //    MusicDeath();
            //}
            if(FacePos.y<10 && flag)
            {
                flag = false;
                //�۷�
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
        gameObject.GetComponent<SphereCollider>().enabled = false;//�رմ���������ֹ�ڲ�����������ʱ��ɶ��λ��мӷ�
        Destroy(gameObject);
    }
}