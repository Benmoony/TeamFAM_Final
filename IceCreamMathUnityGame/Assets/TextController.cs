using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public Text text;
    enum States { question_0, question_1, question_2, question_3, question_4, question_5, question_6, question_7 }
    int guessNum;
    int scoopNum = 0;
    int ranCherry;
    int ranBanana;
    public string setKey;
    public KeyCode kc;

    private States atQuest;
	// Use this for initialization
	void Start () {
        atQuest = States.question_0;
        ranCherry = Random.Range(1, 6);
        ranBanana = Random.Range(1, 4);
        guessNum = 0;
    }
	
	// Update is called once per frame
	void Update () {
        print(atQuest);

        if(atQuest == States.question_0)
        {
            state_quest0();
        }
        else if (atQuest == States.question_1)
        {
            state_quest1();
        }
        else if (atQuest == States.question_2)
        {
            state_quest2();
        }
        else if (atQuest == States.question_3)
        {
            state_quest3();
        }
        else if (atQuest == States.question_4)
        {
            state_quest4();
        }
        else if (atQuest == States.question_5)
        {
            state_quest5();
        }
        else if (atQuest == States.question_6)
        {
            state_quest6();
        }
        else if (atQuest == States.question_7)
        {
            state_quest7();
        }
    }

    void state_quest0()
    {

        text.text = "Welcome to the game! press S to start";

        if (Input.GetKeyDown(KeyCode.S))
        {
            guessNum = 0;
            atQuest = States.question_1;
        }
    }
    void state_quest1()
    {
        print("guessNum = " + guessNum);
        print("ranBanana = " + ranBanana);
        text.text = "How many bananas are in the tree?\n\n" +
                        "Answer by pressing 1, 2, or 3";

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            guessNum = guessNum + 1;
            if(ranBanana == 1)
            {
                atQuest = States.question_2;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            guessNum = guessNum + 1;
            if (ranBanana == 2)
            {
                atQuest = States.question_2;
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            guessNum = guessNum + 1;
            if (ranBanana == 3)
            {
                atQuest = States.question_2;
            }
        }
    }
    void state_quest2()
    {
        print("guessNum = " + guessNum);
        text.text = "Well done, there were "+ ranBanana +" bananas\n\n" + 
                    "How many scoops do you want to scoop?\n\n" +
                    "Answer by pressing 1, 2, or 3";

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            guessNum = guessNum + 1;
            scoopNum = 1;
            atQuest = States.question_3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            guessNum = guessNum + 1;
            scoopNum = 2;
            atQuest = States.question_3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            guessNum = guessNum + 1;
            scoopNum = 3;
            atQuest = States.question_3;
        }
    }
    void state_quest3()
    {
        text.text = "You scooped up " + scoopNum + " scoops of Ice cream\n\n" + "Pick "+ ranCherry +" cherries\n\n" + "Press the number to pick that many.";
        print("guessNum = " + guessNum);
        setKey = "Alpha" + ranCherry;
        kc = (KeyCode)System.Enum.Parse(typeof(KeyCode), setKey);

        if (Input.GetKeyDown(kc))
        {
            guessNum = guessNum + 1;
            atQuest = States.question_4;
        }
        else if (Input.anyKeyDown)
        {
            guessNum = guessNum + 1;
        }
    }
    void state_quest4()
    {
        text.text = "Place " + scoopNum + " Icecream scoops into the bowl.";
        setKey = "Alpha" + scoopNum;

        kc = (KeyCode)System.Enum.Parse(typeof(KeyCode), setKey);

        if (Input.GetKeyDown(kc))
        {
            atQuest = States.question_5;
        }
        else if (Input.anyKeyDown)
        {
            guessNum = guessNum + 1;
        }

    }
    void state_quest5()
    {
        text.text = "Add " + ranBanana + " Bananas to the Sundae";
        setKey = "Alpha" + ranBanana;
        kc = (KeyCode)System.Enum.Parse(typeof(KeyCode), setKey);
        if (Input.GetKeyDown(kc))
        {
            atQuest = States.question_6;
        }
        else if (Input.anyKeyDown)
        {
            guessNum = guessNum + 1;
        }
    }
    void state_quest6()
    {
        text.text = "Add " + ranCherry + " Cherries to the top of the Sunday";

        setKey = "Alpha" + ranCherry;
        kc = (KeyCode)System.Enum.Parse(typeof(KeyCode), setKey);
        if (Input.GetKeyDown(kc))
        {
            atQuest = States.question_7;
        }
        else if (Input.anyKeyDown)
        {
            guessNum = guessNum + 1;
        }
    }
    void state_quest7()
    {
        text.text = "Enjoy your Sunday!\n\n" + 
                    "To play again, press P";
        if (Input.GetKeyDown(KeyCode.P))
        {
            atQuest = States.question_0;
        }
    }
}
