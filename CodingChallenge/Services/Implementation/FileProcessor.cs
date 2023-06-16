using CodingChallenge.Models;
using CodingChallenge.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Services.Implementation
{
    public class FileProcessor : IFileProcessor
    {
        public FileProcessor() 
        {
        
        }

        /// <summary>
        /// Gets all files in directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<FileIdentity>>> GetAllFilesInDirectory(string path)
        {
            ServiceResponse<List<FileIdentity>> response = new();

            try
            {
                List<FileIdentity> allFiles = new List<FileIdentity>();

                DirectoryInfo directory = new DirectoryInfo(path);

                FileInfo[] fileInfo = directory.GetFiles("*.csv");

                if (fileInfo.Length > 0)
                {
                    int counter = 0;

                    foreach (FileInfo file in fileInfo)
                    {
                        FileIdentity identity = new FileIdentity()
                        {
                            Id = counter,
                            Name = file.Name,
                            Length = file.Length
                        };

                        allFiles.Add(identity);

                        counter++;
                    }

                    response.Success = true;
                    response.Message = allFiles.Count.ToString() + " " + "files obtained";
                    response.Data = allFiles;
                }
                else
                {
                    response.Success = false;
                    response.Message = "No files found in the provided directory.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            
            return response;
        }

        /// <summary>
        /// Splits the files according to size.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ServiceResponse<List<List<FileIdentity>>>> SplitFilesBasedOnSize(List<FileIdentity> files)
        {
            ServiceResponse<List<List<FileIdentity>>> response = new();

            try
            {
                //Get number of files
                long numberOfFiles = files.Count;

                //Sort in ascending order
                List<FileIdentity> sortedFiles = files.OrderByDescending(x => x.Length).ToList();

                long availableThreads = GetAvailableThreads();

                List<List<FileIdentity>> result = new();

                if(numberOfFiles < availableThreads) 
                {  
                     //assign each file to its own thread
                }
                else
                {
                    //Use the number of threads available to distribute the files
                    for (int i = 0; i < availableThreads; i++)
                    {
                        result.Add(new List<FileIdentity>());          
                    }

                    //Ensure even distribution amongst the threads
                    int counter = 0;

                    for (int i = 0; i < sortedFiles.Count; i++)
                    {
                        result[counter].Add(sortedFiles[i]);

                        if(counter == result.Count - 1)
                        {
                            counter = 0;
                        }
                        else
                        {
                            counter++;
                        }
                    }
                }

                response.Success = true;
                response.Data = result;
            }   
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private long GetAvailableThreads()
        {
            return ThreadPool.ThreadCount;
        }
    }
}