using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
	const float skinWidth = .015f;
	public int horizontalRayCount = 4, verticalRayCount = 4;
	float horizontalRaySpacing, verticalRaySpacing;

	public float maxSlopeAngle = 80, maxDecendAngle = 70;

	public LayerMask collisionMask;

	BoxCollider2D _collider;

	RaycastOrigins raycastOrigins;
	public CollisionInfo collisions;
	public Color rayCastColor = Color.cyan;

	// Use this for initialization
	void Start ()
	{
		_collider = GetComponent<BoxCollider2D>();

		CalculateRaySpacing();
	}

	void Update ()
	{

	}

	public void Move(Vector3 velocity)
	{
		UpdateRaycastOrigins();
		collisions.Reset();

		collisions.velocityOld = velocity;

		if (velocity.y < 0)
		{
			DecendSlope(ref velocity);
		}
		if (velocity.x != 0)
		{
			HorizontalCollisions (ref velocity);
		}
		if (velocity.y != 0)
		{
			VerticalCollisions (ref velocity);
		}
		
		transform.Translate (velocity);
	}
	
	void HorizontalCollisions(ref Vector3 velocity)
	{
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;
		
		for (int i = 0; i < horizontalRayCount; i ++)
		{
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.cyan);
			
			if (hit)
			{
				if (hit.collider.isTrigger) {

					return;

				}

				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

				if (i == 0 && slopeAngle <= maxSlopeAngle)
				{
					if (collisions.decendingSlope)
					{
						collisions.decendingSlope = false;
						velocity = collisions.velocityOld;
					}

					float distanceToSlopeStart = 0;
					if (slopeAngle != collisions.slopeAngleOld)
					{
						distanceToSlopeStart = hit.distance - skinWidth;
						velocity.x -= distanceToSlopeStart * directionX;
					}

					ClimbSlope(ref velocity, slopeAngle);
					velocity.x += distanceToSlopeStart * directionX;
				}

				if (!collisions.climbingSlope || slopeAngle > maxSlopeAngle)
				{
					velocity.x = (hit.distance - skinWidth) * directionX;
					rayLength = hit.distance;

					if (collisions.climbingSlope)
					{
						velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs (velocity.x);
					}

					collisions.left = directionX == -1;
					collisions.right = directionX == 1;
				}
			}
		}
	}

	void VerticalCollisions(ref Vector3 velocity)
	{
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;
		
		for (int i = 0; i < verticalRayCount; i ++)
		{
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;

			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			
			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.cyan);

			if (hit)
			{
				if (hit.collider.isTrigger) {

					return;

				}

				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				if (collisions.climbingSlope)
				{
					velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
				}

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}
		}

		if (collisions.climbingSlope)
		{
			float directionX = Mathf.Sign(velocity.x);

			rayLength = Mathf.Abs(velocity.x) + skinWidth;

			Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * velocity.y;
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.right * directionX,rayLength,collisionMask);
			
			if (hit)
			{
				float slopeAngle = Vector2.Angle(hit.normal,Vector2.up);
				if (slopeAngle != collisions.slopeAngle)
				{
					velocity.x = (hit.distance - skinWidth) * directionX;
					collisions.slopeAngle = slopeAngle;
				}
			}
		}
	}

	void ClimbSlope (ref Vector3 velocity, float slopeAngle)
	{
		float moveDistance = Mathf.Abs(velocity.x);
		float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
		
		if (velocity.y <= climbVelocityY)
		{
			velocity.y = climbVelocityY;
			velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (velocity.x);
			
			collisions.below = true;
			collisions.climbingSlope = true;
			collisions.slopeAngle = slopeAngle;
		}
	}

	void DecendSlope( ref Vector3 velocity)
	{
		float directionX = Mathf.Sign (velocity.x);
		Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
		RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

		if (hit)
		{
			float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
			if (slopeAngle != 0 && slopeAngle <= maxDecendAngle)
			{
				if (Mathf.Sign(hit.normal.x) == directionX)
				{
					if (hit.distance - skinWidth <= Mathf.Tan (slopeAngle * Mathf.Deg2Rad) * Mathf.Abs (velocity.x))
					{
						float moveDistance = Mathf.Abs(velocity.x);
						float decendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

						velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (velocity.x);
						velocity.y -= decendVelocityY;

						collisions.slopeAngle = slopeAngle;
						collisions.decendingSlope = true;
						collisions.below = true;
					}
				}
			}
		}
	}

	void UpdateRaycastOrigins()
	{
		Bounds bounds = _collider.bounds;
		bounds.Expand(skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	void CalculateRaySpacing()
	{
		Bounds bounds = _collider.bounds;
		bounds.Expand(skinWidth * -2);

		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	struct RaycastOrigins
	{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

	public struct CollisionInfo
	{
		public bool above, below;
		public bool left, right;

		public bool climbingSlope, decendingSlope;
		public float slopeAngle, slopeAngleOld;
		public Vector3 velocityOld;


		public void Reset()
		{
			above = below = left = right = climbingSlope = decendingSlope = false;

			slopeAngleOld = slopeAngle;
			slopeAngle = 0;
		}
	}
}
