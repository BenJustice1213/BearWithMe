using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] float setupDelay;
    private bool isActive = false;
    void Start()
    {
        StartCoroutine(SetupFire());
    }
    IEnumerator SetupFire()
    {
        yield return new WaitForSeconds(setupDelay);
        RegisterFire();
    }
    public void RegisterFire()
    {
        isActive = true;
        // Let fire start contributing to forest health degradation
    }
    public void PutOut()
    {
        if (isActive) {/* Deregister fire if it's already registered */}


    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Stomp")
        {
            PutOut();
        }
    }
}