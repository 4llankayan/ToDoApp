using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ToDoApp.Model
{
    public class ToDo
    {
        string _name;
        bool _done = false;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name {
            get => _name;

            set
            {
                if (value == null) throw new Exception("Nome inválido");
                _name = value;
            } 
        
        }
        public string Description { get; set; }
        public bool Done {
            get => _done;

            set
            {
                _done = value;
            }
        }
        public DateTime CreatedAt { get; set; }
    }
}
