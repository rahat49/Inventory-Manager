namespace InventoryManger.Models
{
    public static class Helper
    {
        public static string GetTypeName (string fulltypeName)
        {
            string retString = "";
            try
            {
                int lastIndex =fulltypeName.LastIndexOf ('.') +1;
                retString=fulltypeName.Substring(lastIndex,fulltypeName.Length - lastIndex);
            }
            catch
            {
                retString =fulltypeName;
            }
            
           retString = retString.Replace("]", "");
            
          
            return retString;

        }
    }
}
