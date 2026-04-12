using UnityEngine;

public class GoalArrowPoint : MonoBehaviour
{
    // need to follow the player position, so take it in as an arg
    // originally just had the arrow as a child of the player, but that caused issues with following the player rotation
    public Transform player;
    public Transform GoalLocation;
    [SerializeField] private Vector3 arrowModelOffset = new Vector3(0f,90f,90f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Make sure the arrow follows the player, offset to float above their head
        transform.position = new Vector3(player.position.x,player.position.y + 1.5f, player.position.z);

        // update goal arrow to point towards the goal
        
        Vector3 goalVector = GoalLocation.position - transform.position;
        goalVector.y = 0f;
        if (goalVector.sqrMagnitude > 0.0001f)
        {
            goalVector.Normalize();
            Vector3 forward = transform.forward;
            forward.y = 0f;
            forward.Normalize();

            float dot = Mathf.Clamp(Vector3.Dot(forward, goalVector), -1f, 1f);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            float sign = Mathf.Sign(Vector3.Cross(forward, goalVector).y);
            Quaternion aimRotation = Quaternion.Euler(0f,angle*sign,0f);
            transform.rotation = aimRotation * Quaternion.Euler(arrowModelOffset);
        }
        
    }
}
