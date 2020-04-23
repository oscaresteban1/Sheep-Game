using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHay : MonoBehaviour
{

    public GameObject hayPrefab;
    public Transform haySpawnpoint;

    public float movementSpeed;
    public float horizontalBoundary = 22;
    public float shootInterval;
    private float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float HorizontalInput = Input.GetAxisRaw("Horizontal");

        if (HorizontalInput < 0 && transform.position.x > -horizontalBoundary) {
            transform.Translate(-transform.right * movementSpeed * Time.deltaTime);
        }
        else if (HorizontalInput > 0 && transform.position.x < horizontalBoundary) {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }

        UpdateShooting();
    }

    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            ShootHay();
        }
    }

    private void ShootHay()
    {
        Instantiate(hayPrefab, haySpawnpoint.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();
    }
}
