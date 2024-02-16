using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEditor.Rendering.FilterWindow;

public class PlayerController : MonoBehaviour
{
	[SerializeField, Range(1,5)] float WalkSpeed = 2.5f;
	[SerializeField, Range(1,5)] float SprintSpeed = 5f;
	[SerializeField] bool defaultSprint = false;
	[SerializeField] private Animator animator;
    [SerializeField, Range(0.1f, 10)] float WalkAnimationSpeed = 1f;
    [SerializeField, Range(0.1f, 10)] float SprintAnimationSpeed = 2f;
    Rigidbody2D rb;
	PlayerControls controls;
	public bool HasActiveControls;
	
	[SerializeField] Flashlight flashlight;

	Vector2 movement = Vector2.zero;
	Vector2 lastMovement = Vector2.zero;
	Vector2 direction = Vector2.zero;
	bool sprinting;

	#region MonoBehaviour
	private void Awake()
	{
		rb ??= gameObject.GetComponent<Rigidbody2D>();
		rb.gravityScale = 0f;

		flashlight ??= gameObject.GetComponent<Flashlight>();

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
		rb.velocity = Vector2.zero;
		animator.speed = 0;
		DeactivateControls();
	}

	private void Update()
	{
		Animate();
	}

	private void FixedUpdate()
	{
		rb.velocity = movement.normalized * (sprinting ? SprintSpeed : WalkSpeed);
	}

	private void LateUpdate()
	{
		lastMovement = movement;
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
		actions.Add("flashlight",controls.Player.Flashlight);

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

        actions["flashlight"].performed += Flashlight;
    }

	public void ActivateControls()
	{
		foreach (var kvp in actions) kvp.Value.Enable();
		HasActiveControls = true;
	}

	public void DeactivateControls()
	{
		foreach (var kvp in actions) kvp.Value.Disable();
		HasActiveControls = false;
	}

	public void UnassignControls()
	{
		actions["move"].performed -= Move;
		actions["move"].canceled -= Move;

		actions["interact"].performed -= Interact;

		actions["pause"].performed -= Pause;

		actions["sprint"].started -= Sprint;
		actions["sprint"].canceled -= Sprint;

		actions["flashlight"].performed -= Flashlight;

		DeregisterControls();
	}

	public void DeregisterControls() => actions.Clear();

	#endregion

	#region Controls
	private readonly Vector3 centerY = new(0,0.5f,0);
	public void Move(InputAction.CallbackContext context)
	{
		movement = context.ReadValue<Vector2>();

		if (movement.magnitude > 0)
		{
			UpdateDirection();

			flashlight.transform.localPosition = ((Vector3)movement.normalized * 0.5f) + centerY;
			flashlight.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(movement.x, movement.y) * 180 / Mathf.PI, -Vector3.forward);
		}
	}

	public void UpdateDirection()
	{
		if (movement.magnitude > 0)
		{
			direction = movement.Cardinalize(ExtensionMethods.Axis.Vertical);

			//Debug.Log(direction);

			animator.SetBool("FaceUp", movement.y > 0);
			animator.SetBool("FaceRight", movement.x >= 0);
		}
			
	}

	public void Interact(InputAction.CallbackContext context)
	{
		var hits = Physics2D.BoxCastAll(transform.position, Vector2.one * 0.5f, 0, direction, 1);
		ExtDebug.DrawBoxCastBox(transform.position, Vector2.one * 0.25f, Quaternion.identity, direction, 1, Color.blue);
			
		foreach (var hit in hits)
		{
			Debug.DrawRay(hit.point, Vector3.up * 0.1f, Color.red, 5.0f);
			Debug.DrawRay(hit.point, Vector3.right * 0.1f, Color.red, 5.0f);

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

	public void Flashlight(InputAction.CallbackContext context)
	{
		flashlight.Toggle();
	}

	#endregion

	#region Animator
	public void Animate()
	{
		if (animator == null) return;
		
        //AudioManager.instance.PauseSounds();
        animator.SetFloat("XSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("YSpeed", Mathf.Abs(rb.velocity.y));

		animator.speed = (sprinting? SprintAnimationSpeed : WalkAnimationSpeed);
    }
	//public void PlayFootstep() { AudioManager.instance.PlaySound("Footsteps"); }
    
    #endregion



}
