﻿using System.Collections.Generic;

namespace Lists.Entity
{
    public class PersonFirstNameAscendingComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            return x.FirstName.CompareTo(y.FirstName);
        }
    }
}
