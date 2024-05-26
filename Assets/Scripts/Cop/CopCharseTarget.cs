using UnityEngine;

public class CopCharseTarget : MonoBehaviour
{
    public CircleCollider2D collider;
    public float charseRange = 3f;
    public Transform currentTarget;
    public Transform player;
    public Transform[] list;

    private void Start()
    {
        collider.radius = charseRange;
        collider.isTrigger = true;
        PickRandomPoint();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            currentTarget = player;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            PickRandomPoint();
    }

    private void Update()
    {
        if (currentTarget != player && Vector2.Distance(transform.position, currentTarget.position) < 1)
        {
            PickRandomPoint();
        }
    }

    void PickRandomPoint()
    {
        if (list.Length > 0)
        {
            currentTarget = list[Random.Range(0, list.Length)];
        }
    }

}
