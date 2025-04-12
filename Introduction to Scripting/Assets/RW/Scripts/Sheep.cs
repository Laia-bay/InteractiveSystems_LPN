using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    //sheep properties (speed, delay after getting hay, "eating" hay)
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;

    //dropping sheep when not saved
    public float dropDestroyDelay;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    //spawning the sheep
    private SheepSpawner sheepSpawner;

    //showing the heart
    public float heartOffset;
    public GameObject heartPrefab;

    //to ensure only 1 drop and save per sheep
    private bool hasDropped = false;
    private bool hasSaved = false;

    // Start is called before the first frame update
    void Start()
    {
        //defining the sheep dropper object
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    //stop sheep when hit by hay + hay and sheep dissapear + heart shows
    private void HitByHay()
    {
        if (hasSaved)
        {    
            return;
        }
        hasSaved = true;

        sheepSpawner.RemoveSheepFromList(gameObject);
        GameStateManager.Instance.SavedSheep();

        hitByHay = true;
        runSpeed = 0;

        Destroy(gameObject, gotHayDestroyDelay);

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();;
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay; 

        SoundManager.Instance.PlaySheepHitClip();

        
    }

    //trigger activated when the object is hay or when the object is the dropper
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }

        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }

    //drop sheep
    private void Drop()
    {
        if (hasDropped)
        {    
            return;
        }

        hasDropped = true;
        GameStateManager.Instance.DroppedSheep();
        sheepSpawner.RemoveSheepFromList(gameObject);
        
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);

        SoundManager.Instance.PlaySheepDroppedClip();
    }

    //spawning sheep reference
    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}
