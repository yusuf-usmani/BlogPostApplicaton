using BOL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class UserDb
    {
        private BlogPostEntities db;

        public UserDb()
        {
            db = new BlogPostEntities();
        }

        public IEnumerable<tbl_user> GetALL()
        {
            return db.tbl_user.ToList();
        }
        public tbl_user GetByID(int Id)
        {
            return db.tbl_user.Find(Id);
        }
        public void Insert(tbl_user user)
        {
            db.tbl_user.Add(user);
            Save();
        }
        public void Delete(int Id)
        {
            tbl_user user = db.tbl_user.Find(Id);
            db.tbl_user.Remove(user);
            Save();
        }
        public void Update(tbl_user user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
