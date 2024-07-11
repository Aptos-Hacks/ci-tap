using System.ComponentModel;
using System;
using UnityEditor;

public static class EnumUtility
{
    public static string GetDescription<TEnum>(TEnum enumValue) where TEnum : Enum
    {
        var enumType = typeof(TEnum);
        var memberInfo = enumType.GetMember(enumValue.ToString());
        if (memberInfo.Length > 0)
        {
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return ((DescriptionAttribute)attributes[0]).Description;
            }
        }
        return enumValue.ToString();
    }

    public static TEnum GetEnumValueByDescription<TEnum>(string description) where TEnum : Enum
    {
        var enumType = typeof(TEnum);
        foreach (var enumValue in Enum.GetValues(enumType))
        {
            var memberInfo = enumType.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    var enumDescription = ((DescriptionAttribute)attributes[0]).Description;
                    if (enumDescription == description)
                    {
                        return (TEnum)enumValue;
                    }
                }
            }
        }
        throw new ArgumentException($"No enum value with the description '{description}' found in {enumType.Name}");
    }
}