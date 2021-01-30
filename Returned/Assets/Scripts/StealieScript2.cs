using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealieScript2 : MonoBehaviour
{
    public Transform Player;
    public float Speed;

    private bool _isFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_isFollow)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Item")
        {

        }
    }

    IEnumerator Stunned()
    {
        _isFollow = false;
        yield return new WaitForSeconds(3.0f);
        _isFollow = true;

    }
}
