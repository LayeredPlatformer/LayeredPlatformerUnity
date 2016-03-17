using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;

public class TypeStoryText : MonoBehaviour {

	// TODO follow c# naming conventions
    public TextAsset filename;
    public Text text;
    public float TypeInterval;
    public int counter;
    private string fileText = "";
    private bool typing = false;
    private bool typed = false;

	//TODO: eventually clean up commented code
	
    public void Start()
    {
        if (filename != null)
            fileText = filename.text;
//        fileText = content();
    }

 /*   public string content()
    {
        Console.WriteLine("Trying to read file.");
        string output = "";
        try
        {
            using (StreamReader stream = new StreamReader(filename))
            {
                output = stream.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not read file.");
            Console.WriteLine(e.Message);
        }
        return output;
    }*/
	
	// Update is called once per frame
	void Update () {
        //        if (fileText.Equals(""))
        //            fileText = content();
		//TODO: remove brackets on single lined conditionals
        if (!typing && !typed) //begin typing text
        {
            StartCoroutine(typeText(fileText));
        }
        else if(typing)
        {
            if(Input.anyKeyDown) //if a key is pressed, finish typing
            {
                typed = true;//will interrupt coroutine on next pass and type all remaining text.
            }
        }
        //Console.WriteLine("updating");
	}

    public void OnClick()
    {
        if(typing) //if still typing, finish typing
        {
            typed = true;//interrupt coroutine to type all remaining text instantly.
        }
        else if (typed)//if done typing, move to next scene
        {
            //Put load scene stuff here.
        }
    }

    private IEnumerator typeText(string textString)
    {
        text.text = "";
        typing = true;
        counter = 0;
        while (counter < textString.Length && !typed) 
        {
            text.text += textString[counter];
            counter++;
            yield return new WaitForSeconds(TypeInterval);
        }
        text.text = textString;
        typed = true;
        typing = false;
    }
}
