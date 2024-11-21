using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject toSpawn;
    public GameObject bear;
    private GameObject obj_spawned;
    private int rand;
    public float start_z = 10;

    public GameObject small_bridge;
    public GameObject medium_bridge;
    public GameObject rope_bridge;
    public GameObject ramped_bridge;
    public GameObject wide_fence;
    public GameObject coins_fence_left;
    public GameObject coins_fence_right;
    public GameObject double_fence;
    public GameObject coins_left;
    public GameObject coins_right;
    public GameObject coins_middle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bear.transform.position.z + 100 > start_z)
        {
            rand = Random.Range(0, 13);
            if(rand == 0)
            {
                obj_spawned = Instantiate(small_bridge, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 10;
            }
            if (rand == 1)
            {
                obj_spawned = Instantiate(medium_bridge, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 30;
            }
            if (rand == 2)
            {
                obj_spawned = Instantiate(rope_bridge, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 17.5f;
                obj_spawned = Instantiate(small_bridge, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 10;
            }
            if (rand == 3)
            {
                obj_spawned = Instantiate(ramped_bridge, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 33.8f;
            }
            if (rand == 4)
            {
                obj_spawned = Instantiate(wide_fence, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 20;
            }
            if (rand == 5)
            {
                obj_spawned = Instantiate(coins_fence_left, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 20;
                obj_spawned = Instantiate(small_bridge, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 10;
            }
            if (rand == 5)
            {
                obj_spawned = Instantiate(coins_fence_right, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 20;
                obj_spawned = Instantiate(small_bridge, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 10;
            }
            if (rand == 6)
            {
                obj_spawned = Instantiate(double_fence, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 20;
                obj_spawned = Instantiate(small_bridge, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 10;
            }
            if (rand >= 7 && rand <= 8)
            {
                obj_spawned = Instantiate(coins_left, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 20;
            }
            if (rand >= 9 && rand <= 10)
            {
                obj_spawned = Instantiate(coins_right, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 20;
            }
            if (rand >= 11 && rand <= 12)
            {
                obj_spawned = Instantiate(coins_middle, new Vector3(0, 0, start_z), Quaternion.identity);
                Destroy(obj_spawned, 100.0f);
                start_z += 20;
            }
        }
    }
}
