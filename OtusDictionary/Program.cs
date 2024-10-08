namespace OtusDictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OtusDictionary otusDictionary = new();
            Random rand = new();

            try
            {
                for (int i = 1; i < 45; i++)
                {
                    otusDictionary.Add(i, $"Value = {i}");
                }
                //otusDictionary.Add(30, $"Value = 30");  //checking throw Exception

                Console.WriteLine("----Add all strings----");
                Console.WriteLine();

                string getValue = otusDictionary.Get(rand.Next(50));
                
                Console.WriteLine($"We get => {getValue}");

                Console.WriteLine($"Index[6] = {otusDictionary[6]}");
                Console.WriteLine($"Index[60] = {otusDictionary[60]}");
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
