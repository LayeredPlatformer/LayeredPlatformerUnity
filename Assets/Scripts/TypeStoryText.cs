using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;

public class TypeStoryText : MonoBehaviour {

	// TODO follow c# naming conventions
    public TextAsset FileName;
    public Text BoxText;
    public float TypeInterval;
    public int Counter;
    private string _fileText = "";
    private bool _typing = false;
    private bool _typed = false;

	//TODO: eventually clean up commented code
	
    public void Start()
    {
        if (FileName != null)
            _fileText = FileName.text;
//        _fileText = content();
    }
	
	// Update is called once per frame
	void Update () {
        if (!_typing && !_typed) 
            StartCoroutine(typeText(_fileText));//begin typing text
        else if(_typing)
            if(Input.anyKeyDown) //if a key is pressed, finish typing
                _typed = true;//will interrupt coroutine on next pass and type all remaining text.
        //Console.WriteLine("updating");
	}

    public void OnClick()
    {
        if(_typing) //if still typing, finish typing
        {
            _typed = true;//interrupt coroutine to type all remaining text instantly.
        }
        else if (_typed)//if done typing, move to next scene
        {
            //Put load scene stuff here.
        }
    }

    private IEnumerator typeText(string textString)
    {
        BoxText.text = "";
        _typing = true;
        Counter = 0;
        while (Counter < textString.Length && !_typed) 
        {
            BoxText.text += textString[Counter];
            Counter++;
            yield return new WaitForSeconds(TypeInterval);
        }
        BoxText.text = textString;
        _typed = true;
        _typing = false;
    }
}
