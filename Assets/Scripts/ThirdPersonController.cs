using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ThirdPersonController : MonoBehaviour
{
    private InputController inputActions;
    private InputAction move;
    private Animator animator;
    private Rigidbody rb;

    private Vector3 forceDirection = Vector3.zero;

    [SerializeField] private float movementForce = 1f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private Camera mainCamera;

    public InventorySystem inventorySystem { get; private set; }

    [Header("UI")]
    [SerializeField] private Button pickupButton;
    public TextMeshProUGUI info;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new InputController();
        animator = GetComponent<Animator>();
        Statics.player = this;
        inventorySystem = GetComponent<InventorySystem>();
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        move = inputActions.Player.Move;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        animator.SetFloat("speed", rb.velocity.magnitude / maxSpeed);
    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * movementForce * GetCameraRight(mainCamera);
        forceDirection += move.ReadValue<Vector2>().y * movementForce * GetCameraForward(mainCamera);

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Physics.gravity.y * Time.fixedDeltaTime * Vector3.down;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

        LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera camera)
    {
        Vector3 forward = camera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera camera)
    {
        Vector3 right = camera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    public void ChangeActiveOutStorage(OutStorage outStorage)
    {
        Statics.activeOutStorage = outStorage;
        pickupButton.interactable = outStorage != null;
    }
}
