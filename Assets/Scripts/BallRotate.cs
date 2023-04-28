using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotate : MonoBehaviour
{
    private Matrix4x4 rotateY;
    private Matrix4x4 rotateX;
    private Matrix4x4 rotateZ;

    private MeshFilter mf;
    private Vector3[] origVerts;
    private Vector3[] newVerts;
    private float spinner;
    public float spinSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        origVerts = mf.mesh.vertices;
        newVerts = new Vector3[origVerts.Length];
        spinner = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Cos(spinner / 10) * 0.5f;
        spinner += spinSpeed * Time.deltaTime * 1.5f;
        if (spinner > 360)
            spinner = 0;
        Matrix4x4 translate = Matrix4x4.Translate(new Vector3(1, 5 + y, 1));

        rotateY = Matrix4x4.Rotate(Quaternion.Euler(1, spinner, 1));
        rotateZ = Matrix4x4.Rotate(Quaternion.Euler(1, 1, spinner * 2));
        rotateX = Matrix4x4.Rotate(Quaternion.Euler(spinner * 10.5f, 1, 1));
        Matrix4x4 negativeYrotate = Matrix4x4.Rotate(Quaternion.Euler(1, -2 * spinner, 1));
        Matrix4x4 negativeXrotate = Matrix4x4.Rotate(Quaternion.Euler(-2 * spinner, 1, 1));
        Matrix4x4 scale = Matrix4x4.Scale(new Vector3(1, spinner / 180 + 1, 1));

        rotateY = translate * rotateY * rotateX * rotateZ; //scale;
        //rotateX = translate * rotateX * negativeXrotate;
        int i = 0;
        while (i < origVerts.Length)
        {
            newVerts[i] = rotateY.MultiplyPoint(origVerts[i]);
            i++;
        }
        mf.mesh.vertices = newVerts;
    }
}
