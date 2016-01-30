using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class MultiplicationQuest {

    Random rnd = new Random(Guid.NewGuid().GetHashCode());

    private MathProblem problem; //mathematical problem, contains term and correct solution
    private ArrayList options; //contains solution, its validity and if the option is already clicked
    readonly int amountOfOptions = 12;
    private int optionsClicked = 0;

    public MultiplicationQuest()
    {
        this.problem = createRndMultProblem();
        options = createOptions();
    }

    //creates a random multiplication problem
    private MathProblem createRndMultProblem()
    {
        int t1 = rnd.Next(1, 20);
        int t2 = rnd.Next(1, 20);
        return MathProblem.createMultProblem(t1, t2);
    }

    /// <summary>
    ///  creates 1 correct answer and several incorrect answers for the task
    /// </summary>
    //TODO: improve random incorrect solutions + avoid doubled answers
    private ArrayList createOptions()
    {
        float solution = problem.getSolution();
        ArrayList newOptions = new ArrayList();

        MathOption correctOption = new MathOption(solution, true);
        newOptions.Add(correctOption);

        for (int i = 0; i < amountOfOptions; i++)
        {
            int incorrectSolution = rnd.Next(1, 400);

            while (incorrectSolution == solution)                   //avoids the correct solution occuring additionally as incorrect solution
            {
                incorrectSolution = rnd.Next(1, 400);
            }
            newOptions.Add(new MathOption(incorrectSolution));
        }

        //giving the correct answer a random index
        int index = rnd.Next(0, 11);
        MathOption transAnswer = (MathOption)newOptions[index];
        newOptions[0] = transAnswer;
        newOptions[index] = correctOption;

        return newOptions;
    }

    public ArrayList getOptions()
    {
        return options;
    }

    public MathProblem getProblem()
    {
        return problem;
    }
}
