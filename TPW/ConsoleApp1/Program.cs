using TPW;
class Program
{
    static void Main(string[] args)
    {
        string? input = Console.ReadLine();
        if(input == "pl")
        {
            Console.WriteLine(Class1.GetString(0));
        }
        else
        {
            Console.WriteLine(Class1.GetString(1));
        }
    }
}