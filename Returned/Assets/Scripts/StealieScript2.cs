using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealieScript2 : MonoBehaviour
{
    public Transform Player;
    public float Speed;

    private bool _isFollow,_isHolding;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        _isFollow = true;
        Player= GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _isHolding = playerMovement.holdingSomething;
        if(_isHolding)
        {
            _isFollow = false;
        }
        else
        {
            _isFollow = true;
        }

        //checks if Stealie can follow player
        if(_isFollow)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
        }
    }

    //checks if collides with item
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Item")
        {
            StartCoroutine(Stunned());
        }
    }

    //Stops following player
    IEnumerator Stunned()
    {
        _isFollow = false;
        yield return new WaitForSeconds(3.0f);
        _isFollow = true;

    }
}
