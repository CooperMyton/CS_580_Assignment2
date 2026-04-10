using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    // Original Fields
    public Transform player;
    public GameEnding gameEnding;

    // Detection Light Fields
    public Light detectionLight;
    public Color safeColor = Color.green;
    public Color dangerColor = Color.red;
    public float colorLerpSpeed = 3f;
    public float warningDistance = 8f;

    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        bool playerSpotted = false;

        // Calculate distance between enemy and player every frame
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    playerSpotted = true;
                    gameEnding.CaughtPlayer();
                }
            }
        }

        Color targetColor;

        if (playerSpotted)
        {
            targetColor = dangerColor;          // Red — caught
        }
        else if (distanceToPlayer < warningDistance)
        {
            targetColor = Color.yellow;         // Yellow — getting close
        }
        else
        {
            targetColor = safeColor;            // Green — safe
        }

        detectionLight.color = Color.Lerp(
            detectionLight.color,
            targetColor,
            colorLerpSpeed * Time.deltaTime
        );
    }
}