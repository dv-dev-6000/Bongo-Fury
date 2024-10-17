using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor
{
    public class DataIO
    {
        // Vars
        string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "LevelData");

        public DataIO() 
        {
            // Checks local files for LevelData data folder, if no folder exists a data folder is created 
            if (!Directory.Exists(outputDirectory))
            {
                // create main directory
                Directory.CreateDirectory(outputDirectory);
            }
        }

        private string GenerateFilename()
        {
            bool loop = true;
            int count = 1;
            string filename = "/LevelData_" + count + ".csv";

            while (loop)
            {
                if (File.Exists(outputDirectory + filename))
                {
                    count++;
                    filename = "/LevelData_" + count + ".csv";
                }
                else { loop = false; }
            }

            return filename;
        }

        public bool ExportLevel_CSV(List<Tile> tiles)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(outputDirectory + GenerateFilename()))
                {
                    // write data
                    for (int i = 0; i < 12; i++)
                    {
                        // Get Co-ords
                        List<Vector2> line = new List<Vector2>();
                        foreach (Tile tile in tiles)
                        {
                            if (tile.ID == i)
                            {
                                line.Add(tile.ActualPos);
                            }
                        }

                        // write line
                        string lineString = "";
                        foreach (Vector2 v in line)
                        {
                            lineString = lineString + v.X + "-" + v.Y +",";
                        }
                        sw.WriteLine(lineString);

                        // clear list
                        line.Clear();
                    }

                }


            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error: ", ex);
            }

            return false;
        }
    }
}
