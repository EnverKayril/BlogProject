﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static string FullDateAndTimeStringWithUnderscore(this DateTime dateTime)
        {
            return $"{dateTime.Millisecond}_{dateTime.Second}_{dateTime.Minute}_{dateTime.Hour}_{dateTime.Day}_{dateTime.Year}";
        }
    }
}
