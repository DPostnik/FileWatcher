using project.DA;

namespace project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FileInfoContext userContext = new FileInfoContext();
            
            userContext.SaveChanges();
        }
    }
}
