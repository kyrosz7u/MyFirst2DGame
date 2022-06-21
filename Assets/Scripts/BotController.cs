using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public float speed=2.0f;
    public float xlimits=5.0f;
    public float ylimits = 5.0f;
    public ParticleSystem smokeEffect;

    float changeTime = 0.5f;

    // states
    float timer;
    bool broken= true;

    // position
    Rigidbody2D rigidbody2D;
    float xzero;
    float yzero;
    float horizontal;
    float vertical;

    // animator
    Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;

        Vector2 position = transform.position;
        xzero = position.x;
        yzero = position.y;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }

        if (timer < 0)
        {
            timer = changeTime;
            horizontal = Random.Range(-1f, 1f);
            if (horizontal > -0.1&&horizontal<0.1)
            {
                horizontal = 0;
            }
            vertical = Random.Range(-1f, 1f);
            if (vertical > -0.1&&vertical<0.1)
            {
                vertical = 0;
            }
        }
        timer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = transform.position;

        position.x = position.x + horizontal * Time.deltaTime * speed;
        position.y = position.y + vertical * Time.deltaTime * speed;

        position.x = Mathf.Clamp(position.x, xzero - xlimits, xzero+xlimits);
        position.y = Mathf.Clamp(position.y, yzero-ylimits, yzero+ylimits);

        Vector2 move = new Vector2(position.x, position.y);

        animator.SetFloat("MoveX", horizontal);
        animator.SetFloat("MoveY", vertical);
        //animator.SetFloat("Speed", move.magnitude);

        // 引擎会检测是否能完成移动
        rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }

}
