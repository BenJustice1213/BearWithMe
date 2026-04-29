using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] float setupDelay;
    [HideInInspector] public TreeTower targetTower;
    [HideInInspector] private bool isActive = false;
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
        targetTower.StartAttack(this.gameObject);
    }
    public void PutOut()
    {
        if(isActive) targetTower.StopAttack(this.gameObject);
        Destroy(gameObject);
    }
}