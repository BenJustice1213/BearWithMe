using UnityEngine;
using System.Collections;

public class AttackMechanics : MonoBehaviour
{
    public GameObject roaringHitbox;
    public GameObject stompingHitbox;
    public float hitboxDuration = 1.0f;
    public float cooldown = 2f;

    private bool stompingReady = true;
    private bool roaringReady = true;
    private bool stompingActive = false;
    private bool roaringActive = false;

    void Start()
    {
        roaringHitbox.SetActive(false);
        stompingHitbox.SetActive(false);
    }

    void Update()
    {
        // Space for Stomping
        if (Input.GetKeyDown(KeyCode.Space) && stompingReady && !roaringActive)
        {
            StartCoroutine(StartStomping());
        }
        // Shift for Roaring
        if (Input.GetKeyDown(KeyCode.LeftShift) && roaringReady && !stompingActive)
        {
            StartCoroutine(StartRoaring());
        }
    }

    IEnumerator StartStomping()
    {
        stompingReady = false;
        stompingActive = true;
        stompingHitbox.SetActive(true);
        yield return new WaitForSeconds(hitboxDuration);
        stompingHitbox.SetActive(false);
        stompingActive = false;

        // Cooldown for Stomping
        yield return new WaitForSeconds(cooldown);
        stompingReady = true;
    }

    IEnumerator StartRoaring()
    {
        roaringReady = false;
        roaringActive = true;
        roaringHitbox.SetActive(true);
        yield return new WaitForSeconds(hitboxDuration);
        roaringHitbox.SetActive(false);
        roaringActive = false;

        //Cooldown for Roaring
        yield return new WaitForSeconds(cooldown);
        roaringReady = true;
    }
}
