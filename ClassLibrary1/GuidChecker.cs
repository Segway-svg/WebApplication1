namespace ClassLibrary1;

public static class GuidChecker
{
    public static Guid CheckGuid(string guidStr)
    {
        var guid = new Guid();
        
        if (Guid.TryParse(guidStr, out _))
        {
            guid = Guid.Parse(guidStr);
        }
        else
        {
            Console.WriteLine("Неверный формат, попробуйте снова");
        }
        return guid;
    }
}