using WarehouseManagementSystem_Kristiqn_11g;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        WarehouseApp app = new WarehouseApp();
        app.Run();
    }
}