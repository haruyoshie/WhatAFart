using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInteraction : MonoBehaviour
{
    public ParticleSystem partSystem;
    public List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        partSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

void OnParticleCollision(GameObject other)
{
    if (other.tag == "Customer")
    {
        partSystem.GetCollisionEvents(other, collisionEvents);
        // Aquí puedes manejar la lógica de lo que sucede cuando las partículas colisionan con el objeto
        Debug.Log("Partículas han colisionado con " + other.name);
    }
}

// void OnParticleTrigger(GameObject other)
// {
//     if (other.tag == "Customer")
//     {
//         // Aquí puedes manejar la lógica de lo que sucede cuando las partículas colisionan con el objeto
//         Debug.Log("Partículas han colisionado con " + other.name);
//     }
// }
}
