using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shoot : MonoBehaviour
{

    private float force = 2.0f;

    private Vector2 startPos;
    private bool tiro = false;
    private bool mirando = false;
    [SerializeField]
    private GameObject dotsGO;
    private List<GameObject> caminho;

    [SerializeField]
    private Rigidbody2D myRB;
    [SerializeField]
    private Collider2D myCollider;


    //Variáveis auxiliares
    [SerializeField] private float valorX, valorY;


    // Start is called before the first frame update
    void Start()
    {
        myRB.isKinematic = true;
        myCollider.enabled = false;
        startPos = transform.position;
        caminho = dotsGO.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        for (int x = 0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        Mirando();    
    }

    // METODOS

    void MostraCaminho()
    {
        for(int x=0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = true;
        }
    }

    void EscondeCaminho()
    {
        for(int x=0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = false;
        }
    }

    Vector2 PegaForca(Vector3 mouse)
    {
        return (new Vector2(startPos.x + valorX, startPos.y + valorY) - new Vector2(mouse.x, mouse.y)) * force;
    }

    Vector2 CaminhoPonto(Vector2 posInicial, Vector2 velInicial, float tempo)
    {
        return posInicial + velInicial * tempo + 0.5f * Physics2D.gravity * tempo * tempo;
    }

    void CalculoCaminho()
    {
        Vector2 vel = PegaForca(Input.mousePosition) * Time.fixedDeltaTime / myRB.mass;

        for (int x = 0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = true;
            float t = x / 20f;
            Vector3 point = CaminhoPonto(transform.position, vel, t);
            point.z = 1.0f;
            caminho[x].transform.position = point;
        }
    }

    void Mirando()
    {
        if(tiro == true)
        {
            return;
        }

        if(Input.GetMouseButton(0))
        {
            if(mirando == false)
            {
                mirando = true;
                startPos = Input.mousePosition;
                CalculoCaminho();
                MostraCaminho();
            }
            else
            {
                CalculoCaminho();
            }
        }
        else if(mirando && tiro == false)
        {
            myRB.isKinematic = false;
            myCollider.enabled = true;
            tiro = true;
            mirando = false;
            myRB.AddForce(PegaForca(Input.mousePosition));
            EscondeCaminho();
        }
    }
}


