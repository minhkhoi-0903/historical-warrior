using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemy_china : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject mySword;
    [SerializeField] private bool MoveTowardsPlayer;
    [SerializeField] private float Hpnow;
    [SerializeField] private float MaxHp = 10;
    [SerializeField] private float timeToDestroy;
    //[SerializeField] private float IsDestroy;

    /*public delegate void EnemyDeathAction();
    public event EnemyDeathAction OnEnemyDeath;*/

    Vector3 movement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        mySword = GameObject.FindGameObjectWithTag("enemy1").gameObject;

        speed = 3f;
        MoveTowardsPlayer = false;
        timeToDestroy = 3f;
        //IsDestroy = 0;

        Hpnow = MaxHp;

        this.gameObject.SetActive(true);
        mySword.SetActive(true);
    }

    void Update()
    {
        /*if (isFacingRight == true && movement.x == -1)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
            isFacingRight = false;
        }

        if(isFacingRight == false && movement.x == 1)
        {
            transform.localScale = new Vector3(1f,1f,1f);
            isFacingRight = true;
        }*/

        if (MoveTowardsPlayer == true)
        {
            _MoveTowardsPlayer();
        }

        if (Hpnow <= 0)
        {
            animator.SetTrigger("death");
            speed = 0f;
            timeToDestroy -= Time.deltaTime;
        } 

        if (timeToDestroy <= 0)
        {            
            destroy();
        }
    }

    void _MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);

            // Kiểm tra và cập nhật trạng thái animation
            UpdateAnimationState(direction);
        }
    }

    void UpdateAnimationState(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        animator.SetFloat("Angle", angle);

        // Cập nhật trạng thái chạy (Run) trong Animator
        if (direction.magnitude > 0)
        {
            animator.SetBool("Is_running", true);
        }
        else
        {
            animator.SetBool("Is_running", false);
        }
    }

    void destroy()
    {
        this.gameObject.SetActive(false);
        mySword.SetActive(false);
        enemyIsDestroy();    
    }

    public void enemyIsDestroy(float IsDestroy = 0)
    {
        IsDestroy += 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            MoveTowardsPlayer = true;
        }

        if (collision.CompareTag("katana"))
        {
            Hpnow -= 5;
        }

        if (collision.CompareTag("skill_dmg_okita"))
        {
            Hpnow -= 8;
        }
    }
}
