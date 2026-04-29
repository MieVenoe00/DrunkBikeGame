using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 3f;
    public bool isNormalWorldObstacle = true; // Sættes automatisk af spawner

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private WorldManager worldManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        worldManager = FindAnyObjectByType<WorldManager>();
        UpdateVisibility();
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -5f)
            Destroy(gameObject);

        UpdateVisibility();
    }

    void UpdateVisibility()
    {
        // Vis kun obstacle hvis det tilhører den aktive verden
        bool isCurrentWorld = (isNormalWorldObstacle == worldManager.IsNormalWorld());
        spriteRenderer.enabled = isCurrentWorld;
        col.enabled = isCurrentWorld; // Kan ikke slå spilleren ihjel i forkerte verden
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ramt! Game Over");
            // Game over logik kommer her
        }
    }
}