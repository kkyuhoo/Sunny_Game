using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public Transform target;
    public float speed;

    public Vector2 center;
    public Vector2 size;

    float height;
    float width;

    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize;  // 월드 세로
        width = height * Screen.width / Screen.height;  // 월드 가로
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void LateUpdate()  // LateUpdate 함수는 Update 함수가 호출된 후에 호출된다.
    {
        // Vector3.Lerp(Vector3 A, Vector3 B, float t) --> A, B 사이의 벡터값을 반환한다.
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

        // Mathf.Clamp(Value, min, max)
        
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }



}
