using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class DevTeams_Repo
    {
        private readonly List<DevTeam> _teamDatabase = new List<DevTeam>();

        private int _count = 0;

        public bool AddTeamToDatabase(DevTeam devTeam)
        {
            if(devTeam != null)
            {
                _count++;
                devTeam.ID = _count;
                _teamDatabase.Add(devTeam);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<DevTeam> GetAllTeams()
        {
            return _teamDatabase;
        }

        public DevTeam GetTeamByID(int id)
        {
            foreach(DevTeam devTeam in _teamDatabase)
            {
                if(devTeam.ID==id)
                {
                    return devTeam;
                }
            }
            return null;
        }

        public bool UpdateTeamData(int devTeamID, DevTeam newTeamData)
        {
            var oldTeamData = GetTeamByID(devTeamID);

            if(oldTeamData != null)
            {
                oldTeamData.Name = newTeamData.Name;
                oldTeamData.Developers = newTeamData.Developers;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveTeamFromDatabase(int id)
        {
            var devTeam = GetTeamByID(id);
            if(devTeam != null)
            {
                _teamDatabase.Remove(devTeam);
                return true;
            }
            else
            {
                return false;
            }
        }
    }