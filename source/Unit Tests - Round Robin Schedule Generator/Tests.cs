using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Diagnostics;
using SomeTechie.RoundRobinScheduleGenerator;

namespace Unit_Tests___Round_Robin_Schedule_Generator
{
    [TestFixture]
    class Tests
    {
        protected const int minTeams = 2;
        protected const int maxTeams = 20;
        protected const int minDivisions = 1;
        protected const int maxDivisions = 4;
        protected const int minCourts = 1;
        protected const int maxCourts = 10;
        protected int[][] gamesToTest = new int[][] { new int[] { 8 }};

        /* [SetUp]
         public void generateTournamentObjects()
         {
         }*/
        protected int _getNextDivisionSpecs_numDivisions = 0;
        private List<int[]> getNextDivisionSpecs(int[] initialSpecs, int minTeams, int maxTeams, bool includeInitialValue = false, int maxNum = 10)
        {
            int numDivisions = initialSpecs.Length;
            List<int[]> divisionsSpecs = new List<int[]>();
            int[] currentDivisionSpecs = (int[])initialSpecs.Clone();
            if (includeInitialValue) divisionsSpecs.Add((int[])currentDivisionSpecs.Clone());
            bool isFinished = false;
            while (divisionsSpecs.Count < maxNum)
            {
                for (int i = numDivisions - 1; i >= 0; i--)
                {
                    if (currentDivisionSpecs[i] >= maxTeams)
                    {
                        if (i == 0)
                        {
                            isFinished = true;
                            break;
                        }
                        currentDivisionSpecs[i] = minTeams;
                    }
                    else
                    {
                        currentDivisionSpecs[i]++;
                        break;
                    }
                }
                if (isFinished) break;
                divisionsSpecs.Add((int[])currentDivisionSpecs.Clone());
            }

            return divisionsSpecs;
        }
        private int[] buildDefaultDivisionSpecs(int numDivisions, int minTeams, int maxTeams)
        {
            int[] initialDivisionSpecs = new int[numDivisions];
            for (int i = 0; i < numDivisions; i++)
            {
                initialDivisionSpecs[i] = minTeams;
            }
            return initialDivisionSpecs;
        }
        private List<Division> buildDivisions(int[] numTeamsInDivisions)
        {
            List<Division> divisions = new List<Division>();
            int divisionNumber = 1;
            foreach (int numTeams in numTeamsInDivisions)
            {
                string divisionNumberString = divisionNumber.ToString();
                string divisionName = "Division" + divisionNumberString;
                string divisionAbbreviation = "D" + divisionNumberString + "_";
                Division division = new Division(divisionName, divisionAbbreviation);
                List<Team> teams = new List<Team>();
                for (int i = 1; i <= numTeams; i++)
                {
                    teams.Add(new Team(divisionAbbreviation + i.ToString(), i));
                }
                division.Teams = teams;
                divisions.Add(division);
                divisionNumber++;
            }
            return divisions;
        }

