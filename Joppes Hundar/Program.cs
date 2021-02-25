using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Joppes_Hundar
{
        abstract class Animal
        {
            protected int age;
            protected string name;
            protected string fav_food;
            protected string breed;
            protected bool hungry;

            public Animal(int Age, string Name, string Fav_food, string Breed, bool Hungry)
            {
                this.age = Age;
                this.name = Name;
                this.fav_food = Fav_food;
                this.breed = Breed;
                this.hungry = Hungry;

            }

            public string Name
            {
                get {return name;}
                set { name = value;}
            }

        public virtual void interact(Ball ball)
        {
            if (hungry == true)
                Console.WriteLine("{0} is hungry and doesn't want to play.", name);
            if (ball.Quality == 0)
                Console.WriteLine("The ball is not usable");

            if (hungry == false && ball.Quality >= 1)
            {
                Console.WriteLine("{0} chews the ball and plays.", name);
                ball.lower_quality(1);
                hungry = true;
            }

        }
           

        public virtual void eat(string food)
        {
                if (food == fav_food)
                {
                    Console.WriteLine("{0} loves this food and eats it up.",name);
                    hungry = false;
                }
                else 
                {
                    Console.Write("{0} doesn't like this food. ", name);
                    hungry_animal();
                    hungry = true;
                }
        }

            public abstract void hungry_animal();

            public override string ToString()
            {
                return string.Format("{0} is {1} years old, it's a {2} and loves {3}.", name, age, breed, fav_food);
            }

        }

        class Ball
        {
            private string color;
            private int quality;

            public Ball(int Quality, string Color)
            {
                this.color = Color;
                this.quality = Quality;
            }

            public int Quality
            {
                get {return quality;}
                set { quality = value;}
            }

            public string Color
            {
                get { return color; }
                set {color = value; }
            }

            public void lower_quality(int tal)
            {
                quality = quality - tal;
            }

            public override string ToString()
            {
                return string.Format("The ball is {0} and can be used {1} times.",color, quality);
            }

        }

        class Food
        {
            private string food_type;
            private int food_left;

            public Food(string Food_type, int Food_left)
            {
                this.food_type = Food_type;
                this.food_left = Food_left;
            }

            public int Food_left
            {
                get { return food_left; }
                set { food_left = value; }
            }

            public string Food_type
            {
                get { return food_type; }
                set { food_type = value; }
            }

            public override string ToString()
            {
                return string.Format("There are {0} cans of {1}.", food_left, food_type);
            }
        }

        class Petowner
        {
            private int age;
            private List<Animal> pets = new List<Animal> ();
            private List<Ball> balls = new List<Ball> ();
            private List<Food> food_pantry = new List<Food> ();

            public Petowner(int Age)
            {
                this.age = Age;
                this.balls = new List<Ball>();
                this.pets = new List<Animal>();
                this.food_pantry = new List<Food>();
            }

            public int Age
            {
                get { return age; }
                set { age = value; }
            }

            public void add_ball(string color)
            {
                balls.Add(new Ball(10, color));
                Console.WriteLine("The ball was successfully added, you can see it under toys menu.");
            }

        public void check_ball()
        {
            int quality_control = 0;

            foreach (var ball in balls)
            {
                Console.WriteLine(ball);
                if (ball.Quality == 0)
                    quality_control++;
            }


             if (quality_control != 0)
             {
                    Console.WriteLine("You have an unusable ball. Would you like to buy a new one? y/n");
                    string option = "";
                    bool control = true;
                    do
                    {
                        try
                        {
                            option = Console.ReadLine();
                        if (option == "Y" || option == "y" || option == "N" || option == "n")
                            control = false;
                        else
                            Console.WriteLine("Please only enter y or n");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input!");
                        }
                    } while (control);

                 if (option == "y" || option == "Y")
                 {
                        Console.WriteLine("What color would you like the ball to have?");
                        string color = "";

                    bool _control = true;
                    do
                    {
                        try
                        {
                            color = Console.ReadLine();
                            _control = false;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input!");
                        }
                    } while (_control);

                    add_ball(color);
                 }
             }
        }

            public void List_animals()
            {
                foreach (var temp in pets)
                    Console.WriteLine(temp);
            }
             
            public void fetch()
            {
                string name_temp = "";
                bool loop = true;
                bool _loop = true;
            do
            {
                Console.WriteLine("Who would you like to play with?  (1)Oreo  (2)Boots  (3)Bailey  (4)Coffee");
                int option = 0;
                do
                {
                    try
                    {
                        option = Convert.ToInt32(Console.ReadLine());
                        _loop = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please only enter integers from the menu.");
                    }

                } while (_loop);

                switch (option)
                {
                    case 1:
                        name_temp = "Oreo";
                        loop = false;
                        break;
                    case 2:
                        name_temp = "Boots";
                        loop = false;
                        break;
                    case 3:
                        name_temp = "Bailey";
                        loop = false;
                        break;
                    case 4:
                        name_temp = "Coffee";
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Please select an existing animal...");
                        break;
                } } while (loop == true);

            do
            {
                Console.WriteLine("Which color ball would you like to use?");
                foreach (var ball in balls)
                    Console.Write(ball.Color + "    ");

                Console.WriteLine();
                string ball_temp = "";
                bool temp = true;
                do
                {
                    try
                    {
                        ball_temp = Console.ReadLine();
                        temp = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input.");
                    }


                } while (temp);

                bool control = true;
                foreach (var pet in pets)
                {
                    if (pet.Name == name_temp)
                    {
                        foreach (var ball in balls)
                        {
                            if (ball.Color == ball_temp && ball.Quality != 0)
                            {
                                pet.interact(ball);
                                control = false;
                                loop = true;
                                break;
                            }
                        }
                    }
                }

                if (control == true)
                {
                    Console.WriteLine("The ball you picked either doesn't exist or is not usable");
                    loop = true;
                }

            } while (loop == false);

            
            }

            public void restock_pantry()
            {
              if (food_pantry != null)
                food_pantry.Clear();

                food_pantry.Add(new Food("raw food", 1));
                food_pantry.Add(new Food("canned food", 5));
                food_pantry.Add(new Food("home cooked food", 5));
                food_pantry.Add(new Food("milk", 5));
                Console.WriteLine("The pantry is restocked.");
                display_pantry();
            }

            public void display_pantry()
            {

            if (food_pantry.Count == 0)
            { Console.WriteLine("Pantry is empty, please restock."); }

            if (food_pantry.Count != 0)
            {
                Console.WriteLine("This is what you have in your pantry: ");
                foreach (var food in food_pantry)
                    Console.WriteLine(food);
            }

            }

        public void feed()
        {
            if (food_pantry.Count == 0)
                Console.WriteLine("Your pantry is empty, please restock!");
            if (food_pantry.Count != 0)
            {
                string name_temp = "";
                bool loop = true;
                int _option = 0;
                do
                {
                    Console.WriteLine("Who would you like to feed?  (1)Oreo  (2)Boots  (3)Bailey  (4)Coffee");
                    do
                    {
                        try 
                        {
                            _option = Convert.ToInt32(Console.ReadLine());
                                loop = false;
                            
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input.");
                        }
                    } while (loop);


                    switch (_option)
                    {
                        case 1:
                            name_temp = "Oreo";
                            loop = true;
                            break;
                        case 2:
                            name_temp = "Boots";
                            loop = true;
                            break;
                        case 3:
                            name_temp = "Bailey";
                            loop = true;
                            break;
                        case 4:
                            name_temp = "Coffee";
                            loop = true;
                            break;
                        default:
                            Console.WriteLine("Please select an existing animal...");
                            break;
                    }
                } while (loop == false);

                
                string food_temp = "";
                bool _loop = false;
                do
                {
                    Console.WriteLine("What are you feeding it?  (1)milk  (2)home cooked food  (3)canned food  (4)raw food");
                    int option = 0;
                    do
                    {
                        try
                        {
                            option = Convert.ToInt32(Console.ReadLine());
                             _loop = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input.");
                        }
                    } while (_loop == false);

                    switch (option)
                    {
                        case 1:
                            food_temp = "milk";
                            _loop = false;
                            break;
                        case 2:
                            food_temp = "home cooked food";
                            _loop = false;
                            break;
                        case 3:
                            food_temp = "canned food";
                            _loop = false;
                            break;
                        case 4:
                            food_temp = "raw food";
                            _loop = false;
                            break;
                        default:
                            Console.WriteLine("Please select from the menue...");
                            break;
                    }
                } while (_loop == true);

                int choice = 0;
                foreach (var pet in pets)
                {
                    if (pet.Name == name_temp)
                    {
                        foreach (var food in food_pantry)
                        {
                            if (food_temp == food.Food_type)
                            {
                                if (food.Food_left == 0)
                                {
                                    Console.WriteLine("There's no more {0}, you need to restock.", food.Food_type);
                                    Console.WriteLine("Would you like to buy some? (0)No  (1)Yes  ");
                                    bool iteration = true;
                                    do
                                    {
                                        try
                                        {
                                            choice = Convert.ToInt32(Console.ReadLine());
                                            if (choice == 0 || choice == 1)
                                                iteration = false;
                                            else
                                                Console.WriteLine("Please only enter 0 or 1");
                                        }

                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Invalid input.");
                                        }
                                    } while (iteration);

                                    if (choice == 1)
                                        food_pantry.Remove(food);
                                    break;
                                }
                                if (food.Food_left != 0)
                                {
                                    pet.eat(food_temp);
                                    food.Food_left--;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (choice == 1)
                {
                    food_pantry.Add(new Food(food_temp, 2));
                    Console.WriteLine("{0} was restocked successfully", food_temp);
                }
            }
        }

        public void menu()
        {
            pets.Add(new Dog(5, "Bailey", "Golden Retriever", false));
            pets.Add(new Cat(8, "Coffee", "American Shorthair", false));
            pets.Add(new Puppy(3, "Boots", "Corgi", false));
            pets.Add(new Kitten(2, "Oreo", "Munchkin", true));

            balls.Add(new Ball(0, "blue"));
            balls.Add(new Ball(10, "pink"));

            int choice = 0;

            do
            {
                Console.WriteLine();
                Console.WriteLine("Please select...");
                Console.WriteLine("(1) See the animals");
                Console.WriteLine("(2) Play with a pet");
                Console.WriteLine("(3) Feed an animal");
                Console.WriteLine("(4) Add a new ball");
                Console.WriteLine("(5) Check the toys");
                Console.WriteLine("(6) Display the pantry");
                Console.WriteLine("(7) Restock the pantry");
                Console.WriteLine("(0) Exit");

                bool control = true;
                do
                {
                    try
                    {
                        choice = Convert.ToInt32(Console.ReadLine());
                        control = false; 
                    }
                    catch (FormatException) 
                    {
                        Console.WriteLine("Invalid input, please enter only an integer!");
                    }
                } while (control);

                switch (choice)
                {
                    case 1:
                        List_animals();
                        break;
                    case 2:
                        fetch();
                        break;
                    case 3:
                        feed();
                        break;
                    case 4:
                        Console.WriteLine("What color ball would you like?");
                        bool loop = true;
                        string color = "";
                        do
                        {
                            try
                            {
                                color = Console.ReadLine();
                                loop = false;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input.");
                            }
                        } while (loop);
                        add_ball(color);
                        break;
                    case 5:
                        check_ball();
                        break;
                    case 6:
                        display_pantry();
                        break;
                    case 7:
                        restock_pantry();
                        break;
                    case 0:
                        Console.WriteLine("Programme ends...");
                        break;

                    default: 
                        Console.WriteLine("Invalid input! Please select a number of the menu:");
                        break;

                }

            } while (choice != 0);
        }
       
            public override string ToString()
            {
                return string.Format("Our family owner, Joppe, is {0} years old and loves animals.", age);
            }

        }

        class Cat : Animal
        {
            public Cat(int Age, string Name, string Breed, bool Hungry, string Fav_food = "raw food") : base(Age, Name, Fav_food, Breed, Hungry)
            {
                this.fav_food = Fav_food;
            }
            public override void interact(Ball ball)
            {
                base.interact(ball);
            }

            public override void hungry_animal()
            {
                Console.WriteLine("The cat is hungry and tries to catch a mouse.");
                Random probability = new Random();
                hungry = probability.Next(100) < 50 ? true : false;
                if (hungry == true)
                    Console.WriteLine("It couldn't catch a mice and is still hungry.");
                else
                    Console.WriteLine("The cat caught a mice and is full now.");
            }

        public override void eat(string food)
        {
            if (hungry == false)
                Console.WriteLine("{0} isn't hungry right now.", name);
            else
                base.eat(food);
        }

            public override string ToString()
            {
                return base.ToString();
            }
        }

        class Kitten : Cat
        {
            int age_weeks;
           
            public Kitten(int Age_weeks, string Name, string Breed, bool Hungry, int Age = 0, string Fav_food = "milk") : base(Age, Name, Breed, Hungry, Fav_food)
            {
                this.fav_food = Fav_food;
                this.age_weeks = Age_weeks;
            }

            public override void interact(Ball ball)
            {
                Console.WriteLine("{0} is too tiny and cannot play yet.", name);
            }

            public override void hungry_animal()
            {
                Console.WriteLine("{0} is hungry and cries.", name);
            }

            public override void eat(string food)
            {
                base.eat(food);
            }

            public override string ToString()
            {
            return string.Format("This kitty is {0} years old sinces it's only {1} weeks. {2} is a {3} and loves {4}.", age, age_weeks, name, breed, fav_food);
        }

        }

        class Dog : Animal 
        {
            public Dog(int Age, string Name, string Breed, bool Hungry, string Fav_food = "canned food") : base (Age, Name, Fav_food, Breed, Hungry)
            {
                this.fav_food = Fav_food;
            }

        public override void interact(Ball ball)
        {
            if (hungry == true)
                Console.WriteLine("{0} is hungry and doesn't want to play.", name);
            if (ball.Quality <= 1)
                Console.WriteLine("{0} cannot use this ball.", name);

            if (hungry == false && ball.Quality >= 2)
            {
                Console.WriteLine("{0} chews the ball and plays.", name);
                ball.lower_quality(2);
                hungry = true;
            }

        }

            public override void hungry_animal()
            {
                Console.WriteLine("{0} is hungry and doesn't want to play.", name);
            }

            public override void eat(string food)
            {
                base.eat(food);
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }

        class Puppy : Dog
        {
            int age_months;

            public Puppy(int Age_months, string Name, string Breed, bool Hungry, string Fav_food = "home cooked food", int Age = 0) : base (Age, Name, Breed, Hungry)
            {
                this.fav_food = Fav_food;
                this.age_months = Age_months;
            }

        public override void interact(Ball ball)
        {
            if (hungry == false)
            {
                Console.WriteLine("{0} chews the ball and plays.", name);
                ball.lower_quality(1);
                hungry = true;
            }
            else
                Console.WriteLine("{0} is hungry and doesn't want to play.", name);
        }

            public override void hungry_animal()
            {
                Console.WriteLine("{0} whines, because it's hungry :(", name);
                hungry = true;
            }

            public override void eat(string food)
            {
                base.eat(food);
            }

            public override string ToString()
            {
                return string.Format("This puppy is {0} years old sinces it's only {1} months. {2} is a {3} and loves {4}.", age, age_months, name, breed, fav_food);
            }
        }


    class Program
    {
        static void Main(string[] args)
        {
            var Joppe = new Petowner(35);
            Console.WriteLine("Welcome to Joppe's animal farm!");
            Console.WriteLine(Joppe);
            Joppe.menu();
            System.Threading.Thread.Sleep(10000);
        }

    }
}
