using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttractorComp : MonoBehaviour , IAttractor
{
    [SerializeField]
    private float G = 6.674f;

    private Rigidbody m_rb;
    public static List<IAttractor> Attractors = new List<IAttractor>();

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;

        Attractors.Add(this);
    }

    private void FixedUpdate()
    {
        foreach(var attractor in Attractors)
        {
            if(attractor.GetInstanceId() == this.GetInstanceID())
            {
                continue;
            }

            Attract(attractor);
        }
    }

    void Attract(IAttractor _other)
    {
        var direction = GetPosition() - _other.GetPosition();
        var distance = direction.sqrMagnitude;

        var forceMagnitude = ((GetMass() * _other.GetMass()) / Mathf.Pow(distance, 2)) * G;
        var force = direction.normalized * forceMagnitude;

        _other.ApplyAttractionForce(force);
    }

    public float GetMass()
    {
        return m_rb.mass;
    }

    public Vector3 GetPosition()
    {
        return m_rb.position;
    }

    public void ApplyAttractionForce(Vector3 _attractionForce)
    {
        m_rb.AddForce(_attractionForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    public int GetInstanceId()
    {
        return GetInstanceID();
    }
}

public interface IAttractor
{
    int GetInstanceId();
    float GetMass();
    Vector3 GetPosition();
    void ApplyAttractionForce(Vector3 _attractionForce);
}
