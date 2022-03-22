using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class Developer_Repo
    {
        private readonly List<Developer> _developerDatabase = new List<Developer>();
        private int _count;
        public bool AddDeveloperToDatabase(Developer developer)
        {
            if(developer != null)
            {
                _count++;
                developer.ID = _count;
                _developerDatabase.Add(developer);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Developer> GetAllDevelopers()
        {
            return _developerDatabase;
        }

        public Developer GetDeveloperByID(int id)
        {
            foreach(var developer in _developerDatabase)
            {
                if(developer.ID == id)
                {
                    return developer;
                }
            }
            return null;
        }

        public bool RemoveDeveloperFromDatabase(int id)
        {
            var developer = GetDeveloperByID(id);
            if(developer != null)
            {
                _developerDatabase.Remove(developer);
                return true;
            }
            else
            {
                return false;
            }
        }
    }