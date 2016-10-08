using UnityEngine;
using System.Collections;

public class FloorScript : MonoBehaviour
{
    public Transform Room0;
    public Transform[] Rooms;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Room0.gameObject.SetActive(true);
            foreach (var room in Rooms)
            {
                room.gameObject.SetActive(false);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Room0.gameObject.SetActive(false);
        }
    }
}
