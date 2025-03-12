using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float fallSpeed = 18f;
    public GameObject trailEffectPrefab;
    private bool isFalling = false;
    private GameObject trailEffect;
    public LayerMask groundMask;

    private Transform groundCheck;
    public float groundDistance = 0.4f; 

    public RectTransform endScreen;
    private FollowTest followScript;


    void Start()
    {
        groundCheck = transform.Find("groundCheckEnemy");
        //endScreen = GameObject.Find("endScreen").GetComponent<RectTransform>();

        followScript = GetComponent<FollowTest>();
        followScript.enabled = false;
        endScreen.gameObject.SetActive(false);

    }

    public void StartFalling()
    {
        isFalling = true;

        Vector3 trailPosition = transform.position + Vector3.up;

        trailEffect = Instantiate(trailEffectPrefab, trailPosition, Quaternion.identity);
        trailEffect.transform.SetParent(transform);
    }

    void Update()
    {
        if (isFalling)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded)
            {
                isFalling = false;
                Destroy(trailEffect);
                followScript.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endScreen.gameObject.SetActive(true);
        }
    }
}
