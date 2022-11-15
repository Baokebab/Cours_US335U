using UnityEngine;
using System.Collections;

public class LauncherTarget : MonoBehaviour
{
	[SerializeField] Transform _armPos;
	public Vector3 target;
	public float distance = 4;
	public float h = 1;
	public float gravity = -5;
	public bool debugPath;

	void Start()
	{
	}

	void FixedUpdate()
	{
		target = transform.position + transform.forward * distance;
		if (debugPath)
		{
			DrawPath();
		}
	}

	public void Launch(GameObject prefab)
	{
		Rigidbody rb = prefab.GetComponent<Rigidbody>();
		Physics.gravity = Vector3.up * gravity;
		rb.useGravity = true;
		rb.velocity = CalculateLaunchData().initialVelocity;
		
	}

	LaunchData CalculateLaunchData()
	{
		float displacementY = target.y - _armPos.position.y;
		Vector3 displacementXZ = new Vector3(target.x - _armPos.position.x, 0, target.z - _armPos.position.z);
		float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;		
		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	void DrawPath()
	{
		LaunchData launchData = CalculateLaunchData();
		Vector3 previousDrawPoint = _armPos.position;

		int resolution = 30;
		for (int i = 1; i <= resolution; i++)
		{
			float simulationTime = i / (float)resolution * launchData.timeToTarget;
			Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = _armPos.position + displacement;
			Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
			previousDrawPoint = drawPoint;
		}
	}

	struct LaunchData
	{
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData(Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}

	}
}