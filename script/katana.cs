using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katana : MonoBehaviour
{

    [SerializeField] private float atkSpeed;

    [SerializeField] private float damage;

    [SerializeField] private Animator anim;

    public bool atk_katana;

    float TimeUntilAtk;

    void Start()
    {
        atkSpeed = 1f;
        anim = GetComponent<Animator>();
        transform.localScale = new Vector2(0.1f,0.1f);
        anim.SetBool("kiri", false);

        //DelayAtk = timeBtwAtk;

        atk_katana = false;
    }

    void Update()
    {
        RotateSword();

        if(Input.GetKeyDown(KeyCode.Space) && TimeUntilAtk <= 0f)
        {
            anim.SetBool("kiri", true);
            TimeUntilAtk = atkSpeed;
            transform.localScale = new Vector2(0.5f, 0.5f);
            atk_katana = true;
        }
        else
        {
            TimeUntilAtk -= Time.deltaTime;
        }
    }

    public void EndATK()
    {
        anim.SetBool("kiri", false);
        atk_katana = false;
        transform.localScale = new Vector2(0.1f, 0.1f);
    }

    void RotateSword()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if (atk_katana == false)
        { 
        Vector2 scale = transform.localScale;
        if (lookDir.x < 0)
        {
            scale.y = -0.1f;
        }
        else if (lookDir.x > 0)
        {
            scale.y = 0.1f;
        }
        transform.localScale = scale;
        }else
        {
            Vector2 scale = transform.localScale;
        if (lookDir.x < 0)
        {
            scale.y = -0.5f;
        }
        else if (lookDir.x > 0)
        {
            scale.y = 0.5f;
        }
        transform.localScale = scale;
        }
    }
}
