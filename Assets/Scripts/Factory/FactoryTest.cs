using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryTest : MonoBehaviour
{
    private ItemFactory factory = new ItemFactory();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [ContextMenu("Test")]
    public void Test()
    {
        Debug.Log(factory.Create(10000));
    }
}
