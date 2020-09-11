using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Rough
{
    //
    public delegate void Alerting(string vitalName, string message);
    class AddNewVitals
    {
        string vitalName;
        float lower;
        float upper;
        AddNewVitals(string vname,float lower,float upper)
        {
            this.vitalName = vname;
            this.lower = lower;
            this.upper = upper;
        }

    }
   

    class BPM
    {
        public event Alerting Alert;
        static float[] bpmLimits = { 70, 150 };
        public string message;
        
        internal void BpmCheck(float value)
        {
            //float[] bpmLimits = { 70, 150 };
            
                if (value < bpmLimits[0])
                {

                    // Console.WriteLine("BPM is low");
                   message = "BPM is LOW";
                    
                }
                else if (value > bpmLimits[1])
                {
                   message = "BPM is LOW";
                    //this.Alert.Invoke("BPM", msg);
                    //Console.WriteLine("BPM is high");
                }
                else
                {
                   message = "all okay";
                    //this.Alert.Invoke("BPM", msg);
                }
                //Console.WriteLine("all okay");

            

            
        }
        public  void Alerting()
        {
            if (this.Alert != null)
            {
                Console.WriteLine("-------------------");
                this.Alert.Invoke("BPM", this.message);
            }
        }
    }

    class SPO2
    {
        public event Alerting Alert;
        static float[] spo2Limits = { 90, 100 };
        public string message;

        internal  void Spo2Check(float value)
        {
            if (value < spo2Limits[0])
                message = "SPO2 is below";
            //Console.WriteLine("spo2 is low");
            else if (value > spo2Limits[1])
                message = "SPO2 is above";
            //Console.WriteLine("spo2 is high");
            else
                message = "SPO2 is all okay";
                //Console.WriteLine("all okay");
        }

        public void Alerting()
        {
            if (this.Alert != null)
            {
                Console.WriteLine("-------------------");
                this.Alert.Invoke("SPO2", this.message);
            }
        }
    }
    class RESPRATE
    {
        public event Alerting Alert;
        static float[] respRateLimits = { 30, 95 };
        public string message;
        internal  void RespRateCheck(float value)
        {
            if (value < respRateLimits[0])
                message = "RESPRATE IS BELOW";
            //Console.WriteLine("respRate is low");
            else if (value > respRateLimits[1])
                message = "RESPRATE iS ABOVE";
            //Console.WriteLine("RespRate is high");
            else
                message="RESPRATE IS OKAY";
                //Console.WriteLine("all okay");
        }

        public void Alerting()
        {
            if (this.Alert != null)
            {
                Console.WriteLine("-------------------");
                this.Alert.Invoke("RESPRATE", this.message);
            }
        }
    }

    //delegate
    public delegate void CheckVitals(float value);
    
    public class Checker
    {
        //delegate object
        CheckVitals check;
        public Checker(CheckVitals vitalName)
        {
            this.check = vitalName;
        }
        public void startCheck(float val)
        {
            this.check.Invoke(val);
        }

    }

    public class SMSAlert
    {
        public void Update(string vitalName,string message)
        {
            Console.WriteLine($"SMS::::given is details------------ {vitalName}-------- and-------- {message}");
        }
    }
    public class SoundAlert
    {
        public void Notify(string vitalName, string message)
        {
            Console.WriteLine($"SOUND::::given is details ------------{vitalName}--------------and------------------- {message}");
        }
    }
    class Program
    {
        static BPM bpm = new BPM();
        static SPO2 spo2 = new SPO2();
        static RESPRATE respRate = new RESPRATE();

        static void Main(string[] args)
        {
            
            CheckVitals _check = new CheckVitals(bpm.BpmCheck);
            Checker _checkerBpm = new Checker(new CheckVitals(_check));
            
            Checker _checkerSpo2 = new Checker(new CheckVitals(spo2.Spo2Check));
            
            Checker _checkerRespRate = new Checker(new CheckVitals(respRate.RespRateCheck));

            //give value
            _checkerBpm.startCheck(90);
            _checkerSpo2.startCheck(80);
            _checkerRespRate.startCheck(55);

            SMSAlert _sms = new SMSAlert();
            SoundAlert _sound = new SoundAlert();
            Alerting _handlerone = new Alerting(_sms.Update);
            Alerting _handlertwo = new Alerting(_sound.Notify);

            bpm.Alert += _handlerone;
            bpm.Alert += _handlertwo;
            spo2.Alert += _handlerone;
            spo2.Alert += _handlertwo;
            respRate.Alert += _handlerone;
            respRate.Alert += _handlertwo;

            bpm.Alerting();
            spo2.Alerting();
            respRate.Alerting();
            
        }
    }
}
