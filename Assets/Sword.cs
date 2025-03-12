using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform playerCamera; 
    public Transform HandPosition; 
    public Transform restingSword;
    public Transform swingingSword;

    public float swingSpeed = 10f; 
    public float swingArc = 90f; 
    public float swingDuration = 0.3f; 
    public float trackingTime = 0.1f; 

    private Vector3 pastLookDirection;
    private Vector3 currentLookDirection; 
    private bool isSwinging = false;
    private float swingProgress = 0f;
    private Vector3 swingAxis;

    public GameObject crosshairDot;
    public RectTransform crosshairLine;
    private Vector3 crosshairLookDirection;
    private Vector3 crosshairSwingAxis; 

    void Start()
    {
        pastLookDirection = playerCamera.forward;

        crosshairDot.SetActive(true);
        crosshairLine.gameObject.SetActive(true);
    }

    void Update()
    {

        pastLookDirection = Vector3.Lerp(pastLookDirection, playerCamera.forward, Time.deltaTime / trackingTime);

        if (Input.GetButtonDown("Fire1") && !isSwinging)
        {
            BeginSwing();
        }

        if (isSwinging)
        {
            UpdateSwing();
        }

        UpdateCrosshair();
    }

    void BeginSwing()
    {
        isSwinging = true;
        swingProgress = 0f;

        currentLookDirection = playerCamera.forward;
        
        restingSword.gameObject.SetActive(false);
        swingingSword.gameObject.SetActive(true);

        swingingSword.transform.position = HandPosition.position;
        swingingSword.transform.rotation = Quaternion.LookRotation(pastLookDirection, swingAxis);

        swingAxis = Vector3.Cross(pastLookDirection, currentLookDirection).normalized;
    }

    void UpdateSwing()
    {
        swingProgress += Time.deltaTime / swingDuration;

        if (swingProgress >= 1f)
        {
            EndSwing();
            return;
        }

        float swingAngle = -Mathf.Lerp(-swingArc, swingArc, swingProgress);
        swingingSword.transform.rotation = Quaternion.AngleAxis(swingAngle, swingAxis) * Quaternion.LookRotation(pastLookDirection, swingAxis);
        //swingingSword.transform.rotation = Quaternion.AngleAxis(swingAngle, swingAxis) * Quaternion.LookRotation(pastLookDirection);

    }

    void EndSwing()
    {
        isSwinging = false;
        restingSword.gameObject.SetActive(true);
        swingingSword.gameObject.SetActive(false);
    }

    //void UpdateCrosshair()
    //{
    //    crosshairLookDirection = playerCamera.forward;
    //    crosshairSwingAxis = Vector3.Cross(pastLookDirection, currentLookDirection).normalized;
    //    Vector3 crosshairSwingDirection = crosshairSwingAxis.normalized;

    //    float angle = Mathf.Atan2(crosshairSwingDirection.x, crosshairSwingDirection.y) * Mathf.Rad2Deg;

    //    crosshairLine.rotation = Quaternion.Euler(0, 0, angle);
    //}

    void UpdateCrosshair()
    {
        Vector3 cameraMovementDirection = playerCamera.forward - pastLookDirection;

        cameraMovementDirection.y = 0;
        cameraMovementDirection.Normalize();

        float angle = Mathf.Atan2(cameraMovementDirection.x, cameraMovementDirection.z) * Mathf.Rad2Deg;

        crosshairLine.rotation = Quaternion.Euler(0, 0, angle);
    }
}
