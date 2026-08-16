using System;
using System.Collections.Generic;
using System.Linq;

namespace SA_Pulse
{
    // Manages communities and performs CRUD operations
    internal class CommunityManager : ICommunityManager, IInputValidator
    {
        // Stores all communities managed by the system
        private List<Community> communities = new List<Community>();


        // Gets a valid whole number from the user
        public int GetValidInteger(string message)
        {
            int value;

            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }

                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }


        // Gets a rating between 1 and 10
        public int GetValidRating(string message)
        {
            int value;

            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out value))
                {
                    if (value >= 1 && value <= 10)
                    {
                        return value;
                    }
                }

                Console.WriteLine("Please enter a number between 1 and 10.");
            }
        }


        // Gets text from the user and makes sure it is not empty
        public string GetValidText(string message)
        {
            while (true)
            {
                Console.Write(message);

                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine("This field cannot be empty.");
            }
        }


        // Creates a new community and adds it to the list
        public void AddCommunity()
        {
            Console.WriteLine("\n===== ADD COMMUNITY =====");

            string name = GetValidText("Enter community name: ");

            int id = GetValidInteger("Enter community ID: ");

            // Makes sure that every community has a unique ID
            while (communities.Any(c => c.CommunityID == id))
            {
                Console.WriteLine("That Community ID already exists.");
                id = GetValidInteger("Enter another Community ID: ");
            }

            int population = GetValidInteger("Enter population: ");

            // Population cannot be negative
            while (population < 0)
            {
                Console.WriteLine("Population cannot be negative.");
                population = GetValidInteger("Enter population: ");
            }

            int pressure =
                GetValidRating("Enter population pressure (1-10): ");

            string riskLevel =
                GetValidText("Enter risk level: ");

            int pulseScore =
                GetValidInteger("Enter pulse score (0-100): ");

            // Pulse score must be between 0 and 100
            while (pulseScore < 0 || pulseScore > 100)
            {
                Console.WriteLine("Pulse score must be between 0 and 100.");

                pulseScore =
                    GetValidInteger("Enter pulse score (0-100): ");
            }

            // Creates a Community object using Person 1's class
            Community community = new Community(
                name,
                id,
                population,
                pressure.ToString(),
                riskLevel,
                pulseScore
            );

            // Adds the community to the list
            communities.Add(community);

            Console.WriteLine("Community added successfully.");
        }


        // Displays all communities stored in the system
        public void ViewCommunities()
        {
            Console.WriteLine("\n===== COMMUNITIES =====");

            if (communities.Count == 0)
            {
                Console.WriteLine("No communities found.");
                return;
            }

            foreach (Community community in communities)
            {
                Console.WriteLine(
                    $"ID: {community.CommunityID} | " +
                    $"Name: {community.CommunityName} | " +
                    $"Population: {community.Population} | " +
                    $"Pulse Score: {community.PulseScore} | " +
                    $"Status: {community.PulseStatus}"
                );
            }
        }


        // Finds a community and allows the user to update its information
        public void EditCommunity()
        {
            Console.WriteLine("\n===== EDIT COMMUNITY =====");

            if (communities.Count == 0)
            {
                Console.WriteLine("No communities available.");
                return;
            }

            int id = GetValidInteger("Enter Community ID to edit: ");

            // Finds the community with the entered ID
            Community community =
                communities.FirstOrDefault(c => c.CommunityID == id);

            if (community == null)
            {
                Console.WriteLine("Community not found.");
                return;
            }

            community.CommunityName =
                GetValidText("Enter new community name: ");

            int population =
                GetValidInteger("Enter new population: ");

            while (population < 0)
            {
                Console.WriteLine("Population cannot be negative.");

                population =
                    GetValidInteger("Enter new population: ");
            }

            community.Population = population;

            community.PopulationPressure =
                GetValidRating(
                    "Enter new population pressure (1-10): "
                ).ToString();

            community.RiskLevel =
                GetValidText("Enter new risk level: ");

            int pulseScore =
                GetValidInteger("Enter new pulse score (0-100): ");

            while (pulseScore < 0 || pulseScore > 100)
            {
                Console.WriteLine("Pulse score must be between 0 and 100.");

                pulseScore =
                    GetValidInteger(
                        "Enter new pulse score (0-100): "
                    );
            }

            community.PulseScore = pulseScore;

            Console.WriteLine("Community updated successfully.");
        }


        // Removes a community from the system
        public void DeleteCommunity()
        {
            Console.WriteLine("\n===== DELETE COMMUNITY =====");

            if (communities.Count == 0)
            {
                Console.WriteLine("No communities available.");
                return;
            }

            int id =
                GetValidInteger("Enter Community ID to delete: ");

            // Finds the community with the entered ID
            Community community =
                communities.FirstOrDefault(c => c.CommunityID == id);

            if (community == null)
            {
                Console.WriteLine("Community not found.");
                return;
            }

            // Removes the community from the list
            communities.Remove(community);

            Console.WriteLine("Community deleted successfully.");
        }


        // Displays detailed information about one community
        public void ViewCommunityDetails()
        {
            Console.WriteLine("\n===== COMMUNITY DETAILS =====");

            int id =
                GetValidInteger("Enter Community ID: ");

            // Finds the selected community
            Community community =
                communities.FirstOrDefault(c => c.CommunityID == id);

            if (community == null)
            {
                Console.WriteLine("Community not found.");
                return;
            }

            Console.WriteLine(
                "\nCommunity: " + community.CommunityName);

            Console.WriteLine(
                "ID: " + community.CommunityID);

            Console.WriteLine(
                "Population: " + community.Population);

            Console.WriteLine(
                "Population Pressure: " +
                community.PopulationPressure);

            Console.WriteLine(
                "Risk Level: " + community.RiskLevel);

            Console.WriteLine(
                "Pulse Score: " + community.PulseScore);

            Console.WriteLine(
                "Pulse Status: " + community.PulseStatus);
        }
    }
}