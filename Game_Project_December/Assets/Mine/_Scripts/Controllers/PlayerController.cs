using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform PlayerCamera = null;
    [SerializeField] float mouseSens = 1.0f;
    [SerializeField] bool mouseLock = true;
    [SerializeField] float moveSpeed = 6.0f;
    [SerializeField] float moveSmoothTime = 0.1f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float jump = 1.5f;

    public Enemy enemy;
    EnemyStats enemyStats;
    bool moved = false;
    bool escape = false;
    public Text Hint;
    public Text EnemyText;
    float hintTimer = 0.0f;


    Camera cam;
    float cameraY = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirV = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        Hint.text = "Use WASD to move, Left Mouse Button to attack, Right to block and Tab to open the menu";
        Application.targetFrameRate = 60;
        cam = Camera.main;

        controller = GetComponent<CharacterController>();
        if (mouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        hintTimer += Time.deltaTime;
        if (hintTimer > 4 && hintTimer <8)
        {
            Hint.text = "Collect the 3 orbs held by the 3 bosses on the island and bring them to the big tree to cleanse the island";
        }
        if (hintTimer >= 8)
        {
            Hint.text = "";
        }
        if (!escape)
        {
            if (Input.GetKeyDown("left shift"))
            {
                moveSpeed = moveSpeed * 1.5f;
            }

            if (Input.GetKeyUp("left shift"))
            {
                moveSpeed = moveSpeed / 1.5f;
            }

            if (Input.GetMouseButtonDown(0))
            {
                RayCast();
            }

            if (!moved)
            {
                MouseLook();
                Movement();
            }
            moved = false;

            if (enemyStats != null)
            {
                enemyStats.ShowEnemy();
            }
        }
    }

    void RayCast()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Enemy interact = hit.collider.GetComponent<Enemy>();
            if (interact != null)
            {
                SetTarget(interact);
            }
            else
            {
                RemoveTarget();
            }
        }
    }

    void MouseLook()
    {
        Vector2 mousePos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraY -= mousePos.y * mouseSens;
        cameraY = Mathf.Clamp(cameraY, -90.0f, 90.0f);

        PlayerCamera.localEulerAngles = Vector3.right * cameraY;

        transform.Rotate(Vector3.up * mousePos.x * mouseSens);
    }

    void Movement()
    {
        Vector2 target = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        target.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, target, ref currentDirV, moveSmoothTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }

        if (Input.GetKeyDown("space") && controller.isGrounded)
        {
            velocityY += Mathf.Sqrt(jump * -1.5f * gravity);
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * moveSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
    }

    void SetTarget(Enemy newEnemy)
    {
        if (newEnemy != enemy)
        {
            enemy = newEnemy;
            enemyStats = enemy.GetComponent<EnemyStats>();
        }
        newEnemy.OnFocused(transform);
    }

    void RemoveTarget()
    {
        if (enemy != null)
        {
            EnemyText.text = "";
            enemy.OnDefocused();
        }

        enemy = null;
    }

    void DoorTrigger(string door)
    {
        velocityY = 0.0f;
        switch (door)
        {
            case "CryptDoor":
                gameObject.transform.position = new Vector3(-277, 12, -2);
                break;

            case "CryptExit":
                gameObject.transform.position = new Vector3(-160, 35, 1185);
                break;

            case "Well":
                gameObject.transform.position = new Vector3(-46, 1, 0);
                break;

            case "BlindWellExit":
                gameObject.transform.position = new Vector3(-404, 33, 1070);
                break;

            case "HunterEnter":
                gameObject.transform.position = new Vector3(-326, 36, 1423);
                break;

            case "HunterExit":
                gameObject.transform.position = new Vector3(-326, 36, 1405);
                break;
        }
        moved = true;
    }

    public void setEscape()
    {
        escape = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void unsetEscape()
    {
        escape = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
