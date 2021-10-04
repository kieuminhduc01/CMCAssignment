using DemoLibrary.Models;

namespace DemoLibrary.DataAccess
{
    public class DemoDataAccess : IDataAccess
    {
        private List<PersonModel> people = new();
        public DemoDataAccess()
        {
            people.Add(new PersonModel() { Id = 1, FirstName = "Kieu", LastName = "Duc" });
            people.Add(new PersonModel() { Id = 2, FirstName = "Hoi", LastName = "Tran" });
        }
        public List<PersonModel> GetPeople()
        {
            return people;
        }
        public PersonModel InsertPerson(string firstName, string lastName)
        {
            PersonModel p = new() { FirstName = firstName, LastName = lastName };
            p.Id = people.Max(a => a.Id) + 1;
            people.Add(p);
            return p;
        }
    }
}
