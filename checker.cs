using System;
using System.Diagnostics;

class Checker
{ 
    abstract class Alert
	{
        public abstract void Alertmessage(string msg);

	}
    class SMSAlert:Alert
	{
        public override void Alertmessage(string msg)
        {
            Console.WriteLine(msg);
        }
	}
    class AlertInsound:Alert
    {
        public override void Alertmessage(string msg)
        {
            Console.WriteLine(msg);
        }
    }
    static bool vitalsIsOk(float value, float lower, float upper)
    {
        if (value>=lower && value<=upper)
        {
            return true;
        }
        smsAlert smsMessage=new SMSAlert();
        smsMessage.Alertmessage("all okay");
        AlertInSound soundMessage=new AlertInSound();
        soundMessage.Alertmessage("Vitals is not Ok");
        return false;
    }
    static bool vitalsAreOk(float bpm, float spo2, float respRate)
    {
        return (vitalsIsOk(bpm,70,150) && vitalsIsOk(spo2,90,100) && vitalsIsOk(respRate, 30, 95));
    }
    
    static void ExpectTrue(bool expression)
    {
        if (!expression)
        {
            Console.WriteLine("Expected true, but got false");
            Environment.Exit(1);
        }
    }
    static void ExpectFalse(bool expression)
    {
        if (expression)
        {
            Console.WriteLine("Expected false, but got true");
            Environment.Exit(1);
        }
    }
    static int Main() {
        ExpectTrue(vitalsAreOk(100, 95, 60));
        ExpectFalse(vitalsAreOk(40, 91, 92));
        Console.WriteLine("All ok");
        return 0;
    }
}
