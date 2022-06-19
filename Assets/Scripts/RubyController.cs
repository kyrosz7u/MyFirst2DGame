using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed=2.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;

    // states
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;

    // position
    Rigidbody2D rigidbody2D;
    float horizontal;
    float vertical;

    // animator
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        invincibleTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        position.x = position.x + horizontal * Time.deltaTime * speed;
        position.y = position.y + vertical * Time.deltaTime * speed;

        Vector2 move = new Vector2(horizontal, vertical);
        animator.SetFloat("Look X", horizontal);
        animator.SetFloat("Look Y", vertical);
        animator.SetFloat("Speed", move.magnitude);

        // 引擎会检测是否能完成移动
        rigidbody2D.MovePosition(position);
    }

    void Launch()
    {
        GameObject projectileObject =Instantiate(
            projectilePrefab,
            rigidbody2D.position + Vector2.up * 0.5f,
            Quaternion.identity
            );
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
    }

    public void ChangeHealth(int amount)
    {
        // 受到上海的时候
        if (amount < 0)
        {
            if (isInvincible)
            {
                return ;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

    }
    public int getCurrentHealth()
    {
        return currentHealth;
    }
}
