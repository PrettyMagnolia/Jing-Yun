using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           //导入资源库
using UnityEngine.SceneManagement;

public class EndScript2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    private void OnTriggerEnter(Collider other)
    {
        FinalMenuGUI2.DisplayEndMenu2 = true;
        Create.ModeTwoStart = false;
        SceneManager.LoadScene("EndMenu2");
    }
}
