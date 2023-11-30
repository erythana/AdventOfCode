using System;
using System.IO;

namespace AdventOfCode.Models;

public static class ApplicationConfig
{
    public static string SessionCookiePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AdventOfCode",
            "SessionCookie.txt");
}