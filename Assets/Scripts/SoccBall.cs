using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoccBall : MonoBehaviour
{
    public Rigidbody BallPrefab;
    public GameObject cursor;
    public LayerMask layer;
    public Transform KickPoint;
    public GameController Controller;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        BallPrefab = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        KickSoccBall();
    }

    void  OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Goal")
        {
            Debug.Log("Goalllllll!!!");
            Controller.IncrementScore();
       
        }
    }

    // Calling the Ballmovement 
    void KickSoccBall()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;

            Vector3 Vo = CalculateVelocity(hit.point, KickPoint.position, 1f);

            transform.rotation = Quaternion.LookRotation(Vo);

            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody obj = Instantiate(BallPrefab, KickPoint.position, Quaternion.identity);
                obj.velocity = Vo;

            }
            else
            {
                cursor.SetActive(false);
            }

        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        // defined the distance x and y
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        //create a float that rep distance
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;

    }
}
