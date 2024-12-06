using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float Speed,JumpForce;

    public bool isGround;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * Speed, rb.velocity.y, Input.GetAxis("Vertical") * Speed);


        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            isGround = true;
        }
        if (collision.gameObject.tag == "end")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
