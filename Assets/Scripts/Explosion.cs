using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage;
    public GameObject weapon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(damage);
        }
        else if (other.tag == "Shooter")
        {
            other.GetComponent<ShooterAI>().TakeDamage(damage);
        }
        else if (other.tag == "Runner")
        {
            other.GetComponent<RunnerAI>().TakeDamage(damage);
        }
        else if (other.tag == "Boss")
        {
            other.GetComponent<DrBananas>().TakeDamage(damage);
        }
    }
}
