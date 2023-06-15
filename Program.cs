namespace Conways
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string textFile = "./ConwayGrid.txt";

            var conwayGrid = new Grid(textFile);

            conwayGrid.Print();
            conwayGrid.ComputeNextGeneration();
            conwayGrid.Print();

        }

    }
}