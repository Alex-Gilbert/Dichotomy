using System;
using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour
{
    public enum PlayerDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    private PlayerDirection playerDir;

    private Animator animator;

    private int WALKING_ID;
    private int UP_ID;
    private int DOWN_ID;
    private int LEFT_ID;
    private int RIGHT_ID;

    private int WALKING_UP_ID;
    private int WALKING_DOWN_ID;
    private int WALKING_LEFT_ID;
    private int WALKING_RIGHT_ID;

    private int IDLE_UP_ID;
    private int IDLE_DOWN_ID;
    private int IDLE_LEFT_ID;
    private int IDLE_RIGHT_ID;

    public float Player_Speed = 1;
    public CameraController cam;

    public Transform CenterCamTarget;
    public Transform BottomCamTarget;
    public Transform TopCamTarget;
    public Transform LeftCamTarget;
    public Transform RightCamTarget;
    // Use this for initialization
    void Start ()
	{
	    animator = this.GetComponent<Animator>();

        WALKING_ID = Animator.StringToHash("Walking");
        UP_ID = Animator.StringToHash("Up");
        DOWN_ID = Animator.StringToHash("Down");
        LEFT_ID = Animator.StringToHash("Left");
        RIGHT_ID = Animator.StringToHash("Right");

        WALKING_UP_ID = Animator.StringToHash("Walk-Up");
        WALKING_DOWN_ID = Animator.StringToHash("Walk-Down");
        WALKING_LEFT_ID = Animator.StringToHash("Walk-Left");
        WALKING_RIGHT_ID = Animator.StringToHash("Walk-Right");

        IDLE_UP_ID = Animator.StringToHash("Idle-Up");
        IDLE_DOWN_ID = Animator.StringToHash("Idle-Down");
        IDLE_LEFT_ID = Animator.StringToHash("Idle-Left");
        IDLE_RIGHT_ID = Animator.StringToHash("Idle-Right");
    }
	
	// Update is called once per frame
	void Update ()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

	    float input_x = Input.GetAxisRaw("Horizontal");
	    float input_y = Input.GetAxisRaw("Vertical");

	    bool walking = input_x != 0 || input_y != 0;

	    if (walking)
	    {
            animator.SetBool(WALKING_ID, true);

            animator.SetBool(UP_ID, input_y == 1);
            animator.SetBool(DOWN_ID, input_y == -1);
            animator.SetBool(LEFT_ID, input_x == -1);
            animator.SetBool(RIGHT_ID, input_x == 1);

            Vector3 toMove = new Vector3(input_x, input_y);
            toMove.Normalize();

            toMove *= Player_Speed*Time.deltaTime;

            transform.Translate(toMove);

	        if (stateInfo.shortNameHash == WALKING_DOWN_ID)
	        {
	            playerDir = PlayerDirection.Down;
	            cam.camTarget = BottomCamTarget;
	        }
	        else if (stateInfo.shortNameHash == WALKING_UP_ID)
	        {
	            playerDir = PlayerDirection.Up;
	            cam.camTarget = TopCamTarget;
	        }
	        else if (stateInfo.shortNameHash == WALKING_LEFT_ID)
	        {
	            playerDir = PlayerDirection.Left;
	            cam.camTarget = LeftCamTarget;
	        }
	        else if (stateInfo.shortNameHash == WALKING_RIGHT_ID)
	        {
	            playerDir = PlayerDirection.Right;
	            cam.camTarget = RightCamTarget;
	        }
	    }
	    else
	    {
            SetDirection(playerDir);
            animator.SetBool(WALKING_ID, false);
	        cam.camTarget = CenterCamTarget;
	    }

        animator.SetBool(WALKING_ID, input_x != 0 || input_y != 0);
        
    }

    public void SetDirection(PlayerDirection dir)
    {
        switch (dir)
        {
            case PlayerDirection.Up:
                animator.SetBool(UP_ID,true);
                animator.SetBool(DOWN_ID, false);
                animator.SetBool(LEFT_ID, false);
                animator.SetBool(RIGHT_ID, false);
                break;
            case PlayerDirection.Down:
                animator.SetBool(UP_ID, false);
                animator.SetBool(DOWN_ID, true);
                animator.SetBool(LEFT_ID, false);
                animator.SetBool(RIGHT_ID, false);
                break;
            case PlayerDirection.Left:
                animator.SetBool(UP_ID, false);
                animator.SetBool(DOWN_ID, false);
                animator.SetBool(LEFT_ID, true);
                animator.SetBool(RIGHT_ID, false);
                break;
            case PlayerDirection.Right:
                animator.SetBool(UP_ID, false);
                animator.SetBool(DOWN_ID, false);
                animator.SetBool(LEFT_ID, false);
                animator.SetBool(RIGHT_ID, true);
                break;
        }
    }
}
