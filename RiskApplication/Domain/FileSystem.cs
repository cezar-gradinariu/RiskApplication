﻿using System.IO;

namespace Domain
{
    public class FileSystem : IFileSystem
    {
        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}