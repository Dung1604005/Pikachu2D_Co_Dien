using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private Playerinputactions playerinputactions;

    [SerializeField] private LayerMask layerCell;
    
    

    void Awake()
    {
        playerinputactions = new Playerinputactions();

        playerinputactions.Player.Enable();
        
        // Call OnMouseClick every time player click left mouse
        playerinputactions.Player.mouse.performed += OnMouseclick;


    }

    public void OnEnable()
    {
        playerinputactions.Enable();
    }

    public void OnDisable()
    {
        playerinputactions.Disable();
    }

    private void OnMouseclick(InputAction.CallbackContext callbackContext)
    {
        // When player click left mouse, get the world position of mouse position
        
        Vector2 screenPosition = Mouse.current.position.ReadValue();

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        // Convert world position to grid position to get cell pos

        gridManager.GetCellByGridPosition
        (CordinateConventor.ConvertWorldPositionToGridPosition(worldPosition, gridManager.GridOrigin, gridManager.GridSize, gridManager.CellSize))?.SetChoosed();
    }
}