        private void NotSameTime(Tournament tournament)
        {
            foreach (CourtRound courtRound in tournament.CourtRounds)
            {
                List<int> teamsUsed = new List<int>();
                foreach (Game game in courtRound.Games)
                {
                    foreach (Team team in game.Teams)
                    {
                        Assert.IsFalse(teamsUsed.Contains(team.NumId), GetTournamentId(tournament) + " - {0} exists more than one time in Court Round {1}", team, courtRound.RoundNumber);
                        teamsUsed.Add(team.NumId);
                    }
                }

            }
        }
        private void PreferBetterTeams(Tournament tournament)
        {
            if (tournament.NumCourts < 2) return;
            foreach (CourtRound courtRound in tournament.CourtRounds)
            {
                int previousDivisionIndex = -1;
                foreach (Game game in courtRound.Games)
                {
                    int divisionIndex = tournament.Divisions.IndexOf(game.Division);
                    Assert.GreaterOrEqual(divisionIndex, previousDivisionIndex, GetTournamentId(tournament) + " - Prefer better teams failed In Court Round {0}", courtRound.RoundNumber);
                    previousDivisionIndex = divisionIndex;
                }
            }
        }
        private void NoVacantCourts(Tournament tournament)
        {
            int maxGamesPerCourtRound = 0;
            foreach (Division division in tournament.Divisions)
            {
                maxGamesPerCourtRound += (int)Math.Floor((decimal)division.Teams.Count / 2);
            }
            if (tournament.NumCourts < maxGamesPerCourtRound) maxGamesPerCourtRound = tournament.NumCourts;
            foreach (CourtRound courtRound in tournament.CourtRounds)
            {
                if (courtRound.RoundNumber != tournament.CourtRounds.Count) Assert.AreEqual(maxGamesPerCourtRound, courtRound.numGames, GetTournamentId(tournament) + " - Vacant courts in Court Round {0}", courtRound.RoundNumber);
            }
        }
        private void DistributeTeamsAcrossCourts(Tournament tournament)
        {
            if (tournament.NumCourts < 2) return;
            Dictionary<Division, Dictionary<int, Dictionary<int, int>>> timesTeamsOnCourtByDivision = new Dictionary<Division, Dictionary<int, Dictionary<int, int>>>();
            foreach (CourtRound courtRound in tournament.CourtRounds)
            {
                foreach (Game game in courtRound.Games)
                {
                    if (!timesTeamsOnCourtByDivision.ContainsKey(game.Division)) timesTeamsOnCourtByDivision.Add(game.Division, new Dictionary<int, Dictionary<int, int>>());
                    Dictionary<int, Dictionary<int, int>> timesTeamsOnCourtForDivision = timesTeamsOnCourtByDivision[game.Division];
                    foreach (Team team in game.Teams)
                    {
                        if (!timesTeamsOnCourtForDivision.ContainsKey(team.NumId)) timesTeamsOnCourtForDivision.Add(team.NumId, new Dictionary<int, int>());
                        Dictionary<int, int> timesTeamOnCourt = timesTeamsOnCourtForDivision[team.NumId];
                        if (timesTeamOnCourt.ContainsKey(game.CourtNumber)) timesTeamOnCourt[game.CourtNumber]++;
                        else timesTeamOnCourt.Add(game.CourtNumber, 1);
                    }
                }
            }
            int largestDistributionDifference = int.MinValue;
            string largestDistributionDiffereceId = "";
            foreach (KeyValuePair<Division, Dictionary<int, Dictionary<int, int>>> timesOnCourtForDivision in timesTeamsOnCourtByDivision)
            {
                Dictionary<int, int> MaxTimesOnCourt = new Dictionary<int, int>();
                Dictionary<int, int> MinTimesOnCourt = new Dictionary<int, int>();
                //Initilize the values for the FirstTimesOnCourt dictionary
                for (int i = 1; i <= tournament.NumCourts; i++)
                {
                    MaxTimesOnCourt.Add(i, int.MinValue);
                    MinTimesOnCourt.Add(i, int.MaxValue);
                }
                foreach (KeyValuePair<int, Dictionary<int, int>> timesOnCourtForTeam in timesOnCourtForDivision.Value)
                {

                    foreach (KeyValuePair<int, int> timesOnCourtPair in timesOnCourtForTeam.Value)
                    {
                        int courtNumber = timesOnCourtPair.Key;
                        int timesOnCourt = timesOnCourtPair.Value;
                        if (timesOnCourt > MaxTimesOnCourt[courtNumber]) MaxTimesOnCourt[courtNumber] = timesOnCourt;
                        if (timesOnCourt < MinTimesOnCourt[courtNumber]) MinTimesOnCourt[courtNumber] = timesOnCourt;
                    }
                }
                for (int i = 1; i <= tournament.NumCourts; i++)
                {
                    int difference = MaxTimesOnCourt[i] - MinTimesOnCourt[i];
                    if (difference > largestDistributionDifference)
                    {
                        largestDistributionDifference = difference;
                        largestDistributionDiffereceId = "on " + CourtRound.CourtNumToCourtName(i) + " " + timesOnCourtForDivision.Key.Name;
                    }
                }
            }
            if (largestDistributionDifference > 5)
            {
                Trace.WriteLine(GetTournamentId(tournament) + " - Largest team distribution difference: " + largestDistributionDifference.ToString() + "; " + largestDistributionDiffereceId);
            }
        }

        private void TeamsPlayTeams(Tournament tournament)
        {
            foreach (Division division in tournament.Divisions)
            {
                foreach (Team team1 in division.Teams)
                {
                    foreach (Team team2 in division.Teams)
                    {
                        if (team1 == team2) continue;
                        bool foundGame = false;
                        foreach (CourtRound courtRound in tournament.CourtRounds)
                        {
                            foreach (Game game in courtRound.Games)
                            {
                                if ((game.Team1 == team1 && game.Team2 == team2) || (game.Team1 == team2 && game.Team2 == team1))
                                {
                                    foundGame = true;
                                    break;
                                }
                            }
                            if (foundGame) break;
                        }
                        Assert.IsTrue(foundGame, GetTournamentId(tournament) + " - {0} never plays {1}", team1, team2);
                    }
                }
            }
        }

