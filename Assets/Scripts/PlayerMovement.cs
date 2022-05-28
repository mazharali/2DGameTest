using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public SpriteRenderer playerSprite;
    public GameObject restartBtn;

    public float rotationOffSet;
    public GameObject ObjExplosion;
    AudioSource AS;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    void Update()
    {

        Vector3 mousePos=Input.mousePosition;
        mousePos.z = 0;


        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffSet));

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            playerSprite.enabled = false;
            ObjExplosion.transform.position = transform.position;
            ObjExplosion.SetActive(true);
            AS.Play();
            GetComponent<CircleCollider2D>().enabled = false;
            restartBtn.SetActive(true);
        }
    }



}
