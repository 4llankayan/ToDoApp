using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ToDoApp.Model;

namespace ToDoApp.Helper
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<ToDo>().Wait();
        }

        public Task<int> Insert(ToDo t)
        {
            return _conn.InsertAsync(t);
        }

        public Task<List<ToDo>> Update(ToDo t)
        {
            string sql = "UPDATE ToDo SET Name = ?, Description = ?, Done = ?, CreatedAt = Now()";

            return _conn.QueryAsync<ToDo>(sql, t.Name, t.Description, t.Done, t.Id);
        }

        public Task<List<ToDo>> GetAll()
        {
            return _conn.Table<ToDo>().ToListAsync();
        }

        public Task<List<ToDo>> GetAllDone()
        {
            return _conn.Table<ToDo>().Where(i => i.Done == true).ToListAsync();
        }

        public Task<List<ToDo>> GetAllPendent()
        {
            return _conn.Table<ToDo>().Where(i => i.Done == false).ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<ToDo>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<ToDo>> Search(string q)
        {
            string sql = "SELECT * FROM ToDo WHERE Name LIKE '%" + q + "%'";

            return _conn.QueryAsync<ToDo>(sql);
        }
    }
}
