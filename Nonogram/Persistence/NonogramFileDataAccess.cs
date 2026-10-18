using System;
using System.Collections.Generic;
using System.Text;

namespace Nonogram.Persistence
{
    public class NonogramFileDataAccess : INonogramDataAccess
    {
        public NonogramTable Load(string path)
        {
            try
            {
                using(StreamReader reader = new StreamReader(path))
                {
                    string? line = reader.ReadLine();
                    if(line == null) throw new NonogramDataException("File is empty");

                    int size = int.Parse(line);
                    NonogramTable table = new NonogramTable(size);

                    for(int i = 0; i < size; ++i)
                    {
                        line = reader.ReadLine();
                        if (line == null) throw new NonogramDataException("Invalid file format");

                        string[] parts = line.Split();
                        for(int j = 0; j < size; ++j)
                        {
                            bool isBlack = parts[j] == "X";
                            table.SetSolutionCell(i, j, isBlack);
                        }
                    }

                    //mentés e vagy új game
                    if(!reader.EndOfStream)
                    {
                        line = reader.ReadLine(); //elválasztó sor
                        if(line == "---")
                        {
                            line = reader.ReadLine();
                            if(line != null)
                            {
                                string[] info = line.Split();
                                table.NonogramTime = int.Parse(info[0]);
                                table.Lives = int.Parse(info[1]);
                            }

                            //player tábla
                            for(int i = 0; i < size; ++i)
                            {
                                line = reader.ReadLine();
                                if (line == null) break;

                                string[] parts = line.Split();
                                for(int j = 0; j < size; ++j)
                                {
                                    CellState state = CellState.Empty;
                                    if (parts[j] == "X") state = CellState.Filled;
                                    else if (parts[j] == "O") state = CellState.Crossed;

                                    table.SetPlayerCell(i, j, state);
                                }
                            }
                        }
                    }
                    
                    return table;
                }
            }
            catch(Exception e)
            {
                throw new NonogramDataException("Something went wrong while reading the file", e);
            }
        }

        public void Save(string path, NonogramTable table)
        {
            try
            {
                using(StreamWriter writer = new StreamWriter(path))
                {
                    int size = table.Size;

                    writer.WriteLine(size);

                    for(int i = 0; i < size; ++i)
                    {
                        for (int j = 0; j < size; ++j)
                        {
                            writer.Write(table.IsBlackInSolution(i, j) ? "X" : ".");
                            if (j < size - 1) writer.Write(" ");
                        }
                        writer.WriteLine();
                    }

                    writer.WriteLine("---");

                    writer.WriteLine($"{table.NonogramTime} {table.Lives}");

                    for(int i = 0; i < size; ++i)
                    {
                        for (int j = 0; j < size; ++j)
                        {
                            CellState state = table.GetPlayerCell(i, j);
                            if (state == CellState.Filled) writer.Write("X");
                            else if (state == CellState.Crossed) writer.Write("O");
                            else writer.Write(".");

                            if (j < size - 1) writer.Write(" ");
                        }
                        writer.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                throw new NonogramDataException("Something went wrong while saving the file", e);
            }
        }
    }
}
