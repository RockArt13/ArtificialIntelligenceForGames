using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The player class contains the common features for green and purple players
public class Player : MonoBehaviour
{
    public enum ForceType { Repulsion = -1, None = 0, Attraction = 1 }

    // Coefficient for balancing variables' floats
    protected static float numNormalizer = 1000f;

    // What is the maximum speed of the player?
    protected float maxSpeed = 0.03f * numNormalizer; 

    // What is the maximum rotation speed of the player?
    protected float maxRotationSpeed = 180f;

    // What is the current speed of the player?
    protected float currentSpeed = 0.01f * numNormalizer;

    // What is the current direction (rotation around the y axis) of the player?
    protected float currentRotation = 10f;

    protected GameManager gameManager;
    protected GameObject target;
    protected GameObject obstacle;
    protected GameObject capturer;

    protected Vector3 direction;

    protected bool rep = true;

    private readonly float radius = 3f;

    private readonly float angle = 180f;

    public LayerMask obstacleMask;

    public List<GameObject> targets;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance();

        maxSpeed = Random.Range(currentSpeed, maxSpeed);
        maxRotationSpeed = Random.Range(currentRotation, maxRotationSpeed);

        obstacle = gameManager.obstacles[Random.Range(0, gameManager.obstacles.Count)];
        RandomMove();
    }

    private void Move()
    {
         transform.Translate(direction * Time.deltaTime); 
         transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)), currentSpeed * Time.deltaTime);
    }

    protected virtual void Update()
    {
        if (gameManager.gameOver)
        {
            currentSpeed = 0f * Time.deltaTime;
        }

        if (targets.Count != 0)
            Move(ClosestObjects(), rep);
        else
            Move();

        ViewUpdate();
    }

  
    public void RandomMove()
    {
        float distance = Vector2.Distance(transform.position, obstacle.transform.position);
        if (distance <= 2.5f) // check the distance
            obstacle = gameManager.obstacles[Random.Range(0, gameManager.obstacles.Count)];
        Move(obstacle, !rep); 
    }
    
    protected virtual void Move(GameObject gameObject, bool rep)
    {
        List<GameObject> gameObjects  = new()
        {
            gameObject
        };
        Move(gameObjects, rep);
    }

    protected virtual void Move(List<GameObject> gameObject, bool rep)
    {
        float distance;

        if (!rep)
        {
            foreach (GameObject o in gameObject)
            {
                Vector3 obstaclePosition;

                if (o.CompareTag(gameManager.fencesTag))
                    obstaclePosition = o.GetComponent<Collider>().ClosestPoint(transform.position);
                else
                    obstaclePosition = o.transform.position;

                direction = new Vector3(obstaclePosition.x, 0f, obstaclePosition.z) - transform.position;

                distance = direction.magnitude;
                direction = direction.normalized;

                if (distance < 0.1f)
                    continue;

                direction = ((float)ForceType.Attraction) * distance * direction; 
                transform.Translate(maxSpeed * Time.deltaTime * direction, Space.World);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)), currentSpeed * Time.deltaTime);
            }
        }

        if (rep)
        {
            foreach (GameObject o in gameObject)
            {
                if(o != target && o != capturer)
                {
                    Vector3 obstaclePosition;

                    if (o.CompareTag(gameManager.fencesTag))
                        obstaclePosition = o.GetComponent<Collider>().ClosestPoint(transform.position);
                    else
                        obstaclePosition = o.transform.position;

                    direction = new Vector3(obstaclePosition.x, 0f, obstaclePosition.z) - transform.position;

                    distance = direction.magnitude;
                    direction = direction.normalized;

                    direction = ((float)ForceType.Repulsion) * (maxSpeed / distance) * direction; 

                    transform.Translate(direction * Time.deltaTime);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)), currentSpeed * Time.deltaTime);
                }
            }
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void SetTarget(GameObject targetObject)
    {
        target = targetObject;
    }

    public bool IsJailed(GreenPlayer greenPlayer)
    {
        Move(gameManager.goal, !rep);
        return (Vector2.Distance(greenPlayer.transform.position, new Vector2(gameManager.goal.transform.position.x, gameManager.goal.transform.position.z)) <= 6f); // jailed distance
    }

    public void ViewUpdate()
    {
        targets.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < hitColliders.Length; i++)
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject != gameObject)
                {
                    if (collider.gameObject.CompareTag(gameManager.purpleTag) || collider.gameObject.CompareTag(gameManager.greenTag
) || collider.gameObject.CompareTag(gameManager.obstacleTag) || collider.gameObject.CompareTag(gameManager.fencesTag))
                    {
                        Transform target = collider.transform;
                        Vector3 directionToTarget = (target.position - transform.position).normalized;

                        if (collider.gameObject.CompareTag(gameManager.obstacleTag) || collider.gameObject.CompareTag(gameManager.fencesTag))
                        {
                            if (!targets.Contains(collider.gameObject))
                                targets.Add(collider.gameObject);
                        }

                        if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                        {
                            float distanceToTarget = Vector3.Distance(transform.position, target.position);

                            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                            {
                                if (!targets.Contains(collider.gameObject))
                                    targets.Add(collider.gameObject);
                            }
                        }
                    }
                }
            }
    }

    public List<GameObject> ClosestObjects()
    {
        targets.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector2.Distance(this.transform.position, a.transform.position)
            .CompareTo(
              Vector2.Distance(this.transform.position, b.transform.position));
        });
        return targets;
    }
}
