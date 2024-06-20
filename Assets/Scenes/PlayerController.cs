using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask stageLayer;

    private Rigidbody2D rb;
    private float speed = 8.0f;
    private Vector2 _direction;
    private Vector2 _directionReserve;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _direction = Vector2.right;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            _directionReserve.x = Input.GetAxisRaw("Horizontal");
            _directionReserve.y = Input.GetAxisRaw("Vertical");
        }
        if (_directionReserve != Vector2.zero)
        {
            CheckDirection(_directionReserve);
        }

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void FixedUpdate()
    {
        Vector2 dist = _direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + dist);
    }

    private void CheckDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast
            (transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, stageLayer);

        if (hit.collider == null)
        {
            _direction = direction;
            _directionReserve = Vector2.zero;
        }
    }
}