using System;

namespace Lists.Entity
{
    public class Person : IComparable<Person>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }

        public int CompareTo(object other)
        {
            if (other == null && LastName == null)
            {
                return 0;
            }
            else if (other == null)
            {
                return 1;
            }
            else if (LastName == null)
            {
                return -1;
            }

            Person otherPerson = other as Person;
            if (otherPerson != null)
                return LastName.CompareTo(otherPerson.LastName);
            else
                throw new ArgumentException("Object is not a Person");
        }

        public int CompareTo(Person other)
        {
            if (other == null && LastName == null)
            {
                return 0;
            }
            else if (other == null)
            {
                return 1;
            }
            else if (LastName == null)
            {
                return -1;
            }

            return LastName.CompareTo(other.LastName);

        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
