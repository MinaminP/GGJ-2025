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

    [SerializeField] float moveInput;
    [SerializeField] Sprite dropletSprite, bubbleSprite, bubblePopSprite;

    public float maxScale;
    public float currentScale;

    public float shrinkRate;

    public float expandRate;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        CurrentSpeed = downSpeed;
    }
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            isMovingSideways = false;
            //sprite.sprite = dropletSprite;
        } 
        else
        {
            //sprite.sprite = bubbleSprite;
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
        //sprite.sprite = bubblePopSprite;
        yield return new WaitForSeconds(pauseDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Coroutine untuk menggerakkan secara horizontal
    IEnumerator MoveHorizontal(Vector3 direction)
    {
        isMovingSideways = true; // Menandai sedang bergerak horizontal
        float elapsedTime = 0f; // Waktu yang telah berlalu

        Vector3 startPosition = transform.position; // Posisi awal
        Vector3 targetPosition = startPosition + (direction * horizontalSpeed); // Posisi tujuan

        while (elapsedTime < horizontalMoveDuration)
        {
            // Lerp posisi dari awal ke tujuan
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / horizontalMoveDuration);
            elapsedTime += Time.deltaTime; // Tambahkan waktu frame
            yield return null; // Tunggu satu frame
        }

        // Pastikan posisi akhir sesuai tujuan
        transform.position = targetPosition;

        // Tunggu beberapa detik sebelum melanjutkan gerakan vertikal
        yield return new WaitForSeconds(pauseDuration);
        isMovingSideways = false; // Melanjutkan gerakan vertikal
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
