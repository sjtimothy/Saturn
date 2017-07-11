using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SgtKeyboardMove))]
public class SgtKeyboardMove_Editor : SgtEditor<SgtKeyboardMove>
{
	protected override void OnInspector()
	{
		DrawDefault("Require");
		DrawDefault("Speed");
		BeginError(Any(t => t.Dampening < 0.0f));
			DrawDefault("Dampening");
		EndError();
	}
}
#endif

// This component handles keyboard movement when attached to the camera
[AddComponentMenu(SgtHelper.ComponentMenuPrefix + "Keyboard Move")]
public class SgtKeyboardMove : MonoBehaviour
{
	[Tooltip("The key that needs to be held down to move")]
	public KeyCode Require = KeyCode.None;
	
	[Tooltip("The maximum speed of the movement")]
	public float Speed = 1.0f;
	
	[Tooltip("How quickly this accelerates toward the target position")]
	public float Dampening = 5.0f;
	
	private Vector3 targetPosition;
	
	protected virtual void Start()
	{
		targetPosition = transform.position;
	}
	
	protected virtual void Update()
	{
		if (Require == KeyCode.None || Input.GetKey(Require) == true)
		{
			targetPosition += transform.forward * Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime;
			
			targetPosition += transform.right * Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime;
		}
		
		transform.position = SgtHelper.Dampen3(transform.position, targetPosition, Dampening, Time.deltaTime, 0.1f);
	}
}