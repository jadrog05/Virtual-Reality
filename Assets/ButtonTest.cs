using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonTest : MonoBehaviour
{
    //points to action based controller script attached to Left5 Hand Controller object.
    [SerializeField] InputActionReference ActionUp;
    [SerializeField] InputActionReference ActionDown;
    public GameObject objectMove;
    
    // Start is called before the first frame update
    void Awake()
    {
        ActionUp.action.performed += ctx => MoveUp();
        ActionDown.action.performed += ctx => MoveDown();
    }
    private void MoveUp()
    {
        Vector3 pos = objectMove.transform.position;
        objectMove.transform.position = new Vector3(pos.x, pos.y + 0.1f, pos.z);
        //Debug.Log("wombat");
    }
    private void MoveDown()
    {
        Vector3 pos = objectMove.transform.position;
        objectMove.transform.position = new Vector3(pos.x, pos.y - 0.1f, pos.z);
        //Debug.Log("wombat2");
    }
}
