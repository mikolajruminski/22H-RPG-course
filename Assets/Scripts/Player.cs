using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private PlayerInputActions playerInputActions;
    private Rigidbody2D playerRB;
    [SerializeField] private float playerSpeedMultiplier;
    private PlayerAreaEnterPossibilitesScript.AreaEntrances areaEntrance;

    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;
    public bool deactivatedMovement;

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

    private void Update()
    {
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, bottomLeftEdge.x, topRightEdge.x),
        Mathf.Clamp(transform.position.y, bottomLeftEdge.y, topRightEdge.y),
        Mathf.Clamp(transform.position.z, bottomLeftEdge.z, topRightEdge.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (deactivatedMovement)
        {
            playerRB.velocity = Vector2.zero;
        }
        else
        {
            Vector2 movementVector = GetMovementVector();
            playerRB.velocity = new Vector2(movementVector.x, movementVector.y) * playerSpeedMultiplier * Time.deltaTime;
        }

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

    public void SetNextAreaEntrancePoint(PlayerAreaEnterPossibilitesScript.AreaEntrances areaEntrance)
    {
        this.areaEntrance = areaEntrance;
    }

    public PlayerAreaEnterPossibilitesScript.AreaEntrances GetNextAreaEntrancePoint()
    {
        return areaEntrance;
    }

    public void SetLimits(Vector3 bottomEdgeToSet, Vector3 topEdgeToSet)
    {
        bottomLeftEdge = bottomEdgeToSet;
        topRightEdge = topEdgeToSet;
    }
}
