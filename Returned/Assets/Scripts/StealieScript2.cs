using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealieScript2 : MonoBehaviour
{
    public Transform Player;
    public float Speed;
    public Transform[] MovePositions;

    private bool _isFollow,_isPlayerHolding,_isStealieHolding, _isStunned;
    private PlayerMovement playerMovement;
    private StealieManager stealieManager;

    public Transform holdSpot;
    // Start is called before the first frame update
    void Start()
    {
        _isFollow = true;
        Player= GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = FindObjectOfType<PlayerMovement>();
        stealieManager = FindObjectOfType<StealieManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _isPlayerHolding = playerMovement.holdingSomething;
        if(_isPlayerHolding && !_isStunned || stealieManager._previousItems<=4 && !_isStunned)
        {
            _isFollow = true;
        }
        else
        {
            _isFollow = false;
        }

        if (_isStealieHolding)
        {
            _isFollow = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(1f,1f,0f), Speed * Time.deltaTime);
            if(transform.position== new Vector3(1f, 1f, 0f))
            {
                playerMovement.heldObject.transform.SetParent(null);
                Rigidbody2D objRb = playerMovement.heldObject.GetComponent<Rigidbody2D>();
                objRb.simulated = true;
                playerMovement.heldObject.GetComponent<BoxCollider2D>().enabled = true;
                playerMovement.holdingSomething = false;
                _isStealieHolding = false;
            }
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
        if (collision.gameObject.tag == "Player")
        {
            playerMovement.heldObject.transform.SetParent(holdSpot);
            playerMovement.holdingSomething = false;
            _isStealieHolding = true;
        }

        if (collision.gameObject.tag=="Item")
        {
            StartCoroutine(Stunned());
        }
    }

    //Stops following player
    IEnumerator Stunned()
    {
        _isStunned = true;
        yield return new WaitForSeconds(3.0f);
        _isStunned = false;

    }
}
