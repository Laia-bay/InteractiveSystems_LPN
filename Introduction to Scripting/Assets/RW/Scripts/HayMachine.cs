using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    //movement of the machine (speed + boundaries)
    public Vector3 movementSpeed;
    public float limitX = 22;

    //shooting hay bales
    public GameObject hayBalePrefab;
    public Transform haySpawnpoint;
    public float shootInterval;
    private float shootTimer;

    //changing color of machine
    public Transform modelParent;

    public GameObject blueModelPrefab;
    public GameObject redModelPrefab;
    public GameObject yellowModelPrefab;
    

    
    // Start is called before the first frame update
    void Start()
    {
        LoadModel();
    }

    //selecting color m¡of machine
    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject); // 1

        switch (GameSettings.hayMachineColor) // 2
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
            break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
            break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateShooting();
    }

    //movement machine
    private void UpdateMovement()
    {

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= limitX)
        {
            transform.Translate(movementSpeed*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -limitX)
        {
            transform.Translate(movementSpeed*Time.deltaTime*-1);
        }
    }

    //configuration of shooting hay bales
    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            ShootHay();
        }
    }

    //shooting hay bales
    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
        
        SoundManager.Instance.PlayShootClip();
    }
}
