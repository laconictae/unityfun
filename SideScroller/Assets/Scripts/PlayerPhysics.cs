using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

    public LayerMask collisionMask;
    private BoxCollider collider;
    private Vector3 s;
    private Vector3 c;

    private float skin = .005f;
    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool movementStopped;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        s = collider.size;
        c = collider.center;
    }

	public void Move(Vector2 moveAmount)
    {
        float deltaY = moveAmount.y;
        float deltaX = moveAmount.x;
        Vector2 p = transform.position;

        // up-down collision
        grounded = false;
        for (int i = 0; i < 3; i++)
        {
            float direction = Mathf.Sign(deltaY);
            float x = (p.x + c.x - s.x / 2) + s.x / 2 * i;
            float y = p.y + c.y + s.y / 2 * direction;

            ray = new Ray(new Vector2(x, y), new Vector2(0, direction));
            if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + skin, collisionMask))
            {
                float distance = Vector3.Distance(ray.origin, hit.point);

                if (distance > skin)
                {
                    deltaY = distance * direction - skin * direction;
                }
                else
                {
                    deltaY = 0;
                }

                grounded = true;
                break;
            }
        }

        // left-right collision
        movementStopped = false;
        for (int i = 0; i < 3; i++)
        {
            float direction = Mathf.Sign(deltaX);
            float x = p.x + c.x + s.x / 2 * direction;
            float y = p.y + c.y - s.y / 2 + s.y / 2 * i;

            ray = new Ray(new Vector2(x, y), new Vector2(direction, 0));
            if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaX) + skin, collisionMask))
            {
                float distance = Vector3.Distance(ray.origin, hit.point);

                if (distance > skin)
                {
                    deltaY = distance * direction - skin * direction;
                }
                else
                {
                    deltaX = 0;
                }
                movementStopped = true;
                break;
            }
        }

        var finalTransform = new Vector2(deltaX, deltaY);

        transform.Translate(finalTransform);
    }
}
