using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTruck : MonoBehaviour
{
    public float Speed;
    public float Rotate = 3;
    public float Boost;
    public float BoostCoolDown = 2;
    public float TimeFromBoost;
    public float BoostCoolDownLeft;
    public bool Boosting;
    public float ReduceSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        List<float> Temp = DataManager.Data.GetStats();
        Speed = Temp[0];
        Boost = Temp[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float BoostMod = 1;
        //Want to have movement that is forward and turn based only
        float forwardMovement = Input.GetAxis("Vertical");
        float rotateMovement = -Input.GetAxis("Horizontal");
        //Debug.Log("ForwardMovement = " + forwardMovement);
        //Logic For Boosting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Starting Boost Logic Check");
            if (TimeFromBoost < Boost && Time.time > BoostCoolDownLeft + BoostCoolDown)
            {
                Debug.Log("Setting Boost to True");
                Boosting = true;
            }
            else if (TimeFromBoost > Boost)
            {
                //When TimeFromsprint reaches its max reset it and set up the cooldown
                BoostCoolDownLeft = Time.time;
                TimeFromBoost = 0;
                Boosting = false;
            }
            else
            {
                Boosting = false;
            }
        }
        else
        {
            Boosting = false;
        }
        if (Boosting)
        {
            Debug.Log("Boost is being applied");
            BoostMod = 2;
            TimeFromBoost += Time.deltaTime;

        }
        //Physics-Based Movement
        Rigidbody2D rB = gameObject.GetComponent<Rigidbody2D>();
        if (forwardMovement <= 0)
        {
            forwardMovement = 0;
            rB.velocity = rB.velocity / 1.1f;

        }
        if (rotateMovement == 0)
            rB.angularVelocity = rB.angularVelocity / 1.2f;

        //Logic to either Display or stop displaing the thrustors outputs
        //This will be an animation on a child gameobject

        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMaster>().Pause != true)
        {
            rB.AddForce(transform.right * forwardMovement * (Speed / ReduceSpeed) * BoostMod);
            rB.AddTorque(rotateMovement * Rotate);
        }
        
    }
}
