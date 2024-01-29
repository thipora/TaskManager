namespace Stage0
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            wellcome4387();
            Console.ReadKey();
        }

        private static void wellcome4387()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
        static partial void wellcome4452();
    }
}