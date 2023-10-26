using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class pohybHrace : MonoBehaviour
{
    Rigidbody2D rigitbody;
    SpriteRenderer spriteRenderer;
    Vector2 vec;
    Vector2 direction;
    Vector2 pohb;
    [SerializeField]
    float speed;
    bool flip = false;
    [SerializeField]
    LayerMask layer;
    [SerializeField]
    GameObject rayes;
    
   
    // Start is called before the first frame update
    void Start()
    {
        rigitbody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        rigitbody.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          
            
            direction = (vec - rigitbody.position).normalized;

           
          /*  RaycastHit2D hit = Physics2D.Raycast(rayes.transform.position, direction,5, layer);
            RaycastHit2D hitTri = Physics2D.Raycast(rayes.transform.position+ new Vector3(0,1.5f), direction,0.5f, layer);
            RaycastHit2D hitDva = Physics2D.Raycast(rayes.transform.position+ new Vector3(0, -1.5f), direction,0.5f, layer);
            Debug.DrawRay(rayes.transform.position, direction.normalized * 5, Color.red, 0.1f);
            Debug.DrawRay(rayes.transform.position + new Vector3(0, 1.5f), direction.normalized * 5, Color.red, 0.1f);
            Debug.DrawRay(rayes.transform.position + new Vector3(0, -1.5f), direction.normalized * 5, Color.red, 0.1f);

            */






         /*   if (hit.collider != null || hitDva.collider != null || hitTri.collider != null)
            {
                direction = Vector2.zero;

            }
         */

            if (vec.x < rigitbody.position.x)
            {
                flip = true;
            }
            else
            {
                flip = false;
            }

            rigitbody.velocity = direction * speed;
        }
        if (Vector2.Distance(rigitbody.position, vec) < 0.1f)
        {
            rigitbody.velocity = Vector2.zero;
        }


        if (flip)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
