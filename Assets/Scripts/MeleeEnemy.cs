using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int damageToGive;

    private Rigidbody2D rigid2D;

    public GameObject meleeWeapon, attackEffect;
    //public Collider2D boxCollider;
    public float weaponSpeed;

    public Animator anim;

    // Use this for initialization
    void Start()
    {
        rigid2D = transform.parent.GetComponent<Rigidbody2D>();

        attackEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{

            //for (int i = 1; i <= 90; i++)
            //{
            //    meleeWeapon.transform.Rotate(new Vector3(0, 0, 1));
            //}
            //meleeWeapon.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180, 0), weaponSpeed * Time.deltaTime);
            //meleeWeapon.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 45, 0), Time.deltaTime * weaponSpeed);
            //meleeWeapon.transform.localPosition = Vector2.Lerp(meleeWeapon.transform.localPosition, new Vector2(1, 0), 0.01f);
            //meleeWeapon.transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, timeCount);
            //timeCount = timeCount + Time.deltaTime;
        //}
        //if (Input.GetKeyUp(KeyCode.Mouse1))
        //{
        //    meleeWeapon.transform.Rotate(new Vector3(0, 0, -90));
        //    attackEffect.SetActive(false);
        //}
    }

    void Attack()
    {
        Debug.Log("Swing");
        anim.SetTrigger("Attack");
        //meleeWeapon.transform.Rotate(new Vector3(0, 0, 90));
        //attackEffect.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyAI2>().TakeDamage(damageToGive);
        }
    }
}
