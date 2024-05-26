using Pathfinding;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(LineRenderer))]
public class CopMovement : MonoBehaviour
{
    public CopCharseTarget copCharseTarget;

    public float moveSpeed = 2f;
    public float nextWayPointDistance = 2f;
    public float repeatTimeUpdatePath = 0.5f;
    public bool facingRight = true;
    public Animator animator;

    Path path;
    Seeker seeker;
    Rigidbody2D rb;

    Coroutine moveCoroutine;
    LineRenderer lineRenderer;



    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        InvokeRepeating("CalculatePath", 0f, repeatTimeUpdatePath);
        SetupLineRenderer();
    }

    void CalculatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, copCharseTarget.currentTarget.position, OnPathCompleted);
    }

    void OnPathCompleted(Path p)
    {
        if (!p.error)
        {
            path = p;
            MoveToTarget();
        }
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

            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - rb.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
            if (distance < nextWayPointDistance) currentWP++;


            if (force.x < 0 && facingRight)
                Flip();
            else if (force.x > 0 && !facingRight)
                Flip();

            animator.SetBool("isMoving", force != Vector2.zero);

            yield return null;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void SetupLineRenderer()
    {
        lineRenderer.positionCount = 50;
        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        UpdateCircle();
    }

    void UpdateCircle()
    {
        float angle = 0f;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float x = Mathf.Cos(angle) * copCharseTarget.charseRange;
            float y = Mathf.Sin(angle) * copCharseTarget.charseRange;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) + transform.position);
            angle += 2 * Mathf.PI / lineRenderer.positionCount;
        }
    }

    private void Update()
    {
        UpdateCircle();
    }
}