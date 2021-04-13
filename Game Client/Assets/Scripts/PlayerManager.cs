using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public Vector2 axisRaw;

    public Animator animator;


    public Vector2 inputsAnimation;
    void Update()
    {
        animator.SetFloat("moveX", inputsAnimation.x);
        animator.SetFloat("moveY", inputsAnimation.y);


        if (inputsAnimation.x == 1 || inputsAnimation.x == -1 || inputsAnimation.y == 1 || inputsAnimation.y == -1)
        {
            animator.SetFloat("lastMoveX", inputsAnimation.x);
            animator.SetFloat("lastMoveY", inputsAnimation.y);
        }
    }

}