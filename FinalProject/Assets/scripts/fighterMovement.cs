using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fighterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float displacement;
    public Rigidbody2D fighter;
    Vector2 initial;
    public Animator animator;
    public float jump; // for jump


    void Start()
    {
        fighter = GetComponent<Rigidbody2D>();
        initial= fighter.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

            Debug.Log(transform.position);
        
        if ((Input.GetKey(KeyCode.RightArrow))){
                animator.SetFloat("speed",1.0f);
                initial.x=initial.x+displacement;
                
        }
        
        if (Input.GetKey(KeyCode.Space)){
            //for jump
            fighter.AddForce(new Vector2(fighter.velocity.x,jump));
            
        }
       
        if (!Input.anyKey)
            animator.SetFloat("speed",0);
        

            

        fighter.MovePosition(initial);
    }
}
