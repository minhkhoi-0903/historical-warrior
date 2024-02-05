using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_china_katana : MonoBehaviour
{
    [SerializeField] private Transform PlayerOkita;
    [SerializeField] private GameObject enemyChina;
    [SerializeField] private float distance;
    [SerializeField] private float speed = 10f;

    void Start()
    {
        PlayerOkita = GameObject.FindGameObjectWithTag("Player").transform;
        this.gameObject.SetActive(true);
        //enemyChina = GameObject.FindGameObjectWithTag("enemyChar").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RotateSword();

        bool IsActive = enemyChina.activeSelf;

        distance = Vector2.Distance(transform.position, enemyChina.transform.position);
        Vector2 direction = enemyChina.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, enemyChina.transform.position, speed * Time.deltaTime);

        if (enemyChina != null && !IsActive)
        {
            this.gameObject.SetActive(false);
        }
    }

    void RotateSword()
    {
        //Vector3 PlayerOkita = Camera.main.ScreenToWorldPoint(Input.PlayerOkita);
        Vector2 lookDir = PlayerOkita.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
    }
}
