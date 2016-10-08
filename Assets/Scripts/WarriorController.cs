using UnityEngine;
using System.Collections;

public class WarriorController : MonoBehaviour
{
    public PlayerController.PlayerColor Color;

    private PlayerController.PlayerDirection warriorDirection;

    public Transform player;
    public Transform[] Raycasters;
    public LayerMask WallMask;
    

    private Animator _anim;


	// Use this for initialization
	void Start ()
	{
	    _anim = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
