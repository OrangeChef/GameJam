using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{

    [Header("Bouncing")]
    public float bounceForce = 8f;
    public int mouseButton = 0;

    [Header("Ground Checking")]
    public LayerMask groundMask;

    private Rigidbody2D parentBody;
    private bool canBounce;
    private bool hasBounced;

    void Start()
    {
        parentBody = GetComponentInParent<Rigidbody2D>();

        if (parentBody == null)
        {
            Destroy(this);
            return;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(mouseButton) && canBounce && !hasBounced)
        {
            parentBody.velocity = transform.up * bounceForce;
            hasBounced = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        canBounce = true;
        hasBounced = false;
        //if (collision.gameObject.layer.Equals(groundMask))
        //{
        //}
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        canBounce = true;
        //if (collision.gameObject.layer.Equals(groundMask))
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        canBounce = false;
        //if (collision.gameObject.layer.Equals(groundMask))
    }
}
