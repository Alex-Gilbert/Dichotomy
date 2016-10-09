using UnityEngine;
using System.Collections;

public class WarriorController : MonoBehaviour
{
    public PlayerController.PlayerColor Color;

    private PlayerController.PlayerDirection warriorDirection;

    private Transform player;
    public Transform[] Raycasters;
    public LayerMask WallMask;


    private Animator _anim;

    private bool inCircle = false;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 toPlayer = player.position - transform.position;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

    }
}
