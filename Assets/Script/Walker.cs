﻿using UnityEngine;

public class Walker : MonoBehaviour, ITakeShellHits
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private GameObject spawnOnStompPrefab;

    private new Collider2D collider;
    private new Rigidbody2D rigidbody2D;
    private SpriteRenderer sprite;

    private Vector2 direction = Vector2.left;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + direction * speed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        if (ReachedEdge() || HitNotPlayer())
        {
            SwitchDirection();
        }
    }
    

    private bool HitNotPlayer()
    {
        float x = GetForwardX();
        float y = transform.position.y;

        Vector2 origin = new Vector2(x, y);
        Debug.DrawRay(origin, direction * 0.1f);

        var hit = Physics2D.Raycast(origin, direction, 0.1f);

        if (hit.collider == null)
            return false;

        if (hit.collider.isTrigger)
            return false;

        if (hit.collider.GetComponent<PlayerMovementController>() != null)
            return false;

        return true;
    }

    private bool ReachedEdge()
    {
        float x = GetForwardX();

        float y = collider.bounds.min.y;

        Vector2 origin = new Vector2(x, y);
        Debug.DrawRay(origin, Vector2.down * 0.1f);

        var hit = Physics2D.Raycast(origin, Vector2.down, 0.1f);

        if (hit.collider == null)
            return true;

        return false;
    }

    private float GetForwardX()
    {
        return direction.x == -1 ?
               collider.bounds.min.x - 0.1f :
               collider.bounds.max.x + 0.1f;
    }

    private void SwitchDirection()
    {
        direction *= -1;
        sprite.flipX = !sprite.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.WasHitByPlayer()) 
        {
            if (collision.WasTop())
                HandleWalkerStomp(collision.collider.GetComponent<PlayerMovementController>());
            else
                GameManager.Instance.KillPlayer();
        }
    }

    private void HandleWalkerStomp(PlayerMovementController playerMovementController)
    {
        if (spawnOnStompPrefab != null)
        {
            Instantiate(spawnOnStompPrefab, transform.position, transform.rotation);
        }

        playerMovementController.Bounce();


        Destroy(gameObject);
    }

    public void HandleShellHit(ShellFlipped shellFlipped)
    {
        Destroy(gameObject);
    }
}
