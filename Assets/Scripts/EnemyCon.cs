using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : MonoBehaviour {
	private Rigidbody2D rb;
	// 移动范围
	public float moveRange;
	public float speed;
	public bool isFrog;
	public float jumforce;
	public Animator anim;
	
	private float face = 1;
	private float left;
	private float right;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		left = transform.position.x - moveRange;
		right = transform.position.x + moveRange;
	}

	// Update is called once per frame
	private void FixedUpdate() {
		Movement();
	}

	private void Movement() {
		if(rb.position.x <= left) {
			face = 1;
		}
		if(rb.position.x >= right) {
			face = -1;
		}
		transform.localScale = new Vector3(-face, 1, 1);

		//frog jump
		// bool FrogJumping=false;
		// if (isFrog){
		// 	if(!FrogJumping){
		// 		rb.velocity = new Vector2(face*speed, jumforce);
		// 		anim.SetBool("jump",true);
		// 		anim.SetBool("fall",false);
		// 		FrogJumping=true;
		// 	}else if(rb.velocity.y<-1f){
		// 		anim.SetBool("jump",false);
		// 		anim.SetBool("fall",true);
		// 	}else if(rb.velocity.y==0){
		// 		anim.SetBool("jump",false);
		// 		anim.SetBool("fall",false);
		// 		FrogJumping=false;
		// 	}
		// }else rb.velocity = new Vector2(face * speed, rb.velocity.y);
	}
}