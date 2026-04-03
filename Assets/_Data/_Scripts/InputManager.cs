using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance => instance;

    [SerializeField] protected float horizontal, vertical;
    [SerializeField] protected bool isLeftClick, isPouring, isJump;
    public float HorizontalInput => horizontal;
    public float VerticalInput => vertical;
    public bool IsLeftClick => isLeftClick;
    public bool IsPouring => isPouring;
    public bool IsJump => isJump;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Only 1 InputManager are allowed to exist");
        InputManager.instance = this;
    }

    private void Update()
    {
        this.GetMovementInput();
        this.GetLeftClickInput();
        this.GetPouringInput();
        this.GetJumpInput();
    }

    protected void GetMovementInput()
    {
        this.horizontal = Input.GetAxis("Horizontal");
        this.vertical = Input.GetAxis("Vertical");
    }

    protected void GetLeftClickInput()
    {
        this.isLeftClick = Input.GetKeyDown(KeyCode.Mouse0);
    }

    protected void GetPouringInput()
    {
        this.isPouring = Input.GetKey(KeyCode.E);
    }

    protected void GetJumpInput()
    {
        this.isJump = Input.GetButtonDown("Jump");
    }
}
