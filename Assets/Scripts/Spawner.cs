using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject SpawnObject;
    public float SpawnCooldown = 10f;

    private float spawnTimer;

    void Awake()
    {
        this.spawnTimer = SpawnCooldown;
    }

    void Update()
    {
        this.spawnTimer -= Time.deltaTime;
        this.spawnTimer = Mathf.Max(this.spawnTimer, 0);

        if (this.spawnTimer <= 0)
        {
            Spawn();
            this.spawnTimer = this.SpawnCooldown;
        }
    }

    private void Spawn()
    {
        Instantiate(this.SpawnObject, this.transform.position, Quaternion.identity);
    }
}
