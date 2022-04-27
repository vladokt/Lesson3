namespace Lesson3
{
    public class Program
    {
        public static void Main()
        {
            int arg1;
            int arg2;
            int result;
            byte operationPos;
            bool isOperationFound = false;
            
                            
            do
            {
                Console.Clear();
                Console.WriteLine("Enter expression (e.g. 3*56, 23/7, 5+8, 67-39, 6%5):");
                string expression = Console.ReadLine();
                string[] strargs = expression.Split('*', '/', '+', '-');




                





                Console.WriteLine("Another expression? (y/n)");
            }
            while (Console.ReadLine() == "y");
            Console.WriteLine("Bye!");
                       
        }

    }
}