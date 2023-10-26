using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirclesApp
{
    public class DbConectionClass
    {
        static public CirclesDbEntities CirclesDBEntities = new CirclesDbEntities();
        static public Users user = new Users();
    }
}
