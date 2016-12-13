using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Windows.Kinect;

public class Level5Interaction : LevelInteration
{
    [SerializeField]
    private GameObject gift;
    [SerializeField]
    private float maxY = 5f;
    [SerializeField]
    [Range(10f, 10000f)]
    private float scale = 10f;
    [SerializeField]
    private Text debugText;

    // Update is called once per frame
    void Update()
    {
        // the number of users with hands above heads
        var countAbove = 0f;
        foreach (var body in BodyManager.Bodies)
        {
            if (body.Position(JointType.HandLeft).y > body.Position(JointType.Head).y
                && body.Position(JointType.HandRight).y > body.Position(JointType.Head).y)
            {
                countAbove++;
            }
        }
        debugText.text = "Users above: " + countAbove;
        if (countAbove / (float)BodyManager.Bodies.Count > (float)BodyManager.Bodies.Count / 2f)
        {
            // more than half
            gift.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, scale * Time.deltaTime));
            debugText.text += " Force: " + scale * Time.deltaTime;
        }

    }
}
