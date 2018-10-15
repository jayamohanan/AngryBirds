using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : MonoBehaviour {
    public float impactSpeed;
    public Sprite deathSprite;
    public int hitPoints;

    float impactSpeedSqr;
    int currentHitPoints;
    SpriteRenderer spriteRenderer;
    new Collider2D collider2D;
    new Rigidbody2D rigidbody2D;
    void Start () {
        impactSpeedSqr = impactSpeed * impactSpeed;
        currentHitPoints = hitPoints;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Damager")
            return;
        if (collision.relativeVelocity.sqrMagnitude < impactSpeedSqr)
            return;
        currentHitPoints--;
        if(currentHitPoints<=0)
            Kill();

    }
    void Kill()
    {
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        rigidbody2D.isKinematic = true;
        
        
    }
}
