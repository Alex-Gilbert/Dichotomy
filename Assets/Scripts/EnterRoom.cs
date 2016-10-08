using UnityEngine;
using System.Collections;

public class EnterRoom : MonoBehaviour
{
    public Transform Room;
    public Transform Room0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Room.gameObject.SetActive(true);
        }
    }
}
