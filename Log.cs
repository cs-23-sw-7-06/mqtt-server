public class Log{

    public static void GInfo(object obj){
        Console.Write("\r");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Info:\r\t\t" + obj);
        Console.ResetColor();
        Console.Write("> ");
    }
    public static void Info(object obj){
        Console.Write("\r");
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Info:\r\t\t"+ obj);
        Console.ResetColor();
        Console.Write("> ");
    }

    public static void Warning(object obj){
        Console.Write("\r");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Warning:\r\t\t" + obj);
        Console.ResetColor();
        Console.Write("> ");
    }

    public static void Error(object obj){
        Console.Write("\r");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error:\r\t\t" + obj);
        Console.ResetColor();
        Console.Write("> ");
    }
}