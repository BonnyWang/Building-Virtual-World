using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeekeeperMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 movement;

    private bool facingLeft = true;
    private SpriteRenderer spriteRenderer;

    Animator animator;
    Keeper keeper;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        keeper = GetComponent<Keeper>();

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("MoveY", Mathf.Abs(movement.y));

        float direction = Input.GetAxis("Horizontal");
        if (direction > 0 && facingLeft)
        {
            Flip();
        }
        else if (direction < 0 && !facingLeft)
        {
            Flip();
        }

    }
    void FixedUpdate()
    {
        transform.Translate(movement * speed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 characterScale = transform.localScale;
        characterScale.x *= -1;
        transform.localScale = characterScale;
    }

}
