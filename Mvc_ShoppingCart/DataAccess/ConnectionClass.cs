using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess
{
    public class ConnectionClass
    {
        public SecureShoppingCartDBEntities entities { get; set; }

        public ConnectionClass()
        {
            entities = new SecureShoppingCartDBEntities();
        }
    }
}
