using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class Program_UI
    {
        private readonly Developer_Repo _dRepo = new Developer_Repo();
        private readonly DevTeams_Repo _dtRepo = new DevTeams_Repo();
        public void Run()
        {
            SeedData();
            RunApplication();
        }

    private void RunApplication()
    {
        bool isRunning = true;
        while(isRunning)
        {
            Console.Clear();
            System.Console.WriteLine("=== Welcome to Komodo Insurance ===");
            System.Console.WriteLine("Please make a selection: \n"+
            "1. Add Developer to Database\n"+
            "2. View All Developers\n"+
            "3. View Developer by ID\n"+
            "4. Remove Developer from Database\n"+
            "--------------------------------------\n"+
            "=== Team Database ===\n"+
            "5. Add Team to Database\n"+
            "6. View All Teams\n"+
            "7. View Team by ID\n"+
            "8. Update Team Data\n"+
            "9. Remove Team from Database\n"+
            "-------------------------------------\n"+
            "10. Close Application\n"
            );

            var userInput = Console.ReadLine();

            switch(userInput)
            {
                case "1":
                AddDeveloperToDatabase();
                break;
                case "2":
                ViewAllDevelopers();
                break;
                case "3":
                ViewDeveloperByID();
                break;
                case "4":
                RemoveDeveloperFromDatabase();
                break;
                case "5":
                AddTeamToDatabase();
                break;
                case "6":
                ViewAllTeams();
                break;
                case "7":
                ViewTeamByID();
                break;
                case "8":
                UpdateTeamData();
                break;
                case "9":
                RemoveTeamFromDatabase();
                break;
                case "10":
                isRunning = CloseApplication();
                break;
                default:
                System.Console.WriteLine("Invalid Selection");
                PressAnyKeyToContinue();
                break;
            }
        }
    }

    private void PressAnyKeyToContinue()
    {
        System.Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private bool CloseApplication()
    {
        return true;
    }

    private void RemoveTeamFromDatabase()
    {
        Console.Clear();
        System.Console.WriteLine("=== Team Removal Page ===\n");

        var teams = _dtRepo.GetAllTeams();
        foreach(DevTeam team in teams)
        {
            DisplayTeamListing(team);
        }

        try
        {
            System.Console.WriteLine("Please select a team by its ID:");
            var userInputSelectedTeam = int.Parse(Console.ReadLine());
            bool isSuccessful = _dtRepo.RemoveTeamFromDatabase(userInputSelectedTeam);
            if(isSuccessful)
            {
                System.Console.WriteLine("Team was successfully deleted");
            }
            else
            {
                System.Console.WriteLine("Team failed to be deleted");
            }
        }
        catch
        {
            System.Console.WriteLine("Sorry, invalid selection");
        }

        PressAnyKeyToContinue();
    }

    private void UpdateTeamData()
    {
        Console.Clear();
        var availTeams = _dtRepo.GetAllTeams();
        foreach(var team in availTeams)
        {
            DisplayTeamListing(team);
        }

        System.Console.WriteLine("Please enter a valid team ID");
        var userInputTeamID = int.Parse(Console.ReadLine());
        var userSelectedTeam = _dtRepo.GetTeamByID(userInputTeamID);

        if(userSelectedTeam != null)
        {
            Console.Clear();
            var newTeam = new DevTeam();

            var currentDevelopers = _dRepo.GetAllDevelopers();

            System.Console.WriteLine("Please enter a Team Name");
            newTeam.Name = Console.ReadLine();

            bool hasAssignedDevelopers = false;
            while(!hasAssignedDevelopers)
            {
                System.Console.WriteLine("Do you have any developers? y/n");
                var userInputHasDevelopers = Console.ReadLine();

                if(userInputHasDevelopers == "Y".ToLower())
                {
                    foreach(var developer in currentDevelopers)
                    {
                        System.Console.WriteLine($"{developer.ID} {developer.FirstName} {developer.LastName}");
                    }
                    var userInputDeveloperSelection = int.Parse(Console.ReadLine());
                    var selectedDeveloper = _dRepo.GetDeveloperByID(userInputDeveloperSelection);

                    if(selectedDeveloper != null)
                    {
                        newTeam.Developers.Add(selectedDeveloper);
                        currentDevelopers.Remove(selectedDeveloper);
                    }
                    else
                    {
                        System.Console.WriteLine($"Sorry, the developer with the ID: {userInputDeveloperSelection} does not exist");
                    }
                }
                else
                {
                    hasAssignedDevelopers = true;
                }
            }

            var isSuccessful = _dtRepo.UpdateTeamData(userSelectedTeam.ID,newTeam);
            if(isSuccessful)
            {
                System.Console.WriteLine("SUCCESS");
            }
            else
            {
                System.Console.WriteLine("FAILED");
            }
        }
        else
        {
            System.Console.WriteLine($"Sorry the team with the ID: {userInputTeamID} does not exist");
        }

        PressAnyKeyToContinue();
    }

    private void ViewTeamByID()
    {
        Console.Clear();
        System.Console.WriteLine("=== Team Details ===\n");

        var teams = _dtRepo.GetAllTeams();
        foreach(DevTeam team in teams)
        {
            DisplayTeamListing(team);
        }

        try
        {
            System.Console.WriteLine("Please select a team by its ID:");
            var userInputSelectedTeam = int.Parse(Console.ReadLine());
            var selectedTeam = _dtRepo.GetTeamByID(userInputSelectedTeam);
            if(selectedTeam !=null)
            {
                DisplayTeamDetails(selectedTeam);
            }
            else
            {
                System.Console.WriteLine($"Sorry the team with the ID: {userInputSelectedTeam} does not exist");
            }
        }
        catch
        {
            System.Console.WriteLine("Sorry, invalid selection");
        }

        PressAnyKeyToContinue();
    }

    private void DisplayTeamDetails(DevTeam selectedTeam)
    {
        Console.Clear();
        System.Console.WriteLine($"Team ID: {selectedTeam.ID}\n"+
        $"TeamName: {selectedTeam.Name}\n");

        System.Console.WriteLine("-- Developers --");
        if(selectedTeam.Developers.Count>0)
        {
            foreach(var developer in selectedTeam.Developers)
            {
                DisplayDeveloperInfo(developer);
            }
        }
        else
        {
            System.Console.WriteLine("There are no developers");
        }

        PressAnyKeyToContinue();
    }

    private void ViewAllTeams()
    {
        Console.Clear();
        System.Console.WriteLine("=== Team Listing ===\n");
        var teamsInDb = _dtRepo.GetAllTeams();

        foreach(var team in teamsInDb)
        {
            DisplayTeamListing(team);
        }

        PressAnyKeyToContinue();
    }

    private void DisplayTeamListing(DevTeam team)
    {
        System.Console.WriteLine($"TeamID: {team.ID}\n TeamName: {team.Name}\n"+
        "----------------------------------------------------------\n");
    }

    private void AddTeamToDatabase()
    {
        Console.Clear();
        var newTeam = new DevTeam();

        var currentDevelopers = _dRepo.GetAllDevelopers();

        System.Console.WriteLine("Please enter a Team Name");
        newTeam.Name = Console.ReadLine();

        bool hasAssignedDevelopers = false;
        while(!hasAssignedDevelopers)
        {
            System.Console.WriteLine("Do you have any developers? y/n");
            var userInputHasDevelopers = Console.ReadLine();

            if(userInputHasDevelopers == "Y".ToLower())
            {
                foreach(var developer in currentDevelopers)
                {
                    System.Console.WriteLine($"{developer.ID} {developer.FirstName} {developer.LastName}");
                }
                var userInputDeveloperSelection = int.Parse(Console.ReadLine());
                var selectedDeveloper = _dRepo.GetDeveloperByID(userInputDeveloperSelection);

                if(selectedDeveloper != null)
                {
                    newTeam.Developers.Add(selectedDeveloper);
                    currentDevelopers.Remove(selectedDeveloper);
                }
                else
                {
                    System.Console.WriteLine($"Sorry, the developer with the ID: {userInputDeveloperSelection} doesnt exist");
                }
            }
            else
            {
                hasAssignedDevelopers = true;
            }
        }
        bool isSuccessful = _dtRepo.AddTeamToDatabase(newTeam);
        if(isSuccessful)
        {
            System.Console.WriteLine($"Team: {newTeam.Name} was added to the database");
        }
        else
        {
            System.Console.WriteLine("Team failed to be added to the database");
        }

        PressAnyKeyToContinue();
    }

    private void RemoveDeveloperFromDatabase()
    {
        Console.Clear();
        System.Console.WriteLine("=== Developer Removal Page ===\n");

        var developers = _dRepo.GetAllDevelopers();
        foreach(Developer developer in developers)
        {
            DisplayDeveloperListing(developer);
        }

        try
        {
            System.Console.WriteLine("Please select a Developer by ID");
            var userInputSelectedDeveloper = int.Parse(Console.ReadLine());
            bool isSuccessful = _dRepo.RemoveDeveloperFromDatabase(userInputSelectedDeveloper);
            if(isSuccessful)
            {
                System.Console.WriteLine("Developer was successfully deleted");
            }
            else
            {
                System.Console.WriteLine("Developer failed to be deleted");
            }
        }
        catch
        {
            System.Console.WriteLine("Sorry, invalid selection");
        }

        PressAnyKeyToContinue();
    }

    private void DisplayDeveloperListing(Developer developer)
    {
        System.Console.WriteLine($"Developer ID: {developer.ID}\n DeveloperName: {developer.FirstName}{developer.LastName}\n"+
        "----------------------------------------------------------------\n");
    }

    private void ViewDeveloperByID()
    {
        Console.Clear();
        System.Console.WriteLine("=== Developer Detail Menu ===\n");
        System.Console.WriteLine("Please enter a Developer ID: \n");
        var userInputDeveloperID = int.Parse(Console.ReadLine());

        var developer = _dRepo.GetDeveloperByID(userInputDeveloperID);

        if(developer != null)
        {
            DisplayDeveloperInfo(developer);
        }
        else
        {
            System.Console.WriteLine($"The Developer with the ID: {userInputDeveloperID} does not exist");
        }

        PressAnyKeyToContinue();
    }

    private void ViewAllDevelopers()
    {
        Console.Clear();

        List<Developer> developersInDb = _dRepo.GetAllDevelopers();

        if(developersInDb.Count > 0)
        {
            foreach(Developer developer in developersInDb)
            {
                DisplayDeveloperInfo(developer);
            }
        }
        else
        {
            System.Console.WriteLine("There are no developers");
        }

        PressAnyKeyToContinue();
    }

    private void DisplayDeveloperInfo(Developer developer)
    {
        System.Console.WriteLine($"Developer ID: {developer.ID}\n"+
        $"DeveloperName: {developer.FirstName} {developer.LastName}\n"+
        "-------------------------------------------------------------\n");
    }

    private void AddDeveloperToDatabase()
    {
        Console.Clear();
        var newDeveloper = new Developer();
        System.Console.WriteLine("=== Developer Enlisting Form ===\n");

        System.Console.WriteLine("Please enter developer first name:");
        newDeveloper.FirstName = Console.ReadLine();

        System.Console.WriteLine("Please enter developer last name:");
        newDeveloper.LastName = Console.ReadLine();

        bool isSuccessful = _dRepo.AddDeveloperToDatabase(newDeveloper);
        if(isSuccessful)
        {
            System.Console.WriteLine($"{newDeveloper.FirstName} - {newDeveloper.LastName} was added to the database");
        }
        else
        {
            System.Console.WriteLine("Developer failed to be added to the database");
        }

        PressAnyKeyToContinue();
    }

    private void SeedData()
    {
        var jim = new Developer("Jim","Halpert");
        var michael = new Developer("Michael","Scott");
        var creed = new Developer("Creed", "Bratton");
        var kevin = new Developer("Kevin","Malone");

        _dRepo.AddDeveloperToDatabase(jim);
        _dRepo.AddDeveloperToDatabase(michael);
        _dRepo.AddDeveloperToDatabase(creed);
        _dRepo.AddDeveloperToDatabase(kevin);

        var teamA = new DevTeam("The Aristocrats",
        new List<Developer>
        {
            jim,
            michael
        });

        var teamB = new DevTeam("Scrantonicity",
        new List<Developer>
        {
            creed,
            kevin
        });

        _dtRepo.AddTeamToDatabase(teamA);
        _dtRepo.AddTeamToDatabase(teamB);
    }
}