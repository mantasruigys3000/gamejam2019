using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {

    public Rigidbody rb;
    public float speed_move = 4f;

    public float speed_move_max = 7f;
    public float speed_move_min = 4f;
    public float speed_move_multiplier = 0f;
    public float speed_move_dec = 0.5f;
    public float speed_move_inc = 0.1f;

    public Vector3 move_velocity = new Vector3(0,0,0);




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer){
            return;
        }

        if (moveControl())
        {
            if (speed_move + speed_move_inc <= speed_move_max)
            {
                speed_move += speed_move_inc;
            }
            else
            {
                speed_move = speed_move_max;
            }
        }
        else
        {
            if(speed_move - speed_move_dec >= speed_move_min)
            {
                speed_move -= speed_move_dec;
            }
            else
            {
                speed_move = speed_move_min;
                move_velocity = Vector3.zero;

            }

        }




	}

    bool moveControl()
    {
        if (rb == null)
        {
            Debug.Log("No RB found");
            return false;
        }

        float x_move = Input.GetAxisRaw("Horizontal");
        float z_move = Input.GetAxisRaw("Vertical");

        Vector3 x_velocity = x_move * rb.transform.right;
        Vector3 z_velocity = z_move * rb.transform.forward;

        if (0 < Mathf.Abs(x_move) || 0 < Mathf.Abs(z_move))
        {
            move_velocity = (x_velocity + z_velocity).normalized * speed_move;
        }

        rb.MovePosition(rb.position + (move_velocity * Time.deltaTime));

        return (0 < Mathf.Abs(x_move) || 0 < Mathf.Abs(z_move));

    }
}
