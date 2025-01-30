using UnityEngine;

public class DragAndThrow : MonoBehaviour
{
    [Header("Drag Settings")]
    [SerializeField] private float dragSpeed = 8f;             // Slower for smoother following
    [SerializeField] private float throwForceMultiplier = 5f;  // Lighter throws
    [SerializeField] private float maxThrowForce = 12f;        // Lower max force for floatier feel
    [SerializeField] private float gravityScale = 0.5f;        // Reduced gravity for floating
    [SerializeField] private float linearDrag = 0.5f;          // Air resistance for smoother movement

    private Camera mainCamera;
    private GameObject selectedObject;
    private Rigidbody2D selectedRigidbody;
    private Vector2 dragStartPos;
    private Vector2 lastDragPosition;
    private bool isDragging = false;
    private float dragStartTime;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Start dragging
            Vector2 mousePosition = GetMouseWorldPosition();
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Particles"))
            {
                selectedObject = hit.collider.gameObject;
                selectedRigidbody = selectedObject.GetComponent<Rigidbody2D>();

                if (selectedRigidbody != null)
                {
                    isDragging = true;
                    dragStartPos = mousePosition;
                    lastDragPosition = mousePosition;
                    dragStartTime = Time.time;

                    // Disable gravity while dragging
                    selectedRigidbody.gravityScale = 0f;
                    selectedRigidbody.velocity = Vector2.zero;
                }
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            // Continue dragging
            Vector2 mousePosition = GetMouseWorldPosition();
            Vector2 dragDelta = mousePosition - (Vector2)selectedObject.transform.position;

            selectedRigidbody.MovePosition(selectedRigidbody.position + dragDelta * dragSpeed * Time.deltaTime);
            lastDragPosition = mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // Release and throw
            Vector2 mousePosition = GetMouseWorldPosition();
            Vector2 throwVelocity = (mousePosition - dragStartPos) / (Time.time - dragStartTime);

            // Apply throw force but keep gravity disabled
            selectedRigidbody.gravityScale = 0f;
            selectedRigidbody.drag = linearDrag;
            throwVelocity = Vector2.ClampMagnitude(throwVelocity * throwForceMultiplier, maxThrowForce);
            selectedRigidbody.velocity = throwVelocity;

            // Reset dragging state
            isDragging = false;
            selectedObject = null;
            selectedRigidbody = null;
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    private void OnDrawGizmos()
    {
        // Draw debug line while dragging
        if (isDragging && selectedObject != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(dragStartPos, GetMouseWorldPosition());
        }
    }
}