using System;
using System.ComponentModel;
using System.Reflection;
using Challenger.Models;

namespace Challenger.Business
{
    public static class Extensions
    {
        public static string ToDescription(this Enum element)
        {
            Type type = element.GetType();

            MemberInfo[] memberInfo = type.GetMember(element.ToString());

            if (memberInfo.Length > 0)
            {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return element.ToString();
        }

        public static void UpdateTodayGoal(this ChallengeOverviewModel c)
        {
            if (c.Type == ChallengeType.AddOneMoreEachDay)
            {
                c.TodayGoal = DateTime.Now.DayOfYear;
                c.TodayTodo = DateTime.Now.DayOfYear - c.TodayCount;
            }
        }
    }
}