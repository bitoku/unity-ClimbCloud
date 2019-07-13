using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatContoller : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;
    float EPSILON = 1e-7f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Mathf.Abs(this.rigid.velocity.y) < EPSILON)
        {
            this.rigid.AddForce(transform.up * this.jumpForce);
            this.animator.SetTrigger("JumpTrigger");
        }

        int key = 0;
        if (Input.acceleration.x > this.threshold) key = 1;
        if (Input.acceleration.x < -this.threshold) key = -1;

        float speedx = Mathf.Abs(this.rigid.velocity.x);

        if (speedx < this.maxWalkSpeed)
        {
            this.rigid.AddForce(transform.right * key * this.walkForce);
        }

        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        if (Mathf.Abs(this.rigid.velocity.y) < EPSILON && this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Walk")
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ゴール");
        SceneManager.LoadScene("ClearScene");
    }
}
