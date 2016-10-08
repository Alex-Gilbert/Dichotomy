using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Broadcaster
{
    private static Broadcaster _instance = null;
    private Broadcaster() { }
    public static Broadcaster Instance
    {
        get
        {
            if(_instance == null)
                _instance = new Broadcaster();
            return _instance;
        } 
    }

    private delegate void UnityAction(GameObject sender, float flag);
}
