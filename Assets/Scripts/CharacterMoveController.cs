using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    
[Header("Movement")]
public float moveAccel;
public float maxSpeed;

[Header("Jump")]
public float jumpAccel;

[Header ("Terrain Raycast")]
public float terrainRaycastDistance;
public LayerMask terrainLayerMask;

private bool isJumping;
private bool isOnTerrain;

private Rigidbody2D rig;
private Animator anim;
private CharacterSoundController sound;

private void Start()
    {
      rig = GetComponent<Rigidbody2D>(); 
      anim = GetComponent<Animator>();
      sound = GetComponent<CharacterSoundController>();
    }

private void Update()
{
    //read input
    if (Input.GetMouseButtonDown(0))
   {
       if (isOnTerrain)
       {
        isJumping = true;

        
       }
           
   } 
    

    // change animation
    anim.SetBool("isOnTerrain", isOnTerrain);
}

private void FixedUpdate()
    {
        //raycast terrain
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, terrainRaycastDistance, terrainLayerMask);
    if (hit)
    {
        if (!isOnTerrain && rig.velocity.y <= 0)
        {
            isOnTerrain = true;
        }
    }
    else
    {
        isOnTerrain= false;
    }

    //calculate velocity vector
    Vector2 velocityVector = rig.velocity;

    if (isJumping)

    {

        velocityVector.y += jumpAccel;
        isJumping = false;



    }

    velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);
    rig.velocity = velocityVector;
    }
 private void OnDrawGizimos()
 {
     Debug.DrawLine(transform.position, transform.position + (Vector3.down * terrainRaycastDistance), Color.white);
 }
}


