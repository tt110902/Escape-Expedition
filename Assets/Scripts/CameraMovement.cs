using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform tagert;

    public float speed = 7f;

    public float map_height = 5f;
    public float map_wight = 9f;

    public int map_x;
    public int map_y;

    public Vector3 cameraPosition;

    protected void Start() => ResetPositionCamera();

    protected virtual void LoadPlayer()
    {
        if (this.tagert != null) return;
        tagert = GameObject.Find("Player").gameObject.transform;
        Debug.Log(transform.name + ": LoadPlayer", gameObject);
    }

    private void FixedUpdate()
    {
        computeCameraPosition();
        transform.position = Vector3.Lerp(
            transform.position,
            cameraPosition,
            Time.deltaTime * speed);
    }

    void computeMapCoordinateFromPlayerPosition()
    {
        int x = (int)(Mathf.Sign(tagert.position.x)) * Mathf.CeilToInt(Mathf.Abs(Mathf.Abs(tagert.position.x) - map_wight) / (2 * map_wight));
        int y = (int)(Mathf.Sign(tagert.position.y)) * Mathf.CeilToInt(Mathf.Abs(Mathf.Abs(tagert.position.y) - map_height) / (2 * map_height));

        map_x = Mathf.Abs(tagert.position.x) < map_wight ? 0 : x;
        map_y = Mathf.Abs(tagert.position.y) < map_height ? 0 : y;
    }

    void computeCameraPosition()
    {
        computeMapCoordinateFromPlayerPosition();
        cameraPosition = new Vector3(map_x * map_wight * 2, map_y * map_height * 2, transform.position.z);
    }

    public void ResetPositionCamera()
    {
        computeCameraPosition();
        transform.position = cameraPosition;
    }
}
