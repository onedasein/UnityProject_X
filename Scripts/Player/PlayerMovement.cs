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

        //计算移动方向
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        

        //处理镜像翻转
        if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); //向左
        }
        else if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); //向右
        }
        //应用移动
        rb.velocity = moveDirection * moveSpeed;
    }
}
