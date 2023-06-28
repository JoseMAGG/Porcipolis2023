using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LodoController : MonoBehaviour
{
    public float rm;

    public Animator animator;
    
    
    void Start()
    {

    }

    void Update()
    {

        rm = PlayerPrefs.GetInt("RockMeter");

        if (rm >= 10)
        {
            animator.SetBool("goodMov", true);
        }
        else
        {
            animator.SetBool("goodMov", false);
        }
    }

}
