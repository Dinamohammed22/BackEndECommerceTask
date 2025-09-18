﻿using System.Reflection;

namespace KOG.ECommerce.Helpers;

public class DescriptionAnnotation : System.Attribute
{
    private string name_ar;
    private string name_en;

    public DescriptionAnnotation(string name_ar, string name_en)
    {
        this.name_ar = name_ar;
        this.name_en = name_en;
    }

    public static string GetDescription(object obj)
    {
        string description = string.Empty;
        if (obj != null)
        {
            Type type = obj.GetType();
            MemberInfo[] members = type.GetMember(obj.ToString());
            if (members != null && members.Any())
            {
                object[] attributes = members[0].GetCustomAttributes(typeof(DescriptionAnnotation), false);
                if (attributes != null && attributes.Any())
                {
                    DescriptionAnnotation annotation = (DescriptionAnnotation)attributes[0];
                    if (Thread.CurrentThread.CurrentCulture.Name == "ar-EG" ||
                        (string.IsNullOrEmpty(annotation.name_en) && !string.IsNullOrEmpty(annotation.name_ar)))
                    {
                        description = annotation.name_ar;
                    }
                    else
                        description = annotation.name_ar;
                }
            }
            if (string.IsNullOrEmpty(description))
                description = obj.ToString();
        }
        return description;
    }
}
