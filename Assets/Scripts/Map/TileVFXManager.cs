using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVFXManager : MonoBehaviour
{
    [SerializeField] GameObject tileTouchVFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TurnOnTileTouch(Vector3 pos)
    {
        tileTouchVFX.SetActive(true);
        tileTouchVFX.transform.position = pos;
    }

    public void TurnOffTileTouch()
    {
        tileTouchVFX.SetActive(false);
    }
}
