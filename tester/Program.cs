namespace tester
{
    public class Program
    {
        public static void Main()
        {
            decimal i = 3.1415926M; object j = i;
            System.Console.WriteLine(j.GetHashCode());
        }
    }
}
