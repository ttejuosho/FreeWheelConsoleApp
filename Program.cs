using System.Text;

namespace FreeWheelConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter Question Number");
            Console.WriteLine("Valid Entries are 1, 2 or 3");
            Console.WriteLine("Enter exit at any point to exit the program");

            int questionNumber = 0;
            bool correctEntryReceived = false;
            bool questionOneDone = false;
            bool questionTwoDone = false;
            bool questionThreeDone = false;

            while(
                    correctEntryReceived == false || 
                    questionOneDone == false || 
                    questionTwoDone == false || 
                    questionThreeDone == false
                )
            {
                questionNumber = GetQuestionNumber();
                if(questionNumber >= 1 && questionNumber <= 3)
                    correctEntryReceived = true;

                if(correctEntryReceived == false)
                {
                    Console.WriteLine("Wrong entry received.");
                }

                if (questionNumber == 1 && questionOneDone == true)
                    Console.WriteLine("Question 1 Solution has been viewed");
                if (questionNumber == 2 && questionTwoDone == true)
                    Console.WriteLine("Question 2 Solution has been viewed");
                if (questionNumber == 3 && questionThreeDone == true)
                    Console.WriteLine("Question 3 Solution has been viewed");

                #region Question 1
                if (questionNumber == 1)
                {
                    while (questionOneDone == false)
                    {
                        var data = GetProgrammes().OrderBy(x => x.ProgramName).ToList();

                        var programNames = new StringBuilder();
                        foreach (var programme in data)
                        {
                            if (programme.ProgramName.Contains("'"))
                            {
                                var programName = programme.ProgramName;
                                for (int i = programName.IndexOf("'"); i > -1; i = programName.IndexOf("'", i + 1))
                                {
                                    programme.ProgramName = programme.ProgramName.Insert(i, "'");
                                }
                            }
                            programNames.Append($"'{programme.ProgramName}'");
                            if (programme == data.Last())
                            {
                                programNames.Append(".");
                            }
                            else
                            {
                                programNames.Append(",");
                            }
                        }
                        
                        Console.WriteLine("\n");
                        Console.WriteLine(programNames);
                        Console.WriteLine("======================= End of question " + questionNumber + " solution ==========================");
                        Console.WriteLine("============================================================================= \n");
                        questionOneDone = true;
                    }
                }
                #endregion

                #region Question 2
                if (questionNumber == 2)
                {
                    while (questionTwoDone == false)
                    {
                        var data = GetProgrammes().OrderBy(x => x.ProgramName).ToList();
                        var programNames = new StringBuilder();
                        programNames.AppendLine("Program Id\tStation Id\tProgram Name\t\tFlight Date");
                        foreach (var item in data)
                        {
                            programNames.AppendLine($"{item.ProgramId}\t\t{item.StationId}\t\t'{item.ProgramName}'\t{item.FlightDate.Date.ToString("MMM dd yyyy") }");
                        }
                        
                        Console.WriteLine("\n");
                        Console.WriteLine(programNames);

                        bool stationFound = false;
                        int stationId = 0;

                        List<int> stationIds = data.Select(x => x.StationId).ToList();

                        while (stationFound == false)
                        {
                            stationId = GetStationId();
                            stationFound = stationIds.Contains(stationId);

                            if (stationFound == false)
                                Console.WriteLine("Station not found.");
                        }

                        Programme programWithEarliestFlightDateAtStation = data.Where(x => x.StationId == stationId).ToList().OrderBy(a => a.FlightDate).First();
                        var output = new StringBuilder();
                        output.AppendLine("Program Id\tStation Id\tProgram Name\t\tFlight Date");
                        output.AppendLine(
                            $"{programWithEarliestFlightDateAtStation.ProgramId}\t\t" +
                            $"{programWithEarliestFlightDateAtStation.StationId}\t\t" +
                            $"'{programWithEarliestFlightDateAtStation.ProgramName}'\t" +
                            $"{programWithEarliestFlightDateAtStation.FlightDate.Date.ToString("MMM dd yyyy")}");
                        
                        Console.WriteLine("\n");
                        Console.WriteLine(output);
                        Console.WriteLine("======================= End of question " + questionNumber + " solution ==========================");
                        Console.WriteLine("============================================================================= \n");
                        questionTwoDone = true;
                    }
                }
                #endregion

                #region Question 3
                if (questionNumber == 3)
                {
                    while (questionThreeDone == false)
                    {
                        List<MarketPop> marketPopData = GetMarketPopData();

                        var marketPopDataTableBefore = new StringBuilder();
                        marketPopDataTableBefore.AppendLine("Market Id\tCell Id");
                        marketPopDataTableBefore.AppendLine("=========================");
                        foreach (MarketPop marketPop in marketPopData.OrderBy(x => x.MarketId))
                        {
                            marketPopDataTableBefore.AppendLine($"  {marketPop.MarketId}\t\t {marketPop.CellId}");
                        }

                        Console.WriteLine(marketPopDataTableBefore);

                        List<int> cellIds = marketPopData.Select(c => c.CellId).ToList();
                        List<int> marketIds = marketPopData.Select(m => m.MarketId).ToList();
                        Console.WriteLine($"Market Pop Data currently has {marketPopData.Count} items - {cellIds.Distinct().Count()} Cell Ids & {marketIds.Distinct().Count()} MarketIds");
                        Console.WriteLine($"Market Pop Data should contain {cellIds.Distinct().Count() * marketIds.Distinct().Count()} after the operation is complete");
                        foreach (int cellId in cellIds)
                        {
                            foreach (int marketId in marketIds)
                            {
                                var record = marketPopData.Where(x => x.MarketId == marketId && x.CellId == cellId).FirstOrDefault();
                                if (record == null)
                                {
                                    marketPopData.Add(new MarketPop
                                    {
                                        MarketId = marketId,
                                        CellId = cellId,
                                    });
                                }
                            }
                        }

                        Console.WriteLine($"Market Pop Data now has {marketPopData.Count} items.");
                        if ((cellIds.Distinct().Count() * marketIds.Distinct().Count()) == marketPopData.Count)
                        {
                            Console.WriteLine("Operation Successful");
                        }
                        var marketPopDataTable = new StringBuilder();
                        marketPopDataTable.AppendLine("Market Id\tCell Id");
                        marketPopDataTable.AppendLine("=========================");
                        foreach (MarketPop marketPop in marketPopData.OrderBy(x => x.MarketId))
                        {
                            marketPopDataTable.AppendLine($"  {marketPop.MarketId}\t\t {marketPop.CellId}");
                        }

                        Console.WriteLine("\n");
                        Console.WriteLine(marketPopDataTable);
                        Console.WriteLine("\n");
                        Console.WriteLine("Please take a look at the Q3.sql file for a possible stor proc type solution");
                        Console.WriteLine("======================= End of question " + questionNumber + " solution ==========================");
                        Console.WriteLine("============================================================================= \n");
                        questionThreeDone = true;
                    }
                }
                #endregion
            }

            static int GetQuestionNumber()
            {
                Console.WriteLine("Please enter a question number to see the solution");
                string input = Console.ReadLine().Trim();
                if(input.Length == 1 && int.TryParse(input, out int inputInteger))
                {
                    return inputInteger;
                }

                if(input.ToLower() == "exit")
                {
                    Environment.Exit(0);
                }

                return GetQuestionNumber();
            }

            static int GetStationId()
            {
                Console.Write("Please enter a Station Id to update \n");
                string input = Console.ReadLine().Trim();
                if(input.Length == 1 && int.TryParse(input, out int inputInteger))
                {
                    return inputInteger;
                }

                if (input.ToLower() == "exit")
                {
                    Environment.Exit(0);
                }

                return GetStationId();
            }

            static List<MarketPop> GetMarketPopData()
            {
                List<MarketPop> marketPopData = new List<MarketPop> { 
                    new MarketPop
                    {
                        MarketId = 1,
                        CellId = 1
                    },
                    new MarketPop
                    {
                        MarketId = 1,
                        CellId = 2
                    },
                    new MarketPop
                    {
                        MarketId = 2,
                        CellId = 4
                    }
                };
                return marketPopData;
            }

            static List<Programme> GetProgrammes()
            {
                List<Programme> programsData = new List<Programme>
                {
                    new Programme
                    {
                        ProgramId = 1,
                        StationId = 1,
                        ProgramName = "10 O''CLOCK NWS",
                        FlightDate = DateTime.Parse("3/11/1990")
                    },
                    new Programme
                    {
                        ProgramId = 2,
                        StationId = 1,
                        ProgramName = "ACCESS HOLLYWD",
                        FlightDate = DateTime.Parse("8/25/1991")
                    },
                    new Programme
                    {
                        ProgramId = 3,
                        StationId = 4,
                        ProgramName = "Jeop",
                        FlightDate = DateTime.Parse("6/30/1975")
                    },
                    new Programme
                    {
                        ProgramId = 4,
                        StationId = 3,
                        ProgramName = "C O''BRIEN-NBC''''Program",
                        FlightDate = DateTime.Parse("1/1/1989")
                    },
                    new Programme 
                    {
                        ProgramId = 3,
                        StationId = 4,
                        ProgramName = "Frasier''s",
                        FlightDate = DateTime.Parse("11/11/1991")
                    },
                    new Programme
                    {
                        ProgramId = 6,
                        StationId = 4,
                        ProgramName = "Barney",
                        FlightDate = DateTime.Parse("9/3/1966")
                    },
                    new Programme
                    {
                        ProgramId = 7,
                        StationId = 1,
                        ProgramName = "Just Shoot me",
                        FlightDate = DateTime.Parse("6/6/1996")
                    },
                    new Programme
                    {
                        ProgramId = 8,
                        StationId = 4,
                        ProgramName = "Wheel",
                        FlightDate = DateTime.Parse("6/30/1975")
                    },
                    new Programme
                    {
                        ProgramId = 9,
                        StationId = 3,
                        ProgramName = "Sesame Street",
                        FlightDate = DateTime.Parse("5/16/1971")
                    }
                };

                return programsData;
            }
        }
    }
}