using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllOutside : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDrection;
    public Animator anim;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //process input
        ProcessInputs();
        Animate();
    }

    void FixedUpdate() {
        //physics calculation
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        moveDrection = new Vector2(moveX, moveY).normalized;

    }
    void Move() {
        rb.velocity = new Vector2(moveDrection.x * moveSpeed, moveDrection.y * moveSpeed);
    }

    void Animate() {
        anim.SetFloat("AniMoveX", moveDrection.x);
        anim.SetFloat("AniMoveY", moveDrection.y);
    }
}
