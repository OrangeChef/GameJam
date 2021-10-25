using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{

    [Header("Bouncing")]
    public float bounceForce = 8f;
    public int mouseButton = 0;

    [Space]
    public Color offColor;
    public Color onColor = Color.white;

    [Header("Ground Checking")]
    public int bouncableLayer = 0;

    private Rigidbody2D parentBody;
    private SpriteRenderer sprite;
    private bool canBounce;
    private bool hasBounced;

    void Start()
    {
        parentBody = GetComponentInParent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (parentBody == null)
        {
            Destroy(this);
            return;
        }
    }

    void Update()
    {
        sprite.color = Input.GetMouseButton(mouseButton) ? onColor : offColor;

        if (Input.GetMouseButton(mouseButton) && canBounce && !hasBounced)
        {
            parentBody.velocity = transform.up * bounceForce;
            hasBounced = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.layer.Equals(bouncableLayer))
            return;

        canBounce = true;
        hasBounced = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.layer.Equals(bouncableLayer))
            return;

        canBounce = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        canBounce = false;
    }

    public IEnumerator ResetBounceAbility(float delay)
    {
        canBounce = false;
        yield return new WaitForSeconds(delay);
    }
}
