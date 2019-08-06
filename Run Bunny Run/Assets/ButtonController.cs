using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private InputField input;
    private InputField output;

    // Start is called before the first frame update
    void Start()
    {
        input = this.GetComponent<InputField>();
        output = this.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetInputData()
    {
        input = GameObject.Find("inputUsername").GetComponent<InputField>();
        string username = input.text;
        output = GameObject.Find("outputField").GetComponent<InputField>();
        output.text = username;
        Debug.Log(username);
    }
}
