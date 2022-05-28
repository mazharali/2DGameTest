using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject game_area;
    public GameObject Enemy_prefab;

    public int ship_count = 0;
    public int ship_limit = 150;
    public int ships_per_frame = 1;

    public float spawn_circle_radius = 80.0f;
    public float death_circle_radius = 90.0f;

    public float fastest_speed = 12.0f;
    public float slowest_speed = 0.75f;

    void Start()
    {
        InstantiateEnemies();
    }

    void Update()
    {
        MaintainPopulation();
    }

    void InstantiateEnemies()
    {
       
        for (int i = 0; i < ship_limit; i++)
        {
            Vector3 position = GetRandomPosition(true);
            Enemy ship_script = AddEnemyShip(position);
            ship_script.transform.Rotate(Vector3.forward * Random.Range(0.0f, 360.0f));
        }
    }

    void MaintainPopulation()
    {
        
        if (ship_count < ship_limit)
        {
            for (int i = 0; i < ships_per_frame; i++)
            {
                Vector3 position = GetRandomPosition(false);
                Enemy ship_script = AddEnemyShip(position);
                ship_script.transform.Rotate(Vector3.forward * Random.Range(-45.0f, 45.0f));
            }
        }
    }

    Vector3 GetRandomPosition(bool within_camera)
    {

        Vector3 position = Random.insideUnitCircle;

        if (within_camera == false)
        {
            position = position.normalized;
        }

        position *= spawn_circle_radius;
        position += game_area.transform.position;

        return position;
    }

    Enemy AddEnemyShip(Vector3 position)
    {
        ship_count += 1;
        GameObject new_ship = Instantiate(
            Enemy_prefab,
            position,
            Quaternion.FromToRotation(Vector3.up, (game_area.transform.position - position)),
            gameObject.transform
        );

        Enemy ship_script = new_ship.GetComponent<Enemy>();
        ship_script.ship_spawner = this;
        ship_script.game_area = game_area;
        ship_script.speed = Random.Range(slowest_speed, fastest_speed);

        return ship_script;
    }
}
