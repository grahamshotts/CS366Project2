using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class BWShaderControl : MonoBehaviour
{
    private bool changer = false;
    public Renderer randomShader;
    // Start is called before the first frame update
    void Start()
    {
        /*randomShader.material.SetFloat("_rChannel", (Random.Range(0f, 500f) / 500f));
        randomShader.material.SetFloat("_gChannel", (Random.Range(0f, 500f) / 500f));
        randomShader.material.SetFloat("_bChannel", (Random.Range(0f, 500f) / 500f));*/

        randomShader.material.SetFloat("_rChannel", 160f / 500f);
        randomShader.material.SetFloat("_gChannel", 32f / 500f);
        randomShader.material.SetFloat("_bChannel", 240f / 500f);
        StartCoroutine(timer());
    }

    // Update is called once per frame
    void Update()
    {

        if (changer == true)
        {
            //randomShader.material.SetFloat("_rChannel", (Random.Range(0f, 500f) / 500f));
            //randomShader.material.SetFloat("_gChannel", (Random.Range(0f, 500f) / 500f));
            //randomShader.material.SetFloat("_bChannel", (Random.Range(0f, 500f) / 500f));
            randomShader.material.SetFloat("_bChannel", (Random.Range(220f, 320f) / 500f));
            changer = false;
            StartCoroutine(timer());
        }
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(1f);
        changer = true;
    }
}

