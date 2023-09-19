using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicalModeSetScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnEnable()
    {
        if (MainMenuGUI.mode == 1)
        {
            //设置AudioListener仅有一个开启
            //GameObject.Find("StartCamera").GetComponent<AudioListener>().enabled = false;
            //GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = true;



            GameObject.Find("Main Camera").SetActive(true);
            GameObject.Find("Controller").SetActive(true);
            GameObject.Find("Canvas").SetActive(true);
        }
        else if (MainMenuGUI.mode == 2)
        {
            Debug.Log("abc");
            //设置AudioListener仅有一个开启
            //GameObject.Find("StartCamera").GetComponent<AudioListener>().enabled = false;
            //GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = true;




            GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = false;
            //GameObject.Find("Canvas").SetActive(false);
        }
    }   
    
}
