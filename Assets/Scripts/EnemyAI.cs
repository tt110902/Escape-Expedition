using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed;
    public float nextWPDistance;
    public Seeker seeker;
    public SpriteRenderer characterEN;
    public Transform target;
    Path path;

    Coroutine moveCoroutine;

    private void Start()
    {
        target = FindObjectOfType<Player>().gameObject.transform;

        InvokeRepeating("CalculatePath", 0f, 0.5f);
    }

    void CalculatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathCallback);
        }
    }

    void OnPathCallback(Path p)
    {
        if (p.error) return;
        
        path = p;
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }
        IEnumerator MoveToTargetCoroutine()
        {
            int currentWP = 0;

            while (currentWP < path.vectorPath.Count) 
            {
                Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
                Vector3 force = direction * moveSpeed * Time.deltaTime;
                transform.position += force;

                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
                if (distance < nextWPDistance)
                {
                    currentWP++;
                }

                if (force.x != 0)
                    if (force.x < 0)
                        characterEN.transform.localScale = new Vector3(-1, 1, 0);
                else
                        characterEN.transform.localScale = new Vector3(-1, 1, 0);
                
                yield return new WaitForSeconds(2f);
            }
        }
}
