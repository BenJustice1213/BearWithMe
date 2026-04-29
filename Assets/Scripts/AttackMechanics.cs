using UnityEngine;
using System.Collections;

public class AttackMechanics : MonoBehaviour
{
    public GameObject roaringHitbox;
    public GameObject stompingHitbox;
    public AudioSource source;
    public AudioClip roarSoundEffect;
    public AudioClip stompSoundEffect;

    public float hitboxDuration = 1.0f;
    public float animationHitboxDelay = 1.0f;
    public float cooldown = 2f;

    public Animator animator;

    private bool canStomp = true;
    private bool canRoar = true;

    void Start()
    {
        roaringHitbox.SetActive(false);
        stompingHitbox.SetActive(false);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canStomp)
        {
            StartCoroutine(StompRoutine());
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && canRoar)
        {
            StartCoroutine(RoarRoutine());
        }
    }

    IEnumerator StompRoutine()
    {
        canStomp = false;

        PlayerBear pb = GetComponent<PlayerBear>();
        pb.moveSpeed = 0;

        animator.SetTrigger("Stomp");
        source.PlayOneShot(stompSoundEffect);

        yield return new WaitForSeconds(animationHitboxDelay);

        stompingHitbox.SetActive(true);

        yield return new WaitForSeconds(hitboxDuration);

        stompingHitbox.SetActive(false);

        pb.moveSpeed = pb.defaultMoveSpeed;

        yield return new WaitForSeconds(cooldown);

        canStomp = true;
    }

    IEnumerator RoarRoutine()
    {
        canRoar = false;

        PlayerBear pb = GetComponent<PlayerBear>();
        pb.moveSpeed = 0;

        animator.SetTrigger("Roar");
        source.PlayOneShot(roarSoundEffect);

        yield return new WaitForSeconds(animationHitboxDelay);

        roaringHitbox.SetActive(true);

        yield return new WaitForSeconds(hitboxDuration);

        roaringHitbox.SetActive(false);

        pb.moveSpeed = pb.defaultMoveSpeed;

        yield return new WaitForSeconds(cooldown);

        canRoar = true;
    }
}