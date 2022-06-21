using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController rubyController = collision.GetComponent<RubyController>();
        if (rubyController != null)
        {
            if(rubyController.getCurrentHealth()< rubyController.maxHealth)
            {
                rubyController.ChangeHealth(1);
                rubyController.PlaySound(collectedClip);
                Destroy(gameObject);    // gameobject是当前游戏对象
            }
            
        }
    }
}
