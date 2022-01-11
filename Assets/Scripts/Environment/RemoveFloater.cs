using UnityEngine;

public class RemoveFloater : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floater"))
        {
            Destroy(other.gameObject);
        }
    }
}
