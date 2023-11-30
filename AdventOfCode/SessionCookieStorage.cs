using System;
using System.IO;
using AdventOfCode.Models;

namespace AdventOfCode;

public class SessionCookieStorage
{
    public string LoadSessionCookie()
    {
        var sessionCookie = new FileInfo(ApplicationConfig.SessionCookiePath);
        return sessionCookie.Exists ? File.ReadAllText(ApplicationConfig.SessionCookiePath) : string.Empty;
    }

    public void SaveSessionCookie(string sessionCookieValue)
    {
        var sessionCookie = new FileInfo(ApplicationConfig.SessionCookiePath);
        try
        {
            sessionCookie.Directory?.Create();
            File.WriteAllText(sessionCookie.FullName, sessionCookieValue);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}