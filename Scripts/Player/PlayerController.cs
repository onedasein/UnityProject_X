//玩家移动控制
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;

    Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // FixedUpdate is called once per frame
    private void FixedUpdate()
    {
        //获取玩家输入
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        //为动画计算强度
        Vector2 rawInput = new Vector2(moveX, moveY);
        float inputStrength = rawInput.sqrMagnitude;

        //为移动计算方向
        Vector2 moveDirection = rawInput.normalized;

        //传递给动画
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", inputStrength);

        

        //处理镜像翻转
        //localScale局部缩放
        if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); //向左
        }
        else if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); //向右
        }
        //应用移动
        //刚体速度，由物理引擎处理
        rb.velocity = moveDirection * moveSpeed;
    }
}

