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
        PlayerBear pb = GetComponent<PlayerBear>();

        pb.moveSpeed = 0; // Stop Player

        stompingHitbox.SetActive(true);
        yield return new WaitForSeconds(hitboxDuration);
        stompingHitbox.SetActive(false);

        pb.moveSpeed = pb.defaultMoveSpeed; // Restore Speed
    }

    IEnumerator StartRoaring()
    {
        PlayerBear pb = GetComponent<PlayerBear>();

        pb.moveSpeed = 0; // Stop Player

        roaringHitbox.SetActive(true);
        yield return new WaitForSeconds(hitboxDuration);
        roaringHitbox.SetActive(false);

        pb.moveSpeed = pb.defaultMoveSpeed; // Restore Speed
    }
}
