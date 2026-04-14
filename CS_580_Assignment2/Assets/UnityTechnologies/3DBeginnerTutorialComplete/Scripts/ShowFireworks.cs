using UnityEngine;

public class ShowFireworks : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem[] fireworks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            // set off the particle emitters
            foreach (ParticleSystem ps in fireworks)
            {
                ps.Play();
            }
        }
    }
}
