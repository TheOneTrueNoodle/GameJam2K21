using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealieManager : MonoBehaviour
{
    private GameObject[] _items;

    public GameObject Stealie;

    private int _previousItems;

    // Start is called before the first frame update
    void Start()
    {
        _items = GameObject.FindGameObjectsWithTag("Item");
        _previousItems = _items.Length;
    }

    // Update is called once per frame
    void Update()
    {
        _items = GameObject.FindGameObjectsWithTag("Item");
        if (_items.Length<_previousItems)
        {
            Instantiate(Stealie, new Vector3(0, 1, 0), Quaternion.identity);
            _previousItems = _items.Length;
        }
    }
}
