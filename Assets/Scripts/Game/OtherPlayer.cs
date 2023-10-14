using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    public Tile City;
    private int _id = 255;
    bool _lockId = false;

    private GameController _gameController;

    private GameObject[] _tiles;

    // Start is called before the first frame update
    void Start()
    {
        _tiles = GameObject.FindGameObjectsWithTag("Tile");
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void SetId(int newId)
    {
        if(!_lockId)
        {
            _id = newId;
            _lockId = true;
        }
        else
        {
            Debug.LogError("Tried to SetId on OtherPlayer when it was already set.");
        }
    }

    public int GetId()
    {
        return _id;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.ServerInput != null)
        {
            int newPosition = _gameController.ServerInput.PlayerUpdateHolder.Get(GetId());
            if (newPosition != -1)
            {
                foreach (GameObject city in _tiles)
                {
                    if (city.GetComponent<Tile>().GetId() == newPosition)
                    {
                        City = city.GetComponent<Tile>();
                    }
                }

                if (City != null)
                {
                  
                    transform.position = City.transform.position;
                }
            }
        }
    }
}
