using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;   // point d'arrivée près de la porte opposée

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.attachedRigidbody.position = destination.position;
            other.transform.position = destination.position;
                        other.GetComponent<PacmanController>().PlayTeleportSound();
            Debug.Log("Téléportation vers " + destination.name);
        }
    }
}