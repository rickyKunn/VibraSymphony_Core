using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Editor上でCameraをキーボード操作するためのスクリプト
/// </summary>
public class EditorCamera : MonoBehaviour
{
    const float Angle = 50f;
    public float baseSpeed = 4f;
    public float minSpeed = 1f;
    public float maxDownAngle = 90f;
    public float minDownAngle = 0f;   // 速度が最大になるカメラの角度（水平）
    private CharacterController characon;
    private new Camera camera;

    private void Start()
    {
        if (this.name == "CenterEyeAnchor") Destroy(this);
        characon = this.GetComponent<CharacterController>();
        camera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
    }

    void Update()
    {
        float cameraPitch = camera.transform.eulerAngles.x;

        if (cameraPitch > 180)
        {
            cameraPitch = 360 - cameraPitch;
        }

        float normalizedSpeed = Mathf.InverseLerp(maxDownAngle, minDownAngle, cameraPitch);
        float currentSpeed = Mathf.Lerp(minSpeed, baseSpeed, normalizedSpeed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 2;
        }

        Vector2 tmp = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);

        if (tmp.magnitude > 0.1f)
        {
            Quaternion q = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);

            var vec = new Vector3(tmp.x, 0, tmp.y);
            vec = q * vec * currentSpeed * Time.deltaTime;

            if (!characon.isGrounded)
            {
                vec.y -= 15f * Time.deltaTime;
            }
            characon.Move(vec);
        }

        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += -transform.right * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += -transform.forward * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right * currentSpeed * Time.deltaTime;
        }

        characon.Move(moveDirection);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, Angle * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -Angle * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            camera.transform.Rotate(-Angle * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            camera.transform.Rotate(Angle * Time.deltaTime, 0, 0);
        }

        // ジャンプ処理(使用予定なし)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (characon.isGrounded)
            {
                characon.Move(Vector3.up * 5);
            }
        }
    }
}
