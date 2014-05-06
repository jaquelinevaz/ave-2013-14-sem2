using System;
using System.Windows.Forms;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Counter c = new Counter();
        c.AddObserver(value => Console.WriteLine("ConsoleHandler = " + value));
        c.AddObserver(value => MessageBox.Show("Item = " + value));
        c.AddObserver(new Observer(new A(2).FeedbackBeep));
        c.DoIt(5, 7);
    }
}

class A {

    int occurences;

    public A(int n){this.occurences = n;}

    public void FeedbackBeep(int value)
    {
        for (int i = 0; i < occurences; i++)
        {
            Console.Beep();
            Console.Write(" Hello : " + value);
        }
        Console.WriteLine();
    }

}


public delegate void Observer(int value);


class Counter
{
    private Observer obs;

    public void AddObserver(Observer o)
    {
        obs = (Observer) Delegate.Combine(obs, o);
    }

    public void RemoveObserver(Observer o)
    {
        obs = (Observer) Delegate.Remove(obs, o);
    }

    public void NotifyObservers(int n)
    {
        // obs(n); // <=> obs.Invoke(n);

        //if any callbacks are specified, call them
        
        foreach (Observer o in obs.GetInvocationList())
        {
            o(n); // <=> o.Invoke(n);
        }
        
    }

    // Notifica o m�todo de callback Feedback do objecto o,
    // por cada itera��o de from a to.
    public void DoIt(int from, int to)
    {
        for (int i = from; i <= to; i++)
        {
            NotifyObservers(i);
        }
    }
}