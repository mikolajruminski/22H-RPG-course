using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private PlayerInputActions playerInputActions;
    private Rigidbody2D playerRB;
    [SerializeField] private float playerSpeedMultiplier;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerRB = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementVector = GetMovementVector();
        playerRB.velocity = new Vector2(movementVector.x, movementVector.y) * playerSpeedMultiplier * Time.deltaTime;
    }

    private Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }

    public Rigidbody2D ReturnPlayerRB()
    {
        return playerRB;
    }
}
