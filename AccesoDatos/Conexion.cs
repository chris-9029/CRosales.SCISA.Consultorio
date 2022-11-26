using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class Conexion
    {
        public static string GetConnectionString()
        {
            return "Data Source=DESKTOP-EIT0CKC;Initial Catalog=SCISAConsultorio;"
                + "Trusted_Connection=False; encrypt=false; User ID=sa; Password=pass@word1;";
        }
    }
}
