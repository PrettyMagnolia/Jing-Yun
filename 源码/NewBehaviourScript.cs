using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    CharacterController controller;
    Animator animator;
    private bool CanAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        CanAttack = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        switch (other.name)
        {
            case "Archer_1":
                if (CanAttack)//只能攻击一次
                {
                    animator.SetTrigger("Attack_1");
                    CanAttack = false;
                }
                break;
            case "Arrow_Regular(Clone)":
                animator.SetTrigger("Death");
                Destroy(gameObject, 0.5F);
                break;
            default:
                break;
        }
        
    }
}
