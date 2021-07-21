using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
	[Header("Prefabs")]
	public GameObject misilePrefab;

	[Header("Rocket")]
	public float baseSpeed;

	[Header("Data - Do not change!")]
	public Vector3 dest;
	public float speed => baseSpeed * Time.deltaTime;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Fire();
		}
	}

	private void FixedUpdate()
	{
		dest = Game.instance.MousePos();
		var dist = dest - transform.position;
		if (dist.magnitude < speed)
			transform.position = dest;
		else
			transform.position += dist.normalized * speed;
		if (dist.magnitude != 0)
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg - 90);
	}

	public void Fire()
	{
		var obj = Instantiate(misilePrefab);
		obj.transform.position = transform.position;
		var misile = obj.GetComponent<Misile>();
		misile.sinOff = Time.time;
		misile.baseRot = transform.rotation.eulerAngles.z;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.position, dest);
	}
}
