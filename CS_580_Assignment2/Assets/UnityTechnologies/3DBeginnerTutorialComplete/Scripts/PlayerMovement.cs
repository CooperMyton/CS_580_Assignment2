using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public AudioClip wallHitSound; //added sound trigger effect
    public float wallHitCooldown = 0.5f;
    public Transform GoalArrow;
    public Transform GoalLocation;
    [SerializeField] private Vector3 arrowModelOffset = new Vector3(-90f,0f,0f);

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    float m_WallHitTimer;

    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);

        // update goal arrow to point towards the goal
        
        Vector3 goalVector = GoalLocation.position - m_Rigidbody.position;
        goalVector.y = 0f;
        if (goalVector.sqrMagnitude > 0.0001f)
        {
            goalVector.Normalize();
            Vector3 forward = GoalArrow.forward;
            forward.y = 0f;
            forward.Normalize();

            float dot = Mathf.Clamp(Vector3.Dot(forward, goalVector), -1f, 1f);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            float sign = Mathf.Sign(Vector3.Cross(forward, goalVector).y);

            if (Mathf.Abs(Vector3.Cross(forward, goalVector).y) > 0.0001f)
            {
                Quaternion arrowOffset = Quaternion.Euler(arrowModelOffset);
                Quaternion goalRotation = Quaternion.Euler(0f, angle * sign, 0f);
                GoalArrow.rotation = goalRotation * arrowOffset;
            }
        }
    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
    void Update()
    {
        if (m_WallHitTimer > 0)
        {
            m_WallHitTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && m_Movement.magnitude > 0 && m_WallHitTimer <= 0)
        {
            m_AudioSource.PlayOneShot(wallHitSound, 2f);
            m_WallHitTimer = wallHitCooldown;
        }
    }
}