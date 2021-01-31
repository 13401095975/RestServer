using RestServer.Common.Logger;
using RestServer.RestAttribute;
using System.Collections.Generic;

namespace Examples
{
    [Component("PersonService")]
    public class PersonService
    {
        private ILogger logger = LoggerFactory.GetLogger();

        public List<Person> GetPersonList() {
            return TestData.PersonList;
        }

        public Person GetPerson(int id)
        {
            return TestData.PersonList.Find(x => x.id == id);
        }

        public void Create(Person person)
        {
            logger.Debug(person.ToString());
        }
    }
}
