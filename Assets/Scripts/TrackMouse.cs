using UnityEngine;
using System.Collections;

public class TrackMouse : MonoBehaviour
{
    [SerializeField]
    private Rigidbody target;
    [SerializeField]
    private float scale;
    
    private Vector3 previousPosition;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
        var currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var velocity = currentPosition - previousPosition;

        target.velocity = velocity * scale;
        previousPosition = currentPosition;
    }
}
