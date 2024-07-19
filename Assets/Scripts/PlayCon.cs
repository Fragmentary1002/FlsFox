using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayCon : MonoBehaviour
{   [Header("控制移动")]
    public Rigidbody2D rb;//fox 刚体
    public float Speed;//fox 速度
    public float jumpforce;

    [Header("动画效果")]
    public Animator anim;
    public LayerMask Gound;
    public int isJumpingCntMax;
    // public Collision2D coll;
    
    [Header("玩家信息")]
    public int CherryCnt;
    public int Health;
    public TextMeshProUGUI CherryCntText;
    public TextMeshProUGUI HealthText;

    private bool isJumping;
    private int isJumpingCnt;
    private bool isHurt=false;
    private float leftPoint,rightPoint;//左右终止点
    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        if(!isHurt) Movement();
        SwithAnim();
    }
    void Movement(){
        float horizontalMove=Input.GetAxis("Horizontal");
        float faceDircetion=Input.GetAxisRaw("Horizontal");
        //角色移动
        if(horizontalMove!=0){
            rb.velocity=new Vector2(Speed*horizontalMove,rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(faceDircetion));
        }
        if(faceDircetion!=0){
            transform.localScale=new Vector3(faceDircetion,1,1);
        }
        if(Input.GetButtonDown("Jump") && isJumpingCnt< isJumpingCntMax){//角色跳
            rb.velocity=new Vector2(rb.velocity.x,jumpforce);
            isJumping=true;
            isJumpingCnt++;
        }else isJumping=false;
    }
    void SwithAnim(){
        if(isHurt){
            if(Mathf.Abs(rb.velocity.x)<0.1f){
                isHurt=false;
                anim.SetBool("hurt",false);
                anim.SetBool("idle",true);
                anim.SetBool("jumping",false);
                anim.SetBool("falling",false);
                anim.SetFloat("running",0);
            }else{
                anim.SetBool("hurt",true);
                anim.SetBool("jumping",false);
                anim.SetBool("falling",false);
                anim.SetBool("idle",false);
            }

        }else if (isJumping && rb.velocity.y>0){   //如果跳-->速度正
            anim.SetBool("jumping",true);
            anim.SetBool("falling",false);
            anim.SetBool("idle",false);
        }else if(rb.velocity.y<-1f) { //速度负      
            anim.SetBool("jumping",false);
            anim.SetBool("falling",true);
            anim.SetBool("idle",false);   
        }else if(rb.velocity.y==0){//判断是否站立
            anim.SetBool("jumping",false);
            anim.SetBool("falling",false);
            anim.SetBool("idle",true);
            isJumpingCnt=0;
        }
    }
    //收集
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag=="Collection"){
            Destroy(collision.gameObject);
            CherryCnt++;
            CherryCntText.text=CherryCnt.ToString();
        }
    }
    // 消灭敌人
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Enemy"){
            if(anim.GetBool("falling")){
                Destroy(collision.gameObject);
            }else{
                Health--;
                HealthText.text=Health.ToString();
                isHurt=true;
                if(transform.position.x < collision.gameObject.transform.position.x){
                    rb.velocity=new Vector2(-10,rb.velocity.y);
                }else if(transform.position.x>collision.gameObject.transform.position.x){
                    rb.velocity=new Vector2(10,rb.velocity.y);
                }
            }
        }
    }
      
    // void Update(float maxTime){
    //     float m_timer = 0;
    //     m_timer += Time.time;
    //     if (m_timer >= maxTime){
    //         m_timer = 0;
    //         return;
    //     }
    // }
}
