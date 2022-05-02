namespace Lesson3
{
    using System.Text.RegularExpressions;
    public class Program
    {
        public static void Main()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Enter expression (e.g. 1+3*5-8/2*15-89):");
                string expression = Console.ReadLine();
                
                string pattern = @"\s+";
                Regex regex = new Regex(pattern);
                expression = regex.Replace(expression, "");

                pattern = @"(^\G(\d+(-|\+|\*|/))+\d+$)";
                regex = new Regex(pattern);
                if (regex.IsMatch(expression))
                {
                    pattern = @"(-)|(\+)|(\*)|(/)";
                    regex = new Regex(pattern);
                    string[] strargs = regex.Split(expression);

                    int result = 0;
                    int deepcount = 1;

                    try
                    {
                        for (int i = 1; (i + 2) <= strargs.Length; i += 2)
                        {
                            if (i == 1)
                            {
                                result = Compute(ref i, strargs, int.Parse(strargs[0]), ref deepcount);
                            }
                            else
                            {
                                result = Compute(ref i, strargs, result, ref deepcount);
                            }
                        }
                        Console.WriteLine(result.ToString());
                    }
                    catch (DivideByZeroException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch(OverflowException)
                    {
                        Console.WriteLine("Result is too large!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid expression!");
                }
                Console.WriteLine("Another one? (y/n)");
            }
            while (Console.ReadLine() == "y");
            Console.WriteLine("Bye!");
        }

        public static int Compute(ref int pos, string[] str, int prevres, ref int deep)
        {
            int res = prevres;

            if (((pos + 2) < str.Length) && ((str[pos + 2] == "*") || (str[pos + 2] == "/")))
            {
                pos += 2;
                deep++;

                if ((str[pos - 2] == "*") || (str[pos - 2] == "/"))
                {
                    switch (str[pos - 2])
                    {
                        case "*":
                            res = prevres * int.Parse(str[pos - 1]);
                            break;
                        case "/":
                            res = prevres / int.Parse(str[pos - 1]);
                            break;
                    }
                    res = Compute(ref pos, str, res, ref deep);
                }
                else
                {
                    res = Compute(ref pos, str, int.Parse(str[pos - 1]), ref deep);

                    switch (str[pos - 2*deep])
                    {
                        case "*":
                            res *= prevres;
                            break;
                        case "/":
                            res = prevres / res;
                            break;
                        case "+":
                            res += prevres;
                            break;
                        case "-":
                            res = prevres - res;
                            break;
                    }
                }
            }
            else
            {
                switch (str[pos])
                {
                    case "*":
                        res = prevres * int.Parse(str[pos + 1]);
                        break;
                    case "/":
                        res = prevres / int.Parse(str[pos + 1]);
                        break;
                    case "+":
                        res = prevres + int.Parse(str[pos + 1]);
                        break;
                    case "-":
                        res = prevres - int.Parse(str[pos + 1]);
                        break;
                }
                deep--;
            }
            return res;
        }
    }
}