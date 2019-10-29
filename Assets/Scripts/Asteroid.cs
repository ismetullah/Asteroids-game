using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public List<float> speedList;
    public List<int> scoreList;
    public List<float> scaleList;
    public GameObject asteroid;

    private int type = 0;
    private float speed = 10f;
    private int score = 10;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        type = 0;
        speed = speedList[0];
        score = scoreList[0];
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = (Random.onUnitSphere * speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            transform.Rotate(new Vector3(0, 0, 0.5f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (type != 2)
                for (int i = 0; i < 2; i++)
                {
                    Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
                    Vector2 c = new Vector2(transform.position.x + i, transform.position.y + i);
                    GameObject g = Instantiate(asteroid, c, Quaternion.identity);
                    g.GetComponent<Asteroid>().InitWithType(this.type + 1);
                }
            Destroy();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Die();
            Destroy();
        }
        
    }

    private void Destroy()
    {
        GameManager.Instance.AddScore(scoreList[type]);
        Destroy(gameObject);
    }

    public void InitWithType(int type)
    {
        this.type = type;
        speed = speedList[type];
        score = scoreList[type];
        transform.localScale = new Vector3(scaleList[this.type], scaleList[this.type], 1);
        if (rb != null)
        {
            rb.velocity = (Random.onUnitSphere * speed);
        }
    }
}
