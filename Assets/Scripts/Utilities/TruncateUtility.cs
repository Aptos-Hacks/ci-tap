using SimpleGraphQL;
using System;
using UnityEngine;

public static class TruncateUtility
{
    public static string TrucateAddress(string address)
    {
        if (address.IsNullOrEmpty() || address.Length < 8) return "";
        return $"{address[..6]}...{address[^2..]}";
    }
}