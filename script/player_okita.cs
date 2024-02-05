using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;

public class player_okita : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private okita_hp Hp;
    [SerializeField] private okita_mana Mana;
    [SerializeField] private okita_armor giap;
    [SerializeField] private bool isFacingRight;
    [SerializeField] private bool atk_katana;
    [SerializeField] private bool hetGiap;
    [SerializeField] private float speed;
    //[SerializeField] private float timeArmor = 4f;
    [SerializeField] private float timeSkill = 10f;
    [SerializeField] private float luongmautoida = 4;
    [SerializeField] private float luongmauhientai = 4;
    [SerializeField] private float luongGiapToida = 4;
    [SerializeField] private float luongGiapHientai = 4;
    [SerializeField] private float luongmanatoida = 180;
    [SerializeField] private float luongmanahientai = 180;
    [SerializeField] private Vector2 movement;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject skill_atk_area;
    [SerializeField] private Text HpText;
    [SerializeField] private Text ManaText;
    [SerializeField] private Text ArmorText;
    [SerializeField] private Text timeSkillText;
    [SerializeField] private GameObject dialogue_start;

    void Start()
    {
        speed = 5;
        anim.SetBool("skill", false);

        luongmauhientai = luongmautoida;
        Hp.capnhatthanhmau(luongmauhientai,luongmautoida);
        HpText.text = "4/" + luongmauhientai; //Hp setup

        luongmanahientai = luongmanatoida;
        Mana.capnhatthanhmana(luongmanahientai,luongmanatoida);
        ManaText.text = "180/" + luongmanahientai; // mana setup

        luongGiapHientai = luongGiapToida = 4;
        giap.capnhatthanhGiap(luongGiapHientai,luongGiapToida);
        ArmorText.text = "4/" + luongGiapHientai; // armor setup

        timeSkillText.text = "10/" + timeSkill;

        skill_atk_area.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        dialogue_start.SetActive(true);
        Time.timeScale = 0f;

        hetGiap = false;

        anim.SetBool("take_dmg", false);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("speed", movement.magnitude);

        luongGiapHientai += Time.deltaTime;
        giap.capnhatthanhGiap(luongGiapHientai,luongGiapToida);
        ArmorText.text = "4/" + luongGiapHientai;

        if (isFacingRight == true && movement.x == -1)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
            isFacingRight = false;
        }

        if(isFacingRight == false && movement.x == 1)
        {
            transform.localScale = new Vector3(1f,1f,1f);
            isFacingRight = true;
        }

        if (Input.GetKeyDown(KeyCode.Z) && timeSkill >= 10)
        {
            _skill1();
            //timeSkill = 0.001f;
        }

        if (luongmauhientai <= 0)
        {
            _death();
        }

        if (timeSkill > 10)
        {
            timeSkill = 10;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            dialogue_start.SetActive(false);
        }

        if (luongGiapHientai <= 0)
        {
            luongGiapHientai = 0;
            hetGiap = true;
            giap.capnhatthanhGiap(luongGiapHientai,luongGiapToida);
            ArmorText.text = "4/" + luongGiapHientai;
        }
        if (luongGiapHientai > 0)
        {
            hetGiap = false;
        }
        if (luongGiapHientai >= 4)
        {
            luongGiapHientai = 4;
            giap.capnhatthanhGiap(luongGiapHientai,luongGiapToida);
            ArmorText.text = "4/" + luongGiapHientai;
        }

        timeSkillText.text = "10/" + timeSkill;
        timeSkill += Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position+movement*speed*Time.fixedDeltaTime); //movement
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("map"))
        {
            //SceneManager.LoadScene(1);
        }

        if (collision.CompareTag("enemy1"))
        {
            if (hetGiap)
            {
                InvokeRepeating("_take_DMG_1", 0, 0.3f);
                luongmauhientai -= 1;
                Hp.capnhatthanhmau(luongmauhientai,luongmautoida);
                HpText.text = "4/" + luongmauhientai;
                _take_DMG_1();
            }
            else
            {
                luongGiapHientai -= 1;
                giap.capnhatthanhGiap(luongGiapHientai,luongGiapToida);
                ArmorText.text = "4/" + luongGiapHientai;
                _take_DMG_1();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy1"))
        {
            CancelInvoke("_take_DMG_1");
        }
    }

    public void _skill1()
    {
        anim.SetBool("skill", true);
        skill_atk_area.SetActive(true);

        luongGiapHientai = 4;
        giap.capnhatthanhGiap(luongGiapHientai,luongGiapToida);
        ArmorText.text = "4/" + luongGiapHientai;

        timeSkill = 0;
    }

    public void _endSkill()
    {
        anim.SetBool("skill", false);
        skill_atk_area.SetActive(false);

        luongGiapHientai = 4;
        giap.capnhatthanhGiap(luongGiapHientai,luongGiapToida);
        ArmorText.text = "4/" + luongGiapHientai;
    }

    public void _death()
    {
        //Time.timeScale = 0f;
        anim.SetTrigger("death");
    }

    public void _take_DMG_1()
    {
        anim.SetBool("take_dmg", true);

        /*luongGiapHientai -= 1;
        giap.capnhatthanhGiap(luongGiapHientai,luongGiapToida);
        ArmorText.text = "4/" + luongGiapHientai;*/
    }

    public void x_take_DMG()
    {
        anim.SetBool("take_dmg", false);
    }
}
