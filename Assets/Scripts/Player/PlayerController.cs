using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField, Range(1,5)] float WalkSpeed = 2.5f;

    Rigidbody2D rb;
	PlayerControls controls;

	Vector2 movement = Vector2.zero;

	#region MonoBehaviour
	private void Awake()
	{
		rb ??= gameObject.GetComponent<Rigidbody2D>();
		controls ??= new();

		RegisterControls();
	}

	private void Start()
	{
		//TODO
	}

	private void OnEnable()
	{
		ActivateControls();
	}

	private void OnDisable()
	{
		DeactivateControls();
	}

	private void Update()
	{
		//TODO
	}

	private void FixedUpdate()
	{
		rb.velocity = movement.normalized * WalkSpeed * Time.fixedDeltaTime * 100;
	}

	private void LateUpdate()
	{
		//TODO
	}
	#endregion

	#region Input Handling
	InputAction moveAction;
	InputAction interactAction;
	InputAction pauseAction;
	public void RegisterControls()
	{
		moveAction = controls.Player.Move;
		interactAction = controls.Player.Interact;
		pauseAction = controls.Player.Pause;

		AssignControls();
	}

	public void AssignControls()
	{
		moveAction.performed += Move;
		moveAction.canceled += Move;

		interactAction.performed += Interact;

		pauseAction.performed += Pause;
	}

	public void ActivateControls()
	{
		moveAction?.Enable();
		interactAction?.Enable();
		pauseAction?.Enable();
	}

	public void DeactivateControls()
	{
		moveAction?.Disable();
		interactAction?.Disable();
		pauseAction?.Disable();
	}

	public void Move(InputAction.CallbackContext context)
	{
		movement = context.ReadValue<Vector2>();
	}

	public void Interact(InputAction.CallbackContext context)
	{
		var hits = Physics2D.BoxCastAll(transform.position, Vector2.one * 2, 0, Vector2.up, 0);
		
		foreach (var hit in hits)
		{
			if (hit.collider == null) continue;
			if (hit.transform.gameObject.GetComponent<Interactable>() != null)
			{
				hit.transform.gameObject.GetComponent<Interactable>().OnInteract();
				return;
			}
		}
	}

	public void Pause(InputAction.CallbackContext context)
	{
		
	}

	#endregion
}
