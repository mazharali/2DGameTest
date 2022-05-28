using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySpawner ship_spawner;
    public GameObject game_area;

    public float speed;

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += transform.up * (Time.deltaTime * speed);

        float distance = Vector3.Distance(transform.position, game_area.transform.position);
        if (distance > ship_spawner.death_circle_radius)
        {
            RemoveShip();
        }
    }

    void RemoveShip()
    {
        Destroy(gameObject);
        ship_spawner.ship_count -= 1;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="Player")
        Debug.Log("GameObject2 collided with " + col.name);
    }

}
