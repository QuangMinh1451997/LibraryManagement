using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Helper
    {
        public static void SetDefaultCreateModel(BaseModel model, string username)
        {
            model.CreateDate = DateTime.Today;
            model.CreateBy = username;
            model.EditDate = DateTime.Today;
            model.EditBy = username;
            model.Deleted = false;
        }

        public static void SetDefaultEditModel(BaseModel model, string username)
        {
            model.EditDate = DateTime.Today;
            model.EditBy = username;
        }

        public static int GetNextID(DbContext db, string tableName)
        {
            return Convert.ToInt32(db.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('"+tableName+"')").FirstOrDefault()) + 1;
        }
    }
}
