using System;
using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour
{
    public enum PlayerColor
    {
        Light,
        Dark
    }

    public enum PlayerDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    private PlayerDirection playerDir;
    public PlayerColor playerColor;

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

    private int ATTACK_UP_ID;
    private int ATTACK_DOWN_ID;
    private int ATTACK_LEFT_ID;
    private int ATTACK_RIGHT_ID;

    private int LIGHT_ID;
    private int DARK_ID;

    public float Player_Speed = 1;
    public CameraController cam;

    public Transform CenterCamTarget;
    public Transform BottomCamTarget;
    public Transform TopCamTarget;
    public Transform LeftCamTarget;
    public Transform RightCamTarget;

    public Transform[] Raycasters;
    public LayerMask WallLayerMask;

    public Animator LightHeroAnimator;
    public Animator DarkHeroAnimator;

    private SpriteRenderer renderer;
    public Texture lightTexture;
    public Texture darkTexture;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        renderer = this.GetComponentInChildren<SpriteRenderer>();

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


        ATTACK_UP_ID = Animator.StringToHash("Attack-Up");
        ATTACK_DOWN_ID = Animator.StringToHash("Attack-Down");
        ATTACK_LEFT_ID = Animator.StringToHash("Attack-Left");
        ATTACK_RIGHT_ID = Animator.StringToHash("Attack-Right");

        LIGHT_ID = Animator.StringToHash("Light");
        DARK_ID = Animator.StringToHash("Dark");
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        animator.SetLayerWeight(2, playerColor == PlayerColor.Light ? 1 : 0);
        animator.SetLayerWeight(1, playerColor == PlayerColor.Dark ? 1 : 0);

        float input_x = Input.GetAxisRaw("Horizontal");
        float input_y = Input.GetAxisRaw("Vertical");

        bool attacking = stateInfo.shortNameHash == ATTACK_UP_ID || stateInfo.shortNameHash == ATTACK_DOWN_ID ||
                         stateInfo.shortNameHash == ATTACK_LEFT_ID || stateInfo.shortNameHash == ATTACK_RIGHT_ID;
        bool walking = input_x != 0 || input_y != 0;
        

        if (walking && !attacking)
        {
            animator.SetBool(WALKING_ID, true);

            animator.SetBool(UP_ID, input_y == 1);
            animator.SetBool(DOWN_ID, input_y == -1);
            animator.SetBool(LEFT_ID, input_x == -1);
            animator.SetBool(RIGHT_ID, input_x == 1);

            Vector3 toMove = new Vector3(input_x, input_y);
            toMove.Normalize();

            toMove *= Player_Speed * Time.deltaTime;

            RaycastHit2D hit;
            
            foreach (var raycast in Raycasters)
            {
                hit = Physics2D.Raycast(raycast.position, toMove * 1.2f, toMove.magnitude * 1.2f, WallLayerMask);

                if (hit.collider != null)
                {
                    Debug.DrawLine(raycast.position, raycast.position + toMove * 1.2f, Color.red);
                    toMove = Vector3.zero;
                    break;
                }
                Debug.DrawLine(raycast.position, raycast.position + toMove * 1.2f, Color.green);
            }

            


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
            if(!attacking) cam.camTarget = CenterCamTarget;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeColor(playerColor == PlayerColor.Dark ? PlayerColor.Light : PlayerColor.Dark);
        }

        animator.SetBool(WALKING_ID, walking);
    }

    public void SetDirection(PlayerDirection dir)
    {
        switch (dir)
        {
            case PlayerDirection.Up:
                animator.SetBool(UP_ID, true);
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("hello");
        if (collision.gameObject.tag.Equals("Room"))
        {
            
        }
    }

    public void ChangeColor(PlayerColor pColor)
    {
        playerColor = pColor;
        
    }
}
