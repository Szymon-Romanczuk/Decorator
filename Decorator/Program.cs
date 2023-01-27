using System;
using System.Security.Cryptography.X509Certificates;

namespace Decorator
{
    public abstract class Component
    {
        public double price, avrage, optimal;
        public abstract string Operation();
    }
    class Base : Component
    {
        public Base(double distance)
        {
            var rnd = new Random();
            price = distance * (rnd.NextDouble() * 3.5 + 1.5);
            avrage = distance * (rnd.NextDouble() * 0.5);
            optimal = price * 0.8;
            //I shouldn't be storing money in double, but its the eaziest way to do that.            
        }
        public override string Operation()
        {
            return "Cena trasy wyniesie: " + price +"PLN \n";
        }

    }
    abstract class Decorator : Component
    {
        protected Component _component;

        public Decorator(Component component)
        {
            this._component = component;
            this.price = component.price;
            this.optimal = component.optimal;
            this.avrage = component.avrage;
        }

        public void SetComponent(Component component)
        {
            this._component = component;
        }
        public override string Operation()
        {
            if (this._component != null)
            {
                return this._component.Operation();
            }
            else
            {
                return string.Empty;
            }
        }
    }
    class Free : Decorator
    {
        public Free(Component comp) : base(comp)
        {
        }

        public override string Operation()
        {
            string s = base.Operation();
            return s + " Najlepsze pierogi na Mokotwie! \n";
        }
    }

    class SmallComapany : Decorator
    {
        public SmallComapany(Component comp) : base(comp)   
        {
        }
        public override string Operation()
        {
            string s = "Podana cena różni się od średniej rynkowej o " + avrage + "PLN \n";
            return base.Operation() + s;
        }
    }
    class Enterprise : Decorator
    {
        public Enterprise(Component comp) : base(comp)
        {
        }

        public override string Operation()
        {
            string s = "Cena zoptymalizowanej trasy to: " + optimal + "PLN \n";
            return base.Operation() + s;
        }
    }
    
    public class Client
    {
        public void Result(Component component)
        {
            Console.WriteLine(component.Operation());
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Console.WriteLine("Podaj ilość kilometrów w trasie");
            double distance = Convert.ToDouble(Console.ReadLine());
            var price = new Base(distance);


            Console.WriteLine("Podaj rodzaj subscrypcji (F/S/E)");
            string sub = Console.ReadLine();
            if(sub == "F")
            {
                Free free = new Free(price);
                client.Result(free);
            }
            else if (sub == "S")
            {
                SmallComapany small_company = new SmallComapany(price);
                client.Result(small_company);
            } 
            if(sub == "E")
            {
                SmallComapany small_company = new SmallComapany(price);
                Enterprise enterprise = new Enterprise(small_company);
                client.Result(enterprise);
            }
        }
    }
}
