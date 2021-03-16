using System;
using System.Linq;

namespace CurrencyConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool userChoice = true;//Bool variable that allows the menu to loop if true
            while (userChoice) 
            {
                userChoice = MenuOptions();//While userChoice is true the menu options opens
            }
        }
        static bool MenuOptions()//The menu to allow users to choose what they wanna do
        {
            string choose;
            System.Console.WriteLine("1: Currency Conversion"); 
            System.Console.WriteLine("2: Restuarant POS");
            System.Console.WriteLine("3: Exit");
            choose = Console.ReadLine();
             switch (choose)
            {
                case "1":
                    ExchangeDirection();//Allows the user to do ExchangeRate if 1 is chosen
                    return true;

                case "2":
                    RestaurantPos(); //Allows the user to do RestaurantPOS if 2 is chosen
                    return true;   

                case "3":
                    System.Console.WriteLine("Exiting Menu, thanks for playing!");
                    return false;//If user chooses 3 it returns userChoice = false and ends the program

                default:
                    System.Console.WriteLine("Sorry not a valid choice, exiting the system"); //prompts the user that 1-3 was not selected and shows the menu again
                    return false;
            }
        }
        static void CurrencyTable()// Method to show the user the exchange rate and names of the currencies Bonus added extra currencies
        {
            System.Console.WriteLine("C = CanadianDollar (.9813)" + "\nE = Euro (.757)" + "\nI = IndianRupee (52.53)" + "\nJ = JapenseYen (80.92)" + "\nM = MexicanPeso (13.1544)" + "\nB = BritishPound (.6178)" + "\nS = SouthKoreanWon" + "\nR = RussianRuble"); 
            
        }
        static void ExchangeDirection()
        {
            string [] currencyNames = {"C", "E", "I", "J", "M", "B", "S", "R"}; //currency name array that stores all the available options the user can choose from
            double [] currencyAmount = {.9813, .757, 52.53, 80.92, 13.1544, .6178, 1103.21, 73.67}; //currency amount that stores what the userAmount can use to find there to and from currency
            CurrencyTable(); //prompts the currency table to know what to choose

            System.Console.WriteLine("What currency would you like to convert from?"); 
            string fromInput = Console.ReadLine(); //asks and stores users from currency

            CurrencyTable(); //prompts the currency table again

            System.Console.WriteLine("What currency would you like to convert to?");
            string toInput = Console.ReadLine(); //asks and stores users to currency

            ConversionPossibility(fromInput, toInput, currencyNames, currencyAmount); //calls the method to check if the currency selected is possible
        }

        static void ConversionPossibility(string fromInput, string toInput, string [] currencyNames, double [] currencyAmount)
        {
            int available = 0, fromExPlacement = 0, toExPlacement = 0; //varaible to store to see if user has choosen the right currency & stores the currency amount position
            for (var i = 0; i < currencyNames.Length; i++) //for statement that checks the letter of each currrencyNames array and stores how many times it took to find it
            {
                //currencySymbols[i] = currencyNames[i];
                if (fromInput == currencyNames[i]) //if user input contains the variable in the currency name for the from currenct exchange
                {
                    available = ++available; //updates the currenct status of available to 1 
                    System.Console.WriteLine("From currency accepted"); //prompts user to that currency is accepted
                    fromExPlacement = i; //variable to store the i variable to know what exchange amount to use dependent on when the varaible is found
                }
                if (toInput == currencyNames[i]) //if user input contains the variable in the currency name for the to currency exchange
                {
                    available = ++available; //if they choose the right to currency the value of available becomes 2
                    System.Console.WriteLine("To currency accepted"); //prompts user the currency is accepted
                    toExPlacement = i; //stores the variable of i to know what position the exchange amount is
                }
            }
            if (available == 2) //if the to and from currency is accepted it prompts the menu to exchange rate
            {
                ExchangeRate(currencyNames,currencyAmount, fromExPlacement, toExPlacement);
            }
            else 
            {
                ConversionError();
            }

        }
        static void ExchangeRate(string [] currencyNames, double [] currencyAmount, int fromExPlacement, int toExPlacement)//This is my extra points method that exchanges from currency to another currency
        {
            try //error handling if user doesnt input a number
            {
                double fromExchange = 0.0, toExchange = 0.0;
                System.Console.WriteLine("Enter the amount you wish to exchange"); //prompts user to ask how much he wishes to exchange
                double userAmount = int.Parse(Console.ReadLine()); //stores the userAmount he wishes to exchange
        

                if (userAmount <= 0)
                {
                    System.Console.WriteLine("Invalid Amount Please try again");
                    Console.ReadLine();
                }
                else
                {
                    fromExchange = (userAmount / currencyAmount[fromExPlacement]); //From Currency Exchange value to USD
                    System.Console.WriteLine("Your from currency exchange amount is $" + fromExchange); //displays their from exchange 
    
                    toExchange = (fromExchange * currencyAmount[toExPlacement]); //FromExchange to USD to USD to toCurrency Value also the currency symbol before the amount
                    System.Console.WriteLine("Your to currency exchange amount is " + currencyNames[toExPlacement] + " " + toExchange); //displays the toexchange amount
                    System.Console.WriteLine("Would you like to see what from Exchange to USD and To Exchange from from Exchange? :Y or N"); //prompts user to see if they want to see USD to From and From to To currency

                    if (Console.ReadLine() == "Y") //if user answers Y it calls the method
                    {
                        ExchangeFlip(fromExchange, toExchange, currencyNames, currencyAmount, fromExPlacement, toExPlacement, userAmount); //Method being called
                    } 
                    else
                    {
                        System.Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    
                }
            }
            catch (System.Exception e) // tells user the answer wasnt in correct format
            {
                System.Console.WriteLine(e.Message);
            }
        }
        static void ExchangeFlip(double fromExchange, double toExchange, string [] currencyNames, double [] currencyAmount, int fromExPlacement, int toExPlacement, double userAmount) //currency change method
        {
            double switchFrom = 0.0;
            double switchTo = 0.0;
            
            switchFrom = (userAmount * currencyAmount[fromExPlacement]); //from USD to fromExchange also the currency symbol before the amount
            System.Console.WriteLine("From USD to from currency exchange amount is " + currencyNames[fromExPlacement] + " " + switchFrom);

            switchTo = (switchFrom / currencyAmount[fromExPlacement]);//switch from to USD to USD to toCurrency(Value) also the currency symbol before the amount
            switchTo = (switchTo * currencyAmount[toExPlacement]);
            System.Console.WriteLine("Your from amount to USD to toCurrency is " + currencyNames[toExPlacement] + " " + switchTo);    
            System.Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        static void ConversionError()
        {
            System.Console.WriteLine("Conversion not possible");
            System.Console.WriteLine("Press a key to go back to menu");
            Console.ReadKey();
        }
        static void RestaurantPos() //Restarurant Method
        {
            try
            {
                double foodPrice = 0.0, alcoholPrice = 0.0;
                
                System.Console.WriteLine("How much was your food?"); //prompts and stores the users food cost
                foodPrice = double.Parse(Console.ReadLine());
                
                System.Console.WriteLine("How much was your alcohol?"); //prompts and stores the users alcohol cost
                alcoholPrice = double.Parse(Console.ReadLine()); 

                TaxTips(foodPrice, alcoholPrice); //calls the method to figure out the cost of the bill
            }
                
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        static void TaxTips(double foodPrice, double alcoholPrice)
        {
            double total = 0.0;
            foodPrice =  (foodPrice * .18) + foodPrice; //math that finds the tip amount for just the food
            total =((foodPrice + alcoholPrice) * .09 ) + (foodPrice + alcoholPrice); //math that finds the tax amount for the total bill
            
            ChangeDue(total);//calls the method for user to pay and give change
        }
        static void ChangeDue(double total)
        {
            try
            {
                double amountPaid = 0.0;
                System.Console.WriteLine("Your total amount due is $" + total); //tells user how much they owe
                System.Console.WriteLine("Please enter how much you wish to pay"); //stores and prompts the user for how much they wish to pay
                amountPaid = double.Parse(Console.ReadLine()); 
                System.Console.WriteLine("You paid $" + amountPaid);

                if (amountPaid >= total)//if amount paid is greater or equal to the total
                { 
                    amountPaid = amountPaid - total; //math that subtracts the amount paid by total
                    System.Console.WriteLine("Your change is $" + amountPaid); //prompts user for there change
                    Console.ReadLine();
                }
                else if(total > amountPaid) //if total is greater than amount paid
                {
                    total = total - amountPaid;
                    System.Console.WriteLine("Sorry not enough funds you still owe $" + total); //prompts the user that they didnt pay enough
                    Console.ReadLine();
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

        }
        
        
    }
}
