using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour {

    private float speed;
    private float maxSpeed;
    private Vector2 targetPos;

    public float Speed {
        get { return speed; }
        set { speed = value; }
    }

    public float MaxSpeed {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }

    public Vector2 TargetPos {
        get { return targetPos; }
        set { targetPos = value; }
    }
}