        private void CorrectNumberOfGamesInRobinRounds(Tournament tournament)
        {
            Dictionary<int, Dictionary<int, int>> numberOfGamesPerRobinRoundByDivision = new Dictionary<int, Dictionary<int, int>>();
            int numberOfRobinRounds = 0;
            foreach (Division division in tournament.Divisions)
            {
                int num = division.Teams.Count;
                if (num % 2 == 0) num--;
                if(num>numberOfRobinRounds)numberOfRobinRounds=num;
            }
            foreach (Division division in tournament.Divisions)
            {
                Dictionary<int,int> numberOfGamesPerRobinRoundForDivision = new Dictionary<int,int>();
                for (int robinRoundNum = 0; robinRoundNum < numberOfRobinRounds; robinRoundNum++)
                {
                    numberOfGamesPerRobinRoundForDivision.Add(robinRoundNum, 0);
                }
                numberOfGamesPerRobinRoundByDivision.Add(division.NumId, numberOfGamesPerRobinRoundForDivision);
            }
            foreach (CourtRound courtRound in tournament.CourtRounds)
            {
                foreach (Game game in courtRound.Games)
                {
                    Assert.IsTrue(numberOfGamesPerRobinRoundByDivision[game.Division.NumId].ContainsKey(game.RobinRoundNum - 1), GetTournamentId(tournament) + " - Game {0} is in robin round {1}; only {2} robin rounds", game, game.RobinRoundNum, numberOfRobinRounds);
                    numberOfGamesPerRobinRoundByDivision[game.Division.NumId][game.RobinRoundNum - 1]++;
                }
            }
            foreach (Division division in tournament.Divisions)
            {
                int numberOfGames = division.Teams.Count / 2;
                for (int robinRoundNum = 0; robinRoundNum < numberOfRobinRounds; robinRoundNum++)
                {
                    Assert.AreEqual(numberOfGames, numberOfGamesPerRobinRoundByDivision[division.NumId][robinRoundNum], GetTournamentId(tournament) + " - Incorrect number of games: Robin Round {0} {1}", robinRoundNum, division);
                }
            }
        }


        private static int tournamentNumber = 0;
        private void TestTournaments(params Tournament[] tournaments)
        {
            //Run tests
            for (int i = 0; i < tournaments.Length; i++)
            {
                tournamentNumber++;
                Tournament tournament = tournaments[i];
                if (tournamentNumber % 100 == 0)
                {
                    Trace.WriteLine("Testing: " + GetTournamentId(tournament));
                }
                Assert.IsNotNull(tournament.CourtRounds);
                this.NotSameTime(tournament);
                this.NoVacantCourts(tournament);
                this.PreferBetterTeams(tournament);
                this.DistributeTeamsAcrossCourts(tournament);
                this.TeamsPlayTeams(tournament);
                this.CorrectNumberOfGamesInRobinRounds(tournament);
            }
        }

        private string GetTournamentId(Tournament tournament)
        {
            string numTeamsStr = "";
            foreach (Division division in tournament.Divisions)
            {
                if (numTeamsStr.Length > 0) numTeamsStr += ",";
                numTeamsStr += division.Teams.Count;
            }
            return "NumDivisions:" + tournament.Divisions.Count + " NumTeams:" + numTeamsStr + " NumCourts:" + tournament.NumCourts;
        }

        private Tournament[] TournamentsFromSpecs(int[][] divisionsSpecs)
        {
            //Create tournament objects
            List<Tournament> Tournaments = new List<Tournament>();
            foreach (int[] divisionSpecs in divisionsSpecs)
            {
                for (int numCourts = minCourts; numCourts <= maxCourts; numCourts++)
                {
                    Tournaments.Add(new Tournament(buildDivisions(divisionSpecs), numCourts));
                }
            }
            return Tournaments.ToArray();
        }

        [Test]
        public void Selective()
        {
            //Create tournament objects
            Tournament[] tournaments = TournamentsFromSpecs(gamesToTest);
            TestTournaments(tournaments);
        }

        [Test]
        public void BruteForce()
        {
            for (int numDivisions = minDivisions; numDivisions <= maxDivisions; numDivisions++)
            {
                int[] previousDivisionSpecs = buildDefaultDivisionSpecs(numDivisions, minTeams, maxTeams);
                List<int[]> divisionsSpecs;
                bool shouldIncludeInitialDivisionSpecs = true;
                while ((divisionsSpecs = getNextDivisionSpecs(previousDivisionSpecs, minTeams, maxTeams, shouldIncludeInitialDivisionSpecs)).Count > 0)
                {
                    shouldIncludeInitialDivisionSpecs = false;
                    previousDivisionSpecs = divisionsSpecs[divisionsSpecs.Count - 1];

                    //Create tournament objects
                    Tournament[] tournaments = TournamentsFromSpecs(divisionsSpecs.ToArray());

                    TestTournaments(tournaments);
                    tournaments = null;
                }
            }
        }
    }
}