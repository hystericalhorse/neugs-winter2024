using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
	[SerializeField, Range(1,10)] float WalkSpeed = 5f;
	[SerializeField] bool defaultSprint = false;
	[SerializeField] private Animator animator;

    Rigidbody2D rb;
	PlayerControls controls;

	Vector2 movement = Vector2.zero;
	Vector2 direction = Vector2.zero;
	bool sprinting;

	#region MonoBehaviour
	private void Awake()
	{
		rb ??= gameObject.GetComponent<Rigidbody2D>();
		rb.gravityScale = 0f;

		controls ??= new();

		RegisterControls();
	}

	private void Start()
	{
		sprinting = defaultSprint;
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
		rb.velocity = movement.normalized * (sprinting ? 2 * WalkSpeed : WalkSpeed);
	}

	private void LateUpdate()
	{
		
	}

	private void OnDestroy()
	{
		UnassignControls();
	}
	#endregion

	#region Input Handling
	Dictionary<string,InputAction> actions = new();

	public void RegisterControls()
	{
		actions.Add("move",controls.Player.Move);
		actions.Add("interact",controls.Player.Interact);
		actions.Add("pause",controls.Player.Pause);
		actions.Add("sprint",controls.Player.Sprint);

		AssignControls();
	}

	public void AssignControls()
	{
		actions["move"].performed += Move;
		actions["move"].canceled += Move;

		actions["interact"].performed += Interact;

		actions["pause"].performed += Pause;

		actions["sprint"].started += Sprint;
		actions["sprint"].canceled += Sprint;
	}

	public void ActivateControls()
	{
		foreach (var kvp in actions) kvp.Value.Enable();
	}

	public void DeactivateControls()
	{
		foreach (var kvp in actions) kvp.Value.Disable();
	}

	public void UnassignControls()
	{
		actions["move"].performed -= Move;
		actions["move"].canceled -= Move;

		actions["interact"].performed -= Interact;

		actions["pause"].performed -= Pause;

		actions["sprint"].started -= Sprint;
		actions["sprint"].canceled -= Sprint;

		DeregisterControls();
	}

	public void DeregisterControls() => actions.Clear();
	#endregion

	#region Controls
	public void Move(InputAction.CallbackContext context)
	{
		movement = context.ReadValue<Vector2>();

		if (movement.magnitude != 0)
		{
			if (movement.normalized.x > movement.normalized.y)
			{
				if (movement.normalized.x > 0)
					direction = Vector2.right;
				else
					direction = Vector2.left;
			}
			else
			{
				if (movement.normalized.y > 0)
					direction = Vector2.up;
				else
					direction = Vector2.down;
			}
		}
	}

	public void Interact(InputAction.CallbackContext context)
	{
		var hits = Physics2D.BoxCastAll(transform.position, Vector2.one, 0, direction, 1);
		
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

	public void Sprint(InputAction.CallbackContext context)
	{
		if (context.started) sprinting = !defaultSprint;
		if (context.canceled) sprinting = defaultSprint;
	}

	public void Pause(InputAction.CallbackContext context)
	{
		//TODO
	}

	#endregion

	#region Animator
	public void Animate()
	{
		if(direction == Vector2.up)
		{
			animator.SetBool("WalkUp", true);
		}
        if (direction == Vector2.down)
        {
            animator.SetBool("WalkDown", true);
        }
        if (direction == Vector2.right)
        {
            animator.SetBool("WalkRight", true);
        }
        if (direction == Vector2.left)
        {
            animator.SetBool("WalkLeft", true);
        }
    }
	#endregion
}
