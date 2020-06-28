using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineComp : MonoBehaviour
{
    Rigidbody m_rb;

    [SerializeField]
    private float m_speed = 1;
    Vector3 m_prevPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = transform.position - m_prevPos;
        transform.rotation.SetLookRotation(dir);
    }

    private void FixedUpdate()
    {

        m_rb.AddForce(transform.up * m_speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}
