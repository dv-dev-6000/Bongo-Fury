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
    /// <summary>
    /// Handles the export of completed level designs
    /// </summary>
    public class DataIO
    {
        // the path to the output directory (currently set to working directory)
        // ** to do - create game data folder in appdata for project (environment.special)
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

        /// <summary>
        /// Generates a unique filename for the output file to avoid overwriting 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Exports the current level data to CSV file
        /// </summary>
        /// <param name="tiles"> A list of all the tiles in the level</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public bool ExportLevel_CSV(List<Tile> tiles)
        {
            try
            {
                // write data
                using (StreamWriter sw = new StreamWriter(outputDirectory + GenerateFilename()))
                {
                    // for each tile type, go through the level grid and wirte the co-ors of assigned tiles to the file.
                    // * each row in resulting file represents a specific tile type
                    // * each column is the a new tile
                    // * each cell contains data of the tile position
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
