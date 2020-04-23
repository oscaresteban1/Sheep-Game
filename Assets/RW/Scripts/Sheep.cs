using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{

    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay = false;
    public float dropDestroyDelay;

    private SheepSpawner sheepSpawner;
    private Collider myCollider;
    private Rigidbody myRigidbody;
    public float heartOffset;
    public GameObject heartPrefab;


    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }

    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0;

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);

        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();;
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay;
        tweenScale.timeToDestroy = gotHayDestroyDelay;

        Destroy(gameObject, gotHayDestroyDelay);

        SoundManager.Instance.PlaySheepHitClip();

        GameStateManager.Instance.SavedSheep();

    }
    private void Drop()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);

        SoundManager.Instance.PlaySheepDroppedClip();

        GameStateManager.Instance.DroppedSheep();
    }
    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}
