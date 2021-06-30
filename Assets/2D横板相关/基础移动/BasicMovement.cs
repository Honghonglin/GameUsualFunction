using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    //基础组件
    private Rigidbody2D rb;
    public Collider2D coll_up ;
    public Collider2D coll_down;
    public Animator anim ;
    //----------------------------

    //基础数值
    public float speed ;

    public float jumpForce ; 
    
    private bool jumpPressed ; 
    public int SetJumpCount ;   //可跳跃次数
    private int jumpCount ;

    //检测相关
    public Transform cellingChecker ; //上部检测
    public Transform groundChecker ; //下部检测
    public LayerMask ground ;

    //状态
    public bool isGround ,isJump,isCrouch ;

    void Start()
    {
        //获取相关变量
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>() ;
       // coll_up = GetComponent<Collider2D>();  //部分Collider需要准确指出，所以需要在Unity界面中设置

    }

    void Update()
    {
        FreshInUpdate();
        
        //基础WD移动模块
        HorizonMovement();
        SwichAnim();

        if(!isCrouch)  Jump();
        Crouch();
    }

    private void FixedUpdate()
    {
        FreshInFixUpdate();
    }

    private void HorizonMovement(){
        
            //地面移动
            float horizontalMove = Input.GetAxisRaw("Horizontal");
        //通过将按键赋值给临时变量来达到获取移动状态的效果 
             rb.velocity = new Vector2(horizontalMove * speed , rb.velocity.y);
        //x轴方向给予对应的向量加速，而y轴保持原值

             if(horizontalMove != 0 ){
            //如果获取值不为0，也就是非静止状态
            transform.localScale = new Vector3(horizontalMove,1,1);
            //这里直接利用horizontalMove的值，因为右是1，左是-1.如果向左得到-1.就会水平翻转图片（scale = -1）  
        }

    }

    private void Jump(){
        //! 跳跃按键的检测应该与执行放在同一位置
        if(Input.GetButtonDown("Jump") && jumpCount > 0 ){
            jumpPressed = true ;
        }

        if(isGround){
            jumpCount = SetJumpCount ; 
            isJump = false ; 
        }

        if(jumpPressed && isGround){
            isJump = true ; 
            rb.velocity = new Vector2(rb.velocity.x , jumpForce);
            jumpCount -- ;
            jumpPressed = false ; 
        }else if(jumpPressed && jumpCount > 0 && !isGround){
            rb.velocity = new Vector2(rb.velocity.x , jumpForce);
            jumpCount -- ;
            jumpPressed = false ; 
        }
    }

    private void Crouch(){
        if (Input.GetButton("Crouch"))
        {
            coll_up.enabled = false;
            isCrouch = true ;
            anim.SetBool("Crouching",true);
        }
        else
        {
            if (!Physics2D.OverlapCircle(cellingChecker.position, 0.2f, ground))
            {
                coll_up.enabled = true;
                isCrouch = false ;
            anim.SetBool("Crouching",false);
            }
        }
    
    }

    private void SwichAnim(){
        //动画切换
        anim.SetFloat("Running",Mathf.Abs(rb.velocity.x));
        //设置跑步
        if(isGround){
           anim.SetBool("Falling",false);
           //在地面，没有falling 
        }else if (!isGround && rb.velocity.y > 0){
            //上升状态，jumping
            anim.SetBool("Jumping",true);
        }else if (rb.velocity.y < 0 ){
            //下落状态
            anim.SetBool("Falling",true);
            anim.SetBool("Jumping",false);
        }
    }

    private void FreshInUpdate(){

    }

    private void FreshInFixUpdate(){
        //按键与状态检测
        isGround = Physics2D.OverlapCircle(groundChecker.position,0.1f,ground);
    }
}
