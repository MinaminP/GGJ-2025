using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatTurun : MonoBehaviour
{
    public float downSpeed = 2f;
    public float horizontalSpeed = 5f;
    public float pauseDuration = 2f;
    public float horizontalMoveDuration = 0.5f;
    public float fadeSpeed = 0.2f;

    private SpriteRenderer sprite;
    private float CurrentSpeed;
    private bool isMovingSideways = false;

    Rigidbody2D rb;

    Animator anim;

    [SerializeField] float moveInput;
    [SerializeField] Sprite dropletSprite, bubbleSprite;

    public float maxScale;
    public float currentScale;

    public float shrinkRate;

    public float expandRate;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        CurrentSpeed = downSpeed;
    }
    void Update()
    {
        if (!GameManager.isPaused)
        {
            Movement();
        }
    }

    void Movement()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            isMovingSideways = false;
            sprite.sprite = dropletSprite;
        }
        else
        {
            sprite.sprite = bubbleSprite;
            isMovingSideways = true;
        }

        if (!isMovingSideways)
        {
            transform.Translate(Vector3.down * CurrentSpeed * Time.deltaTime);
            DecreaseScale();
        }
        else
        {
            IncreaseScale();
        }

        transform.localScale = new Vector2(currentScale, currentScale);
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(moveInput * horizontalSpeed, rb.velocity.y, 0);
    }

    void DecreaseScale()
    {
        if (currentScale <= 0.2f)
        {
            currentScale = 0.2f;
        }
        else
        {
            currentScale -= Time.deltaTime / shrinkRate;
        }

    }

    void IncreaseScale()
    {
        if (currentScale <= maxScale)
        {
            currentScale += Time.deltaTime / expandRate;
        }
        else
        {
            currentScale = maxScale;
            StartCoroutine(BubblePop());
        }
    }

    IEnumerator BubblePop()
    {
        anim.SetTrigger("bubblePop");
        yield return new WaitForSeconds(pauseDuration);
        Destroy(gameObject);
        GameManager.isFailed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstackle"))
        {
            CurrentSpeed = downSpeed / 2;
            warnaMemudar();
        }

        if (collision.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
            GameManager.isFinished = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstackle"))
        {
            CurrentSpeed = downSpeed;
        }
    }

    public void warnaMemudar()
    {
        Color currentColor = sprite.color;
        currentColor.a -= fadeSpeed;
        currentColor.a = Mathf.Clamp01(currentColor.a);
        sprite.color = currentColor;

        if (currentColor.a < fadeSpeed)
        {
            GameManager.isFailed = true;
        }
    }
}